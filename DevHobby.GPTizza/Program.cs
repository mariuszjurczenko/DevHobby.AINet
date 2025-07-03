using DevHobby.GPTizza.Components;
using DevHobby.GPTizza.Components.Account;
using DevHobby.GPTizza.Contracts.Repositories;
using DevHobby.GPTizza.Contracts.Services;
using DevHobby.GPTizza.Data;
using DevHobby.GPTizza.Repositories;
using DevHobby.GPTizza.Services;
using DevHobby.GPTizza.Util;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CognitiveServices.Speech;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.Configure<ModelSettings>(builder.Configuration.GetSection("ModelSettings"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IPizzaDataService, PizzaDataService>();
builder.Services.AddScoped<IPizzaRecipeRepository, PizzaRecipeRepository>();
builder.Services.AddScoped<IPizzaRecipeDataService, PizzaRecipeDataService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketDataService, TicketDataService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDataService, OrderDataService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp =>
{
    var modelSettings = sp.GetRequiredService<IOptions<ModelSettings>>();
    return new OpenAIClient(modelSettings.Value.OPENAI_API_KEY);
});

builder.Services.AddScoped(sp =>
{
    var modelSettings = sp.GetRequiredService<IOptions<ModelSettings>>().Value;

    var config = SpeechConfig.FromSubscription(modelSettings.SPEECH_KEY, modelSettings.SPEECH_REGION);
    config.SpeechSynthesisVoiceName = modelSettings.SPEECH_Voice;

    return new SpeechSynthesizer(config);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapAdditionalIdentityEndpoints();

CreateRoles(app.Services);

app.Run();

void CreateRoles(IServiceProvider serviceProvider)
{
    var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    Task<IdentityResult> roleResult;

    string email = "admin@dev-hobby.pl";
    Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
    hasAdminRole.Wait();

    if (!hasAdminRole.Result)
    {
        roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
        roleResult.Wait();
    }

    Task<bool> hasUserRole = roleManager.RoleExistsAsync("User");
    hasUserRole.Wait();

    if (!hasUserRole.Result)
    {
        roleResult = roleManager.CreateAsync(new IdentityRole("User"));
        roleResult.Wait();
    }

    Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
    testUser.Wait();

    if (testUser.Result == null)
    {
        ApplicationUser administrator = new()
        {
            Email = email,
            UserName = email,
            EmailConfirmed = true
        };

        Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "admin@dev-hobby.pl1!L");
        newUser.Wait();

        if (newUser.Result.Succeeded)
        {
            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
            newUserRole.Wait();

            newUserRole = userManager.AddToRoleAsync(administrator, "User");
            newUserRole.Wait();
        }
    }
}
