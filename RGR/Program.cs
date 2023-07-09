using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using RGR.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddViewOptions(options =>
{
    options.HtmlHelperOptions.ClientValidationEnabled = true;
});

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Authorization}/{action=Index}/{id?}"
        );
    endpoints.MapControllerRoute(
       name: "registration",
       pattern: "{controller=Authorization}/{action=Registration}/{id?}"
   );
    endpoints.MapControllerRoute(
        name: "login",
        pattern: "{controller=Authorization}/{action=Login}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "myAccount",
        pattern: "{controller=Main}/{action=MyAccount}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "createPost",
        pattern: "{controller=Main}/{action=CreatePost}/{id?}"
    );
    endpoints.MapControllerRoute(
        name:"posts",
        pattern: "{controller=Main}/{action=Posts}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "editPost",
        pattern: "{controller=Main}/{action=EditPost}/{id?}"
);
    endpoints.MapControllerRoute(
    name: "deletePost",
    pattern: "{controller=Main}/{action=DeletePost}/{id?}");
});

app.Run();
