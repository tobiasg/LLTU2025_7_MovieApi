using LLTU2025_7_MovieApi.Data;
using System.Diagnostics;

namespace LLTU2025_7_MovieApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationContext>();
            
            try
            {
                await SeedData.InitAsync(context);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
