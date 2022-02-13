using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCache.Data.Seeder
{
    public static class DatabaseSeeder
    {

        public static void SeedDatabase(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            CheckAndSeed(scope.ServiceProvider.GetRequiredService<AppDbContext>());
        }

        private static void CheckAndSeed(AppDbContext _dbContext)
        {

            if (_dbContext.Blogs.Count() == 0)
            {
                System.Console.WriteLine("We dont have data data seeding...");
                for (int i = 0; i < 50; i++)
                {
                    _dbContext.Blogs.Add(new Blogs
                    {
                        BlogTitle = FakeData.NameData.GetCompanyName(),
                        BlogContent = FakeData.TextData.GetSentences(5)
                    });
                }
                _dbContext.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("We already have data!");
            }
        }
    }
}