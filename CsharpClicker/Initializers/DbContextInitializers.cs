using CsharpClicker.Domain;
using CsharpClicker.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CsharpClicker.Initizlizers
{
    public static class DbContextInitializers
    {
        public static void AddAppDbContext(IServiceCollection services)
        {
            var pathToDbFile = GetPathToDbFile();

            services
                .AddDbContext<AppDbContext>(options => options
                    .UseSqlite($"Data Source={pathToDbFile}"));

            string GetPathToDbFile()
            {
                var applicationFolder = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "CsSharpCLicker");

                if (!Directory.Exists(applicationFolder))
                {
                    Directory.CreateDirectory(applicationFolder);
                }

                return Path.Combine(applicationFolder, "CsSharpClicker.db");
            }
        }

        public static void InitializeDbContext(AppDbContext appDbContext)
        {
            const string Boost1 = "Негорящий";
            const string Boost2 = "Серебряное-кольцо-жадного-змея";
            const string Boost3 = "Щит-желания";
            const string Boost4 = "Посох-Побирушки";
            const string Boost5 = "Символ-алчности";

            appDbContext.Database.Migrate();

            var existingBoosts = appDbContext.Boosts
                .ToArray();

            AddBoostIfNotExist(Boost1, price: 10, profit: 500);
            AddBoostIfNotExist(Boost2, price: 9000, profit: 1100);
            AddBoostIfNotExist(Boost3, price: 13000, profit: 1900);
            AddBoostIfNotExist(Boost4, price: 15000, profit: 2200);
            AddBoostIfNotExist(Boost5, price: 20000, profit: 3000);

            appDbContext.SaveChanges();

            void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
            {
                if (!existingBoosts.Any(eb => eb.Title == name))
                {
                    var pathToImg = Path.Combine(".", "Resources", "BoostImages", $"{name}.png");
                    using var fileStream = File.OpenRead(pathToImg);
                    using var memoryStream = new MemoryStream();

                    fileStream.CopyTo(memoryStream);

                    appDbContext.Add(new Boost
                    {
                        Title = name,
                        Price = price,
                        Profit = profit,
                        IsAuto = isAuto,
                        Image = memoryStream.ToArray()
                    });
                }
            }
        }
    }
}
