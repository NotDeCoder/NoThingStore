// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Data.Repositories.Implementations;
using NoThingStore.Data.Repositories.Interfaces;
using NoThingStore.Models;
using NoThingStore.Services.Implementations;
using NoThingStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ShoppingCart>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "NoThingStore.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ShoppingCart>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "product_create",
    pattern: "{controller}/Create",
    defaults: new { controller = "{controller}", action = "Create" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Manager", "Customer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string adminEmail = "admin@admin.com";
    string adminPassword = "Admin123!";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };

        await userManager.CreateAsync(admin, adminPassword);

        await userManager.AddToRoleAsync(admin, "Admin");

        await userManager.AddToRoleAsync(admin, "Manager");

        await userManager.AddToRoleAsync(admin, "Customer");
    }
}

app.Use(async (context, next) =>
{
    var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();

    if (context.User.Identity.IsAuthenticated)
    {
        var user = await userManager.GetUserAsync(context.User);
        if (user != null)
        {
            var isInRole = await userManager.IsInRoleAsync(user, "Customer");
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(user, "Customer");
            }
        }
    }

    await next.Invoke();
});

app.Run();
