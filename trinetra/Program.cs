using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using trinetra.Data;
using trinetra.Services;

namespace trinetra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/");
                options.Conventions.AllowAnonymousToPage("/Login");
            });
            builder.Services.AddDbContext<Data.AppDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = "/Login");
            builder.Services.AddScoped<Services.UserService>();

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

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedData.Initialize(db);
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapPost("/MyTasks/UpdateStatus", async (AppDbContext db, int id, int statusId) =>
            {
                var task = await db.Tasks.FindAsync(id);
                if (task == null) return Results.NotFound();
                task.StatusId = statusId;
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            app.Run();
        }
    }
}
