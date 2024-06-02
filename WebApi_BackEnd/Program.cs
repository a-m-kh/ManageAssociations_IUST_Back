using DataBase.Configuration.DBContexts;
using DataBase.Configuration.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Logic.Service.Services.Interface;
using Utility.Customize;
using Logic.Service.Services;
using Microsoft.OpenApi.Models;
using DataBase.Repository.Repositories.Interface;
using DataBase.Repository.Repositories;
using Repository;
using Logic.Service.Mapper;
using DataBase.Configuration.ConfigEntities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, MainContext>();

builder.Services.AddScoped<IAssociationRepository, AssociationRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IBaseInfoRepository, BaseInfoRepository>();
builder.Services.AddScoped<ICommunicationRepository, CommunicationRepository>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IAssociationMemberRepository, AssociationMemberRepository>();
builder.Services.AddScoped<IJournalRepository, JournalRepository>();
builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
builder.Services.AddScoped<IParticipantsOfCertificateRepository,ParticipantsOfCertificateRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IImageOfReportRepository, ImageOfReportRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IImageNewsRepository, ImageNewsRepository>();
builder.Services.AddScoped<IGuestLicenseRepository,GuestLicenseRepository>();
builder.Services.AddScoped<ICertificateOfAbsenceRepository, CertificateOfAbsenceRepository>();
builder.Services.AddScoped<IParticipantsOfCertificateOfAbsenceRepository, ParticipantsOfCertificateOfAbsenceRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<ICompetitiveFieldRepository, CompetitiveFieldRepository>();
builder.Services.AddScoped<IMovementFestivalRepository, MovementFestivalRepository>();
builder.Services.AddScoped<ISliderImageRepository, SliderImageRepository>();
builder.Services.AddScoped<IFormRepository, FormRepository>();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAssociationService, AssociationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IBaseInfoService, BaseInfoService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IAssociationMemberService, AssociationMemberService>();
builder.Services.AddScoped<IJournalService, JournalService>();
builder.Services.AddScoped<ICertificationService, CertificationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICertificateOfAbsenceService, CertificateOfAbsenceService>();
builder.Services.AddScoped<IMovementFestivalService, MovementFestivalService>();
builder.Services.AddScoped<ISliderImageService, SliderImageService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IGuestLicenseService, GuestLicenseService>();
//builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddAutoMapper(typeof(Logic.Service.Mapper.AutoMapper));

builder.Services.AddTransient<IUserValidator<User>, OptionalEmailUserValidator<User>>();
builder.Services.AddDbContext<UserDBContext>(opt =>
{
	opt.UseSqlServer(connectionStr/*, b=>b.MigrationsAssembly("WebApi_BackEnd")*/);
});
builder.Services.AddDbContext<MainContext>(opt =>
{
	opt.UseSqlServer(connectionStr);
});
builder.Services.AddIdentity<User, IdentityRole<string>>().AddEntityFrameworkStores<UserDBContext>();
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
		ValidAudience = jwtSettings.GetSection("validAudience").Value,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
	};
});

builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});



builder.Services.AddCors(options => {
	options.AddPolicy("test",
		builder => {
			builder
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("test");

app.MapControllers();

app.Run();
