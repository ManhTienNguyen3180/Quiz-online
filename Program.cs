var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    
    options.IdleTimeout = TimeSpan.FromSeconds(10000);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.UseStaticFiles();
app.UseSession();
app.Run();
