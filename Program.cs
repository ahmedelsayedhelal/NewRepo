using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Sakan_project.models;
using Sakan_project.Repository;
using Sakan_project.Services;

var builder = WebApplication.CreateBuilder(args);



// 🔹 1. إضافة الخدمات إلى الحاوية (Container)

// 📌 تكوين `DbContext`
// Replace the existing AddDbContext line with this:
builder.Services.AddDbContext<Sakancontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOwnerRepository, Ownerrepository>();
builder.Services.AddScoped<IApartmentReposatory, Apartmentreposaitory>();
builder.Services.AddScoped<ICollegeRepository, CollegeRepository>();

// 📌 إعداد الهوية (Identity)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Sakancontext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromMinutes(10));


builder.Services.AddTransient<IEmailService, EmailService>();

// 📌 إعداد المصادقة باستخدام `JWT`
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
// 📌 تفعيل CORS
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// 📌 إضافة التحكم في الـ API
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// 📌 إعداد Swagger لتوثيق API
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });

    // دعم المصادقة في Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 🔹 2. بناء التطبيق
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var task = Task.Run(async () =>
    {
        string[] roles = { "Owners", "Students" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    });

    task.Wait();
}




// 🔹 3. تهيئة الـ Middleware (الأنابيب)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo v1"));
}
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo v1"));



// 📌 دعم الملفات الثابتة (Static Files)
app.UseStaticFiles();

// 📌 تفعيل CORS

// 📌 تفعيل Routing
app.UseRouting();

app.UseCors("MyPolicy");


// 📌 تفعيل المصادقة (`JWT`)
app.UseAuthentication();

// 📌 تفعيل التفويض (Authorization)
app.UseAuthorization();

// 📌 تعيين المسارات (Endpoints)
app.MapControllers();

// 🔹 4. تشغيل التطبيق
app.Run();
