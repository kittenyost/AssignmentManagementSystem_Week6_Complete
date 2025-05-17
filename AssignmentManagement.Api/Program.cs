using AssignmentManagement.Core;
using AssignmentManagement.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
builder.Services.AddSingleton<IAppLogger, ConsoleAppLogger>();
builder.Services.AddSingleton<IAssignmentService, AssignmentService>();

//builder.Services.AddScoped<IAssignmentService, AssignmentService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure middleware
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

// 👇 Make Program class visible to WebApplicationFactory
public partial class Program { }
