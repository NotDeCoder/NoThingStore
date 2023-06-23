using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Data.Repositories.Implementations;
using NoThingStore.Data.Repositories.Interfaces;
using NoThingStore.Models;
using NoThingStore.Services.Implementations;
using NoThingStore.Services.Interfaces;
using System.Globalization;

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

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var ukCulture = new CultureInfo("uk");
    ukCulture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
    ukCulture.DateTimeFormat.DateSeparator = ".";
    ukCulture.DateTimeFormat.ShortTimePattern = "HH:mm";
    ukCulture.DateTimeFormat.TimeSeparator = ":";
    ukCulture.NumberFormat.NumberDecimalSeparator = ".";
    ukCulture.NumberFormat.NumberGroupSeparator = " ";
    ukCulture.NumberFormat.CurrencySymbol = "$";

    var enCulture = new CultureInfo("en-US");
    enCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
    enCulture.DateTimeFormat.DateSeparator = "/";
    enCulture.DateTimeFormat.ShortTimePattern = "HH:mm";
    enCulture.DateTimeFormat.TimeSeparator = ":";
    enCulture.NumberFormat.NumberDecimalSeparator = ".";
    enCulture.NumberFormat.NumberGroupSeparator = ",";


    var supportedCultures = new[] { ukCulture, enCulture };
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

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

app.UseRequestLocalization();

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

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
