using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Categories;
using Library.Persistence.Lendings;
using Library.Persistence.Members;
using Library.Services;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Categories;
using Library.Services.Categories.Contracts;
using Library.Services.Lendings;
using Library.Services.Lendings.Contracts;
using Library.Services.Members;
using Library.Services.Members.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Library.RestAPI
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

            services.AddControllers();
            services.AddScoped<LendingService, LendingAppService>();
            services.AddScoped<MemberService, MemberAppService>();
            services.AddScoped<BookService, BookAppService>();
            services.AddScoped<BookCategoryService, BookCategoryAppService>();
            services.AddScoped<BookRepository, EFBookRepository>();
            services.AddScoped<MemberRepository, EFMemberRepository>();
            services.AddScoped<BookCategoryRepository, EFBookCategoryRepository>();
            services.AddScoped<LendingRepository, EFLendingRepository>();
            services.AddScoped<UnitOfWork, EFUnitOfWork>();
            services.AddDbContext<EFDataContext>(_ =>_.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.RestAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.RestAPI v1"));
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
