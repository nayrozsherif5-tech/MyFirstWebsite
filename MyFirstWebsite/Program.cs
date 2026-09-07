using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();

provider.Mappings[".blend"] = "application/octet-stream";
provider.Mappings[".dwg"] = "application/octet-stream";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();