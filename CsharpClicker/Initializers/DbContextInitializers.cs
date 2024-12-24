using CsharpClicker.Domain;
using CsharpClicker.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CsharpClicker.Initizlizers;

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

        AddBoostIfNotExist(Boost1, price: 1203, profit: 7);
        AddBoostIfNotExist(Boost2, price: 4514, profit: 15);
        AddBoostIfNotExist(Boost3, price: 9943, profit: 33);
        AddBoostIfNotExist(Boost4, price: 18056, profit: 56);
        AddBoostIfNotExist(Boost5, price: 35987, profit: 89);

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
