using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TucGolfklubb.Data;
using TucGolfklubb.Models;

var builder = WebApplication.CreateBuilder(args);

// Databasanslutning
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity + Roller
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI() // Lägg till för att använda inbyggt Identity UI (Login, Register etc.)
    .AddDefaultTokenProviders(); // Viktigt för återställning, bekräftelse m.m.

builder.Services.ConfigureApplicationCookie(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() //  Aktiverar roller!
.AddEntityFrameworkStores<ApplicationDbContext>();

// Razor Pages + MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); //  Viktigt för Identity sidor

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); //  Krävs för Identity Razor Pages

// Seed adminroll och användare
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Skapa Admin-roll om den inte finns
    var roleExists = await roleManager.RoleExistsAsync("Admin");
    if (!roleExists)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Skapa default adminanvändare
    string adminEmail = "admin@golfklubb.se";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}

app.Run();
