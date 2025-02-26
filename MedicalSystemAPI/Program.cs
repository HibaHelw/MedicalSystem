using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
}); ;

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
//builder.Services.AddDbContext<MedicalContext>(option=>
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClinicServices, ClinicServices>();
builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IWorkingHoursServices, WorkingHoursServices>();
builder.Services.AddScoped<IServiceServices, ServiceServices>();
builder.Services.AddScoped<IClinicServiceServises, ClinicServiceServises>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
