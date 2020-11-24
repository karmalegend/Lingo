using Lingo.Data;
using Lingo.Data.Interfaces;
using Lingo.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo
{
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
            /*services.AddDbContext<UserContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LingoConnection")));
            services.AddDbContext<GameSessionContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LingoConnection")));
            services.AddDbContext<FiveLetterWordContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LingoConnection")));
            services.AddDbContext<SixLetterWordContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LingoConnection")));
            services.AddDbContext<SevenLetterWordContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LingoConnection")));*/

            services.AddDbContext<LingoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LingoConnection")));

            //using dependency injection to configure concrete of interface.
            services.AddScoped<IUserRepo, userRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
