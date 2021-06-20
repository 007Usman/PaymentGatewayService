using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PaymentGateway.API.Helpers;
using PaymentGatway.Core.Encryption;
using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Data;
using PaymentGatway.Infrastructure.Repositories;
using PaymentGatway.Infrastructure.Repositories.Base;

namespace PaymentGateway.API
{
	public class Startup
	{
		#region Properties

		public IConfiguration Configuration { get; }

		#endregion

		#region Constructor

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		#endregion

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentGateway.API", Version = "v1" });
			});

			#region Schemas

			services.AddTransient<IEntityTimestamp, EntityTimestamp>();
			services.AddTransient<IBank, Bank>();
			services.AddTransient<IBankCard, BankCard>();
			services.AddTransient<ICurrency, Currency>();
			services.AddTransient<IMerchant, Merchant>();
			services.AddTransient<ITransaction, Transaction>();


			#endregion

			#region DatabaseConfigs

			services.AddDbContext<PaymentGatewayDBContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("PaymentGatewayConnection"), s => s.MigrationsAssembly("PaymentGateway.Infrastructure")), ServiceLifetime.Singleton);


			services.AddTransient<IRepository<BankCard>, BankCardRepository>();
			services.AddTransient<IRepository<Bank>, BankRepository>();
			services.AddTransient<IRepository<Currency>, CurrencyRepository>();
			services.AddTransient<IRepository<Merchant>, MerchantRepository>();
			services.AddTransient<IRepository<Transaction>, TransactionRepository>();

			#endregion

			services.AddSingleton<IStringEncryptor, StringEncryptor>();
			services.AddHttpClient<IClientRequestService, ClientRequestService>(s =>
			{
				// Replace in production
				s.BaseAddress = new Uri(Configuration.GetSection("BankApiBaseAddress").Value);
			});
		}


		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway.API v1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}