using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Helpers;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.Momo;
using PRN231_GroupProject_LearningOnline.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();

builder.Services.AddDbContext<DonationWebApp_v2Context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
builder.Services.AddScoped<DonationWebApp_v2Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IMomoService, MomoService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Get the base URL of the application (http(s)://www.api.com) from the HTTP Request and Context.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext?.Request;
    var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
    return new UriService(uri);
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()  // Allow all origins
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddCustomizeSwagger();
builder.Services.AddSwaggerGen();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();
app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DonationWebApp_v2Context>();
        SeedDatabase(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();

void SeedDatabase(DonationWebApp_v2Context context)
{
    var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "seed-data.sql");

    if (File.Exists(sqlFilePath))
    {
        ClearDatabase(context);
        var sql = File.ReadAllText(sqlFilePath);
        context.Database.ExecuteSqlRaw(sql);
    }
    else
    {
        throw new FileNotFoundException("The seed-data.sql file was not found.", sqlFilePath);
    }
}

void ClearDatabase(DonationWebApp_v2Context context)
{
    //context.Database.ExecuteSqlRaw("DELETE FROM Category");

    //context.SaveChanges();
}