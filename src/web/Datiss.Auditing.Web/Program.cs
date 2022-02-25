using MediatR;
using System.Reflection;
using Datiss.Auditing.Configuration;
using Datiss.Auditing.Domain.Models;
using Datiss.Auditing.Identity.Commands;
using Datiss.Auditing.Identity.Validations;
using Datiss.Auditing.Identity.Web;
using Datiss.Auditing.Admin.Web;
using FluentValidation;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDatissCommon();
builder.Services.AddAuditingCore();
var appConfig = builder.Configuration.Get<AppConfig>();
builder.Services.AddSingleton<IConfiguration>(_ => configuration);
builder.Services.AddAuditingConfiguration(appConfig);
builder.Services.AddAuditingIdentity(appConfig);
builder.Services.AddAuditingSqlServer(appConfig);
//builder.Services.AddAuditingRepositories();

//Add all FluentValidation Validators
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();

//Add MediatR services
builder.Services.AddMediatR((new List<Assembly> {
    { Assembly.GetExecutingAssembly() },
    { typeof(User).Assembly },
    { typeof(AdminIndexPage).Assembly },
    { typeof(CreateUserCommand).Assembly },
    { typeof(LoginPage).Assembly }
}).ToArray());

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20); // set the session expired time.
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors();

builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
});

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeAreaPage("Admin", "/Index");
    options.Conventions.AuthorizeAreaFolder("Admin", "/Users");
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddMvc(_=> _.EnableEndpointRouting = false)
                .AddApplicationPart(typeof(AdminIndexPage).Assembly);

builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();

});
app.MapRazorPages();

app.Run();
