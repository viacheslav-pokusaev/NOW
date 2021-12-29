using Hangfire;
using Hangfire.Services;
using Hangfire.Services.JobTest;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hangfire", Version = "v1" });
});

builder.Services.AddHangfire(c => c.UseSqlServerStorage(configuration.GetConnectionString("SqlServerConnectionString")));

builder.Services.AddHangfireServer();

builder.Services.AddScoped<IJobTestService, JobTestService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//Basic Authentication added to access the Hangfire Dashboard  
app.UseHangfireDashboard("/hangfire");

app.Run();
