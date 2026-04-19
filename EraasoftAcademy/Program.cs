using EraasoftAcademy.DataAccess;
using EraasoftAcademy.Models;
using EraasoftAcademy.Repositories;
using EraasoftAcademy.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EraasoftAcademy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IGenericRepository<Quiz>, GenericRepository<Quiz> >();
            builder.Services.AddScoped<IGenericRepository<QuizQuestion>, GenericRepository<QuizQuestion>>();
            builder.Services.AddScoped<IGenericRepository<QuestionChoices>, GenericRepository<QuestionChoices>>();
            builder.Services.AddScoped<IGenericRepository<QuizAttempt>, GenericRepository<QuizAttempt>>();
            builder.Services.AddScoped<IGenericRepository<StudentAnswer>, GenericRepository<StudentAnswer>>();
            builder.Services.AddScoped<IGenericRepository<Session>, GenericRepository<Session>>();
            builder.Services.AddScoped<IGenericRepository<StudentAttendance>, GenericRepository<StudentAttendance>>();
            builder.Services.AddScoped<IGenericRepository<StudentEnrollment>, GenericRepository<StudentEnrollment>>();
            builder.Services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Admin}/{controller=Quiz}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
