using Autofac.Extensions.DependencyInjection;
using Autofac;
using Core.Dependency;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Business.ValidationRules;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:44371";
        options.Audience = "Employee";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:44371",
            ValidAudience = "Employee",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("employee"))
        };
    });
// Add services to the container.
builder.Services.AddAuthorization(_ =>
{
    _.AddPolicy("ReadEmployee", policy => policy.RequireClaim("scope", "Employee.Read"));
    _.AddPolicy("WriteEmployee", policy => policy.RequireClaim("scope", "Employee.Write"));
    _.AddPolicy("ReadWriteEmployee", policy => policy.RequireClaim("scope", "Employee.Write","Employee.Read"));
    _.AddPolicy("AllEmployee", policy => policy.RequireClaim("scope", "Employee.Admin"));



});
builder.Services.AddRazorPages();

var configuration = builder.Configuration;
builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();

app.Run();
