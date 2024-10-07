using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProdutoCrudAPI.Application.Services;
using ProdutoCrudAPI.Domain.Repositories;
using ProdutoCrudAPI.Infrastructure.Data;
using ProdutoCrudAPI.Infrastructure.Filters;
using ProdutoCrudAPI.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var stringKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
if (string.IsNullOrEmpty(stringKey))
{
    throw new InvalidOperationException("JWT_SECRET_KEY nao esta definida nas variaveis de ambiente.");
}
var key = Encoding.ASCII.GetBytes(stringKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add(new TokenAuthorizationFilter(stringKey));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
