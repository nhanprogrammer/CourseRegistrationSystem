using System.Text.Json;
using CourseRegistrationSystem.Repositories;
using CourseRegistrationSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CORS đúng cách
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Thêm nếu bạn cần cookies/auth
    });
});

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SchoolDB")));

// Đăng ký các services
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<EnrollmentRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoleClaimRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter("yyyy-MM-dd"));
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ⚠️ Thứ tự middleware quan trọng!
// 1. CORS trước HttpsRedirection
app.UseCors("AllowFrontend");

// 2. HttpsRedirection (nếu cần)
// Trong môi trường dev, có thể bỏ dòng này để tránh redirect vấn đề CORS
// if (app.Environment.IsProduction())
// {
//     app.UseHttpsRedirection();
// }

// 3. Authentication trước Authorization
app.UseAuthentication();
app.UseAuthorization();

// 4. Controllers
app.MapControllers();

app.Run();