
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

			app.MapPost("/api/skill", async (Skill skill) =>
			{
				await db.AddData("Skills", skill);

				return Results.Ok($"{skill.Name} was added");
			});

			app.Run();
		}
	}
}
