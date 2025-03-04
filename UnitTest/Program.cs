// See https://aka.ms/new-console-template for more information

using MedicalSystemModule.DTO;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

Console.WriteLine("Hello, World!");

var options = Options.Create(new AppSettings()
{
    //ConnectionString = "Data source=.\\SQLEXPRESS;Database=beetasking;Integrated Security=True;"
    ConnectionString = "Data source=.\\SQLEXPRESS2014;Database=MedicalDB;Integrated Security=True;Trusted_Connection=True;" +
                       "TrustServerCertificate=True;"
});

//ClinicServices clinicServices = new ClinicServices(options);
//clinicServices.CreateClinic(new TransformToClinic()
//{
//    Name = "jng",
//    Location = "ndgj",
//});

//UserServices userServices = new UserServices(options);
//var user = new TransformToUSer()
//{
//    Username = "hiba",
//    CreatedAt = DateTime.UtcNow,
//    Password = "123",
//    Email = "hh@gmail.com"
//};
//userServices.CreateUser(user);

DoctorServices doc = new DoctorServices(options);
var t=doc.GetAll();
var x = 5;
