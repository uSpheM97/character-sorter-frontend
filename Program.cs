var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS (optional, for frontend fetch requests)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors(); // <-- allow frontend JS to call your API

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve index.html and other static files
app.UseDefaultFiles();  // Looks for index.html
app.UseStaticFiles();   // Serves files from wwwroot

// Your POST endpoint
app.MapPost("/sort", (InputModel input) =>
{
    if (input == null || string.IsNullOrEmpty(input.Data))
    {
        return Results.BadRequest(new { error = "Invalid input" });
    }

    var sortedArray = input.Data
                           .ToCharArray()
                           .OrderBy(c => c)
                           .ToArray();

    return Results.Ok(new
    {
        word = sortedArray
    });
})
.WithName("SortCharacters")
.WithOpenApi();

app.Run();

public record InputModel(string Data);