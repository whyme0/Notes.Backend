using System.Reflection;
using Notes.Persistence;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config => 
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

// Add usefull stuff of application layer
builder.Services.AddApplication();

// Add database services
builder.Services.AddPersistence(builder.Configuration);

// Adding CORS
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowAnyOrigin();
    });
});

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<NotesDbContext>();
        DbInitializer.Initialize(context);
    }
    catch
    {
    }
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
