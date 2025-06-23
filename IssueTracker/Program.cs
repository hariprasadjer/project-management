using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;
using IssueTracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Login");
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o => o.LoginPath = "/Login");

builder.Services.AddScoped<UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    SeedData.Initialize(db);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapPost("/MyTasks/UpdateStatus", async (AppDbContext db, int id, int statusId) =>
{
    var issue = await db.IssueTasks.FindAsync(id);
    if (issue == null) return Results.NotFound();
    issue.StatusId = statusId;
    issue.UpdatedUtc = DateTime.UtcNow;
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
