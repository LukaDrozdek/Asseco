using AspNetCoreRateLimit;
using Asseco.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
  builder.Configuration.GetConnectionString("DefaultConnection")
  ));

// needed to load configuration from appsettings.json
builder.Services.AddOptions();

// needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();

//load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

//load ip rules from appsettings.json
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// inject counter and rules stores
builder.Services.AddInMemoryRateLimiting();
//services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
//services.AddDistributedRateLimiting<RedisProcessingStrategy>();
//services.AddRedisRateLimiting();

// Add framework services.
builder.Services.AddMvc();

// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


var app = builder.Build();

app.UseIpRateLimiting();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
