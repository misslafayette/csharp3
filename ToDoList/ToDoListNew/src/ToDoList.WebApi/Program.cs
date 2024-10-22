var builder = WebApplication.CreateBuilder(args);
{
    // Configure DI
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    // Configure Middleware (HTTP request pipeline)
    app.MapControllers();
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/nazdarSvete", () => "Nazdar svete x2!");

app.Run();
