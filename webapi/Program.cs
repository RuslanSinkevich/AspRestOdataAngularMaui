using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Mysqlx.Crud;
using webapi.DataAccess;
using webapi.DataAccess.Repository;
using webapi.DataAccess.Repository.IRepository;
using webapi.Models;


var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
modelBuilder.EntitySet<TasksL>("ListTasks");
modelBuilder.EntitySet<Tasks>("Tasks");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

builder.Services.AddCors();

builder.Services.AddScoped<IListTasksRepository, ListTasksRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.
    AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod() );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
