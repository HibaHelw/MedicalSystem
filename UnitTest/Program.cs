﻿// See https://aka.ms/new-console-template for more information

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

ClinicServices clinicServices = new ClinicServices(options);
clinicServices.CreateClinic(new TransformToClinic()
{
    Name = "jng",
    Location = "ndgj",
});
