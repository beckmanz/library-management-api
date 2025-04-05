using library_management_api.Data;
using library_management_api.Services.Auth;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using library_management_api.Extensions;
using library_management_api.Middlewares;
using library_management_api.Services.Author;
using library_management_api.Services.Book;
using library_management_api.Services.Library;
using library_management_api.Services.Loan;
using library_management_api.Services.Reader;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblies([typeof(Program).Assembly]);
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IBookInterface, BookService>();
builder.Services.AddScoped<IAuthorInterface, AuthorService>();
builder.Services.AddScoped<IReaderInterface, ReaderService>();
builder.Services.AddScoped<ILoanInterface, LoanService>();
builder.Services.AddScoped<ILibraryInterface, LibraryService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();