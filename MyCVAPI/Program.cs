
using Common.Data;
using Common.Models;

namespace MyCVAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthorization();
			builder.Services.AddEndpointsApiExplorer();
			var connectionString = builder.Configuration.GetConnectionString("MicaelCVMongoDB") ?? throw new InvalidOperationException("Connection string not found.");
			builder.Services.AddScoped(_ => new CVDatabase("CVDatabase", connectionString));
			//CVDatabase db = new CVDatabase("CVDatabase");

			var app = builder.Build();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			//Read
            app.MapGet("/api/skills", async (CVDatabase db) =>
			{
				var skills = await db.GetAllData<Skill>("Skills");

				return Results.Ok(skills);
			});

            app.MapGet("/api/admins", async (CVDatabase db) =>
            {
                var admins = await db.GetAllData<Admin>("Admins");

                return Results.Ok(admins);
            });

            app.MapGet("/api/projects", async (CVDatabase db) =>
            {
	            var project = await db.GetAllData<Project>("Projects");

	            return Results.Ok(project);
            });

			//Create

			app.MapPost("/api/skill", async (Skill skill, CVDatabase db) =>
			{
				await db.AddData("Skills", skill);

				return Results.Ok($"{skill.Name} was added");
			});

            app.MapPost("/api/admin", async (Admin admin, CVDatabase db) =>
            {
                await db.AddData("Admins", admin);

                return Results.Ok($"{admin.Name} was added");
            });

            app.MapPost("/api/project", async (Project project, CVDatabase db) =>
            {
	            await db.AddData("Projects", project);

	            return Results.Ok($"{project.Name} was added");
            });

			app.Run();
		}
	}
}
