using DataAccessLayer.Persistence.Context;
using DataAccessLayer.Persistence.FakerData;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Persistence.Seeds
{

    public static class GenerateData
    {
        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            //Enable seeding data

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                FakeData.GenerateFakerData(context);
                return app;

            }
            catch (Exception)
            {

            }

            return app;
        }
    }

}
