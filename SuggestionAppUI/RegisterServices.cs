using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

namespace SuggestionAppUI;

public static class RegisterServices
{

   public static void ConfigureServices(this  WebApplicationBuilder builder)
   {
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddMemoryCache();

      builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
         .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

      builder.Services.AddAuthorization(options =>
      {
         options.AddPolicy("Admin", policy =>
         {
            policy.RequireClaim("jobTitle", "Admin");
         });

      });

      builder.Services.AddSingleton<IDBConnection, DBConnection>();
      builder.Services.AddTransient<ICategoryData, MongoCategoryData>();
      builder.Services.AddTransient<IStatusData,MongoStatusData>();
      builder.Services.AddTransient<ISuggestionData, MongoSuggestionData>();
      builder.Services.AddTransient<IUserData, MongoUserData>();


   }
}
