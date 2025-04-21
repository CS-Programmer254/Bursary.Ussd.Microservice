using Application.CommandHandlers;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUssdService, UssdService>();
builder.Services.AddScoped<ILoginFlowService,LoginFlowService>();
builder.Services.AddScoped<IRegistrationFlowService,RegistrationFlowService>();
builder.Services.AddScoped<IVerificationFlowService,VerificationFlowService>();
builder.Services.AddScoped<IFormatPhoneNumberService,FormatPhoneNumberService>();

builder.Services.AddHttpClient<IRegistrationFlowService, RegistrationFlowService>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(HandleUssdCommandHandler)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(LoginCommandHandler)));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(RegisterUserCommandHandler)));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
