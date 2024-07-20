using CleanArch.Infra.IoC;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Dependency Injection
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);
builder.Services.AddInfrastructureSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "CleanArch API",
        Description = "Projeto API",
        TermsOfService = new Uri("https://examplp.com/termoservico"),
        Contact = new OpenApiContact
        {
            Name = "Contato",
            Url = new Uri("https://examplo.com/contato")
        },
        License = new OpenApiLicense
        {
            Name = "Licença",
            Url = new Uri("https://examplo.com/licenca")
        }
    });

    // usando System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStatusCodePages();

app.UseAuthorization();

app.MapControllers();

app.Run();
