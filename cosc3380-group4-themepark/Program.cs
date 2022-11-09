using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
            });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerSpecific", policy => policy.RequireClaim("Role", "Manager", "Maintenance_Manager"));
    options.AddPolicy("SalesSpecific", policy => policy.RequireClaim("Role", "Sales"));
    options.AddPolicy("MaintenanceSpecific", policy => policy.RequireClaim("Role", "Maintenance", "Maintenance_Manager"));
    options.AddPolicy("MaintManagerSpecific", policy => policy.RequireClaim("Role", "Maintenance_Manager"));
    options.AddPolicy("CustomerSpecific", policy => policy.RequireClaim("Role", "Customer"));
    options.AddPolicy("HRSpecific", policy => policy.RequireClaim("Role", "HR"));
});

// Add services to the container.
builder.Services.AddRazorPages();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

