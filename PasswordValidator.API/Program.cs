using PasswordValidator.Domain.Interfaces.Rules;
using PasswordValidator.Domain.Rules;
using PasswordValidator.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Registrando regras de senha como IPasswordRule, permitindo que o PasswordValidationService receba todas as regras registradas via injeção de dependência
builder.Services.AddSingleton<IPasswordRule, MinimumLengthRule>();
builder.Services.AddSingleton<IPasswordRule, ContainsDigitRule>();
builder.Services.AddSingleton<IPasswordRule, ContainsLowercaseLetterRule>();
builder.Services.AddSingleton<IPasswordRule, ContainsUppercaseLetterRule>();
builder.Services.AddSingleton<IPasswordRule, ContainsSpecialCharacterRule>();
builder.Services.AddSingleton<IPasswordRule, OnlyAllowedCharactersRule>();
builder.Services.AddSingleton<IPasswordRule, NoRepeatedCharactersRule>();

builder.Services.AddSingleton<IPasswordValidator, PasswordValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Necessário para que o WebApplicationFactory<Program> do projeto de testes de integração consiga referenciar este Program
public partial class Program { }