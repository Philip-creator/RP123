using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;
using RP123.Data;
using RP123.PTApp.Data;

namespace RP123
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // PTApp data context for migrated Progress entities
            builder.Services.AddDbContext<PTAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            // Apply migrations and seed a confirmed test user for development
            try
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();

                var db = services.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();

                // Migrate and seed PTApp data
                var ptDb = services.GetRequiredService<PTAppDbContext>();
                ptDb.Database.Migrate();

                if (!ptDb.Customers.Any())
                {
                    var c1 = new RP123.PTApp.Models.Customer { Name = "Contoso Ltd", Email = "sales@contoso.local", ShortName = "Contoso", CreditLimit = 10000m, IsActive = true };
                    var c2 = new RP123.PTApp.Models.Customer { Name = "Fabrikam", Email = "info@fabrikam.local", ShortName = "Fabrikam", CreditLimit = 5000m, IsActive = true };
                    ptDb.Customers.AddRange(c1, c2);

                    var i1 = new RP123.PTApp.Models.Item { Sku = "SKU-001", Description = "Sample Item 1", Price = 12.50m, Cost = 8.00m };
                    var i2 = new RP123.PTApp.Models.Item { Sku = "SKU-002", Description = "Sample Item 2", Price = 25.00m, Cost = 15.00m };
                    ptDb.Items.AddRange(i1, i2);

                    ptDb.SaveChanges();

                    var order = new RP123.PTApp.Models.SalesOrder { CustomerId = c1.Id, OrderDate = DateTime.UtcNow, TotalAmount = i1.Price * 2 };
                    ptDb.SalesOrders.Add(order);
                    ptDb.SaveChanges();

                    var line = new RP123.PTApp.Models.SalesOrderLine { SalesOrderId = order.Id, ItemId = i1.Id, Quantity = 2, UnitPrice = i1.Price };
                    ptDb.SalesOrderLines.Add(line);
                    ptDb.SaveChanges();
                }

                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var testEmail = "dev@test.local";
                var testUser = userManager.FindByEmailAsync(testEmail).GetAwaiter().GetResult();
                if (testUser == null)
                {
                    testUser = new IdentityUser { UserName = testEmail, Email = testEmail, EmailConfirmed = true };
                    var createResult = userManager.CreateAsync(testUser, "P@ssw0rd!").GetAwaiter().GetResult();
                    if (!createResult.Succeeded)
                    {
                        logger.LogWarning("Failed to create seeded test user: {Errors}", string.Join(';', createResult.Errors.Select(e => e.Description)));
                    }
                    else
                    {
                        logger.LogInformation("Seeded test user {Email}", testEmail);
                    }
                }
                else if (!testUser.EmailConfirmed)
                {
                    testUser.EmailConfirmed = true;
                    var updateResult = userManager.UpdateAsync(testUser).GetAwaiter().GetResult();
                    if (updateResult.Succeeded)
                    {
                        logger.LogInformation("Confirmed seeded user {Email}", testEmail);
                    }
                }

                // Ensure Admin role exists and assign to seeded user
                var adminRole = "Admin";
                var roleExists = roleManager.RoleExistsAsync(adminRole).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var rr = roleManager.CreateAsync(new IdentityRole(adminRole)).GetAwaiter().GetResult();
                    if (!rr.Succeeded)
                    {
                        logger.LogWarning("Failed to create role Admin: {Errors}", string.Join(';', rr.Errors.Select(e => e.Description)));
                    }
                }

                // Add user to Admin role
                var inRole = userManager.IsInRoleAsync(testUser, adminRole).GetAwaiter().GetResult();
                if (!inRole)
                {
                    var addRoleResult = userManager.AddToRoleAsync(testUser, adminRole).GetAwaiter().GetResult();
                    if (!addRoleResult.Succeeded)
                    {
                        logger.LogWarning("Failed to add seeded user to Admin role: {Errors}", string.Join(';', addRoleResult.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                // If seeding fails, log and continue. In production consider failing fast.
                var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }

            app.Run();
        }
    }
}
