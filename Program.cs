using Kanban.Data;
using Kanban.Extensions;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.ConfigureOptions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IRepository<TeamMember>, TeamMemberRepository>();
builder.Services.AddScoped<IRepository<Team>, TeamRepository>();
builder.Services.AddSwaggerGen();

// Add Database
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(config.GetConnectionString("KanbanDatabase")));

// Add Services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Add Identity
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

// Add Authentication and JWT
builder.Services
    .AddAuthentication()
    .AddJwtBearer();

builder.Services
    .AddApiVersioning()
    .AddMvc()
    .AddApiExplorer();

// Pipeline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var versions = app.DescribeApiVersions();
        foreach (var version in versions)
            options.SwaggerEndpoint($"/swagger/{version.GroupName}/swagger.json", version.GroupName);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();