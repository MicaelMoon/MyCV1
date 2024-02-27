
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
			CVDatabase db = new CVDatabase("CVDatabase");

			var app = builder.Build();

			app.UseHttpsRedirection();

			app.UseAuthorization();

            app.MapGet("/api/skills", async () =>
			{
				var skills = await db.GetAllData<Skill>("Skills");

				return Results.Ok(skills);
			});

            app.MapGet("/api/admins", async () =>
            {
                var admins = await db.GetAllData<Admin>("Admins");

                return Results.Ok(admins);
            });

            app.MapPost("/api/skill", async (Skill skill) =>
			{
				await db.AddData("Skills", skill);

				return Results.Ok($"{skill.Name} was added");
			});

            app.MapPost("/api/admin", async (Admin admin) =>
            {
                await db.AddData("Admins", admin);

                return Results.Ok($"{admin.Name} was added");
            });

            app.Run();
		}
	}
}
