using FaithMiApplication1.Jwt;
using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using FaithMiApplication1.Repositories.Dao;
using FaithMiApplication1.Repositories.Impl;

namespace FaithMiApplication1
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
            services.AddScoped<IProductDao, ProductDao>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IOrderDao, ShoppingDao>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FaithMiApplication1", Version = "v1" });
            });
            // 首先取出appsettings中的连接字符串，注意参数名应与appsettings中键一致
            var connectionString = Configuration.GetConnectionString("Database");
            // 类型是生成的上下文类名
            services.AddDbContext<faithdbContext>(options => options.UseMySQL(connectionString));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddCors(options =>
            //跨域
            options.AddPolicy("any", policy =>
             {
                 string Origins = "http://localhost:8089";
                 policy.WithOrigins(Origins.Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
             }));

            //jwt
            //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            //将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);
            //添加jwt验证：
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = Const.Domain,//Audience
                        ValidIssuer = Const.Domain,//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey))//拿到SecurityKey
                    };
                });



            services.AddSwaggerGen(c =>
                    {
              
                        c.DocInclusionPredicate((docName, description) => true);
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格",
                            Name = "Authorization",//jwt默认的参数名称
                            In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                            Type = SecuritySchemeType.ApiKey
                        });
                        //认证方式，此方式为全局添加
                          c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                    { new OpenApiSecurityScheme
                                    {
                                    Reference = new OpenApiReference()
                                    {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                    }
                                    }, Array.Empty<string>() }
                                    });
                    });

                      services.AddControllers().AddNewtonsoftJson(option =>
                       //忽略循环引用
                       option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                   );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaithMiApplication1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors("any");

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                #region 开发环境下启用Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

                #endregion
            }
        }
    }
}
