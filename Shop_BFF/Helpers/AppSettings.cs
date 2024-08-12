using Azure.Core;
using Humanizer;
using Jose;
using System.Net;

namespace Shop_BFF.Helpers
{
   
    public class AppSettings
    {
        public JwtSettings? Jwt { get; set; }
        public class JwtSettings
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
        }


    }
    //AppSettings.CS: maps JWT settings from appsettings.json.
    //AuthorizeAttribute.CS: can be used to manually check authorization.
    //Common.CS: provides a method to generate JWT tokens.
    //JwtMiddleware.CS: validates JWT tokens and attaches user information to the request context.

    /*Explanation of Fields
     * 
                    Key: This is the secret key used to sign the JWT tokens. It should be a complex, random string to ensure security. 
                                You can generate a strong key using tools like openssl or online generators. 
                                Important: Keep this key secure and never expose it in your source code or version control.

                    Issuer: This specifies who issued the JWT. It is typically the URL or name of the application that generates the token.

                    Audience: This specifies the recipient or target audience of the token. 
                            It represents the application or services that are supposed to use the token.

     * 
     */
    /*Binding Configuration Settings  In Program.cs, configure the settings like this:
     * 
     *          // Load app settings
                builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Jwt"));

                // Add services and middleware configurations
                // ...

                var app = builder.Build();

                // Add middleware and endpoints
                // ...

                app.Run();
     */
    /*Using Configuration in Your Code
            To access the JWT settings in your code, you can inject IOptions<AppSettings> into your services or controllers:

            Example in a Service:
                    using Microsoft.Extensions.Options;
                    using YourNamespace.Helpers; // Ensure this matches the namespace where AppSettings is defined

                    public class SomeService
                    {
                        private readonly AppSettings _appSettings;

                        public SomeService(IOptions<AppSettings> appSettings)
                        {
                            _appSettings = appSettings.Value;
                        }

                        public void SomeMethod()
                        {
                            var key = _appSettings.Key;
                            var issuer = _appSettings.Issuer;
                            var audience = _appSettings.Audience;

                            // Use these settings as needed
                        }
                    }
     */
}
