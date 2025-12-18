
using LifeDrop.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

string connectionString;

var secretArn = Environment.GetEnvironmentVariable("DB_SECRET_ARN");

if (!string.IsNullOrEmpty(secretArn))
{
    // Running on Elastic Beanstalk
    var client = new AmazonSecretsManagerClient();

    var response = client.GetSecretValueAsync(new GetSecretValueRequest
    {
        SecretId = secretArn
    }).GetAwaiter().GetResult();

    var secret =
        JsonSerializer.Deserialize<Dictionary<string, string>>(response.SecretString);

    connectionString =
        $"Server={secret["DB_HOST"]};" +
        $"Port={secret["DB_PORT"]};" +
        $"Database={secret["DB_NAME"]};" +
        $"User={secret["DB_USER"]};" +
        $"Password={secret["DB_PASSWORD"]};";
}
else
{
    // Local development (keeps your existing setup)
    connectionString = builder.Configuration.GetConnectionString("BloodDonationConnection");
}


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.Parse("8.0.41-mysql")
    ));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


builder.Services.AddSession();


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();