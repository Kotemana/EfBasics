using EfBasics.Data;
using EfBasics.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EfBasics;

class SuperProgramm
{
    public static void Main()
    {
        //AddTolchki();
        //DeleteTolchki();
        //UpdateTolch("Yolsa", new Weapon() { Name = "Saber", Material = Material.Dural, Description = "Old dural saber"});
        ReadTolchki();
        //AddGame();
        //ReadGames();
        //DeleteById(3);
        //ShowTolchJson();
    }

    private static void DeleteById(int id)
    {
        using var context = new LarpPlayersDbContext();
        var toDelete = context.LarpPlayers.Find(id);
        context.LarpPlayers.Remove(toDelete);
        context.SaveChanges();
    }

    private static void ShowTolchJson()
    {
        using var context = new LarpPlayersDbContext();
        var tolchki = context.LarpPlayers.ToList();
        var options = new JsonSerializerOptions()
        {
            Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                },
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(tolchki, options);
        Console.WriteLine(json);
    }

    private static void UpdateTolch(string name, Weapon weapon)
    {
        using var context = new LarpPlayersDbContext();
        var tolch = context.LarpPlayers.FirstOrDefault(x => x.LarpName == name);
        if (tolch != null)
        {
            tolch.Weapons.Add(weapon);
            context.SaveChanges();
        }
    }

    private static void ReadGames()
    {
        using var context = new LarpPlayersDbContext();
        var games = context.Games;//.Include(x=>x.LarpPlayers);
        foreach (var game in games.ToList())
        {
            Console.WriteLine(game.Name);
            Console.WriteLine("Players:");
            foreach (var tolch in game.LarpPlayers)
            {
                Console.WriteLine($"{tolch.FirstName} '{tolch.LarpName}' {tolch.LastName}");
            }
        }

    }

    private static void AddGame()
    {
        using var context = new LarpPlayersDbContext();
        var players = context.LarpPlayers.ToList();
        var game = new Game() { Name = "Witcher", LarpPlayers = players };
        context.Games.Add(game);
        context.SaveChanges();
    }

    public static void DeleteTolchki()
    {
        using (var context = new LarpPlayersDbContext())
        {
            var duplicates = context
                .LarpPlayers
                .ToList()
                .GroupBy(s => new { s.LarpName })
                .SelectMany(grp => grp.Skip(1));
            foreach (var clone in duplicates)
            {
                context.LarpPlayers.Remove(clone);
            }
            context.SaveChanges();
        }
    }
    public static void AddTolchki()
    {
        using (var context = new LarpPlayersDbContext())
        {
            context.Database.EnsureCreated();
            var LarpPlayer = new LarpPlayer()
            {
                FirstName = "Tatsiana",
                LastName = "Dubouskaya",
                LarpName = "Prorochka"
            };
            context.LarpPlayers.Add(LarpPlayer);
            context.SaveChanges();
        }
    }

    public static void ReadTolchki()
    {
        using (var context = new LarpPlayersDbContext())
        {
            var tolchki = context
                .LarpPlayers;
            //.Include(x=>x.Games).Include(x=>x.Weapons); //eager load

            var tolchkiNotDexter = tolchki.Where(x => x.LarpName!="Dexter").ToList();
            for (int i = 1; i <= tolchkiNotDexter.Count; i++)
            {
                Console.WriteLine($"{i} - {tolchkiNotDexter[i-1].LarpName}");
            }
            var answer = int.Parse(Console.ReadLine());
            Console.WriteLine($"{tolchkiNotDexter[answer-1].LarpName} has {tolchkiNotDexter[answer-1].Weapons.Count} weapons");
            //foreach (var tolch in tolchkiNotDexter)
            //{
            //    Console.WriteLine(tolch.LarpName);

            //    foreach (var weapon in tolch.Weapons)
            //    {
            //        Console.WriteLine(weapon.Name);
            //    }

            //}


        }
    }
}
