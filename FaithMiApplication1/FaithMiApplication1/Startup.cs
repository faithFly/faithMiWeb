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
            // ����ȡ��appsettings�е������ַ�����ע�������Ӧ��appsettings�м�һ��
            var connectionString = Configuration.GetConnectionString("Database");
            // ���������ɵ�����������
            services.AddDbContext<faithdbContext>(options => options.UseMySQL(connectionString));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddCors(options =>
            //����
            options.AddPolicy("any", policy =>
             {
                 string Origins = "http://localhost:8089";
                 policy.WithOrigins(Origins.Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
             }));

            //jwt
            //��appsettings.json�е�JwtSettings�����ļ���ȡ��JwtSettings�У����Ǹ������ط��õ�
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            //���ڳ�ʼ����ʱ�����Ǿ���Ҫ�ã�����ʹ��Bind�ķ�ʽ��ȡ����
            //�����ð󶨵�JwtSettingsʵ����
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);
            //���jwt��֤��
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//�Ƿ���֤Issuer
                        ValidateAudience = true,//�Ƿ���֤Audience
                        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                        ValidAudience = Const.Domain,//Audience
                        ValidIssuer = Const.Domain,//Issuer���������ǰ��ǩ��jwt������һ��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey))//�õ�SecurityKey
                    };
                });



            services.AddSwaggerGen(c =>
                    {
              
                        c.DocInclusionPredicate((docName, description) => true);
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ���·�����Bearer {token} ���ɣ�ע������֮���пո�",
                            Name = "Authorization",//jwtĬ�ϵĲ�������
                            In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                            Type = SecuritySchemeType.ApiKey
                        });
                        //��֤��ʽ���˷�ʽΪȫ�����
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
                       //����ѭ������
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
                #region ��������������Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

                #endregion
            }
        }
    }
}
