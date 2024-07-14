using DinkToPdf.Contracts;
using DinkToPdf;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Helpers;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.Momo;
using PRN231_GroupProject_LearningOnline.Services;
using PRN231_GroupProject_LearningOnline.Notification;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();

builder.Services.AddDbContext<DonationWebApp_v2Context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
builder.Services.AddScoped<DonationWebApp_v2Context>();
builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddControllersWithViews();
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IExportHTMLtoPDF, ExportHTMLtoPDF>();
builder.Services.AddScoped<IMomoService, MomoService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Đăng ký BackgroundService
builder.Services.AddHostedService<MyBackgroundService>();
builder.Services.AddSingleton<IUserContextService, UserContextService>();

//CORS
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


//send mail
builder.Services.AddOptions();                                        // Kích hoạt Options
var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
builder.Services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject
builder.Services.AddTransient<IEmailSender, MailService>();        // Đăng ký dịch vụ Mail
//send mail



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
app.UseStaticFiles();

app.MapHub<NotiHub>("/notiHub");
app.UseRouting();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<YourMiddleware>();



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