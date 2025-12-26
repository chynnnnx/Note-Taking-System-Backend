using System;
using FirstAngular.Application.Common.Seeder;

namespace FirstAngular.Application.Common.Seeder
{
    public static class AppSeeder
    {
        public static void Seed(IServiceProvider services, string[] roles)
        {
             RoleSeeder.SeedRolesAsync(services, roles)
                .GetAwaiter().GetResult();
        }
    }
}
