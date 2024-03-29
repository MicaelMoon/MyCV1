using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCV1.Components;
using MyCV1.Components.Account;
using MyCV1.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MyCV1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddHttpClient();

			builder.Services.AddCascadingAuthenticationState();
			builder.Services.AddScoped<IdentityUserAccessor>();
			builder.Services.AddScoped<IdentityRedirectManager>();
			builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

			builder.Services.AddAuthentication(options =>
				{
					options.DefaultScheme = IdentityConstants.ApplicationScheme;
					options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
				})
				.AddIdentityCookies();

			var connectionString = builder.Configuration.GetConnectionString("IdentitySqlString") ?? throw new InvalidOperationException("Connection string not found.");
			//var connectionString = Environment.GetEnvironmentVariable("IdentitySqlString",EnvironmentVariableTarget.Machine) ?? throw new InvalidOperationException("Connection string not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddSignInManager()
				.AddDefaultTokenProviders();

			builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				//app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			// Add additional endpoints required by the Identity /Account Razor components.
			app.MapAdditionalIdentityEndpoints();

			app.Run();
		}
	}
}
