
using Microsoft.EntityFrameworkCore;
using Fshop.Models;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		var connection = Configuration.GetConnectionString("DefaultConnection");
		services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

		services.AddControllersWithViews();
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
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

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
			  name: "areas",
			  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
			);

			endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
		});
	}
}
