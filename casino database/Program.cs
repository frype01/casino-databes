using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace casino_database
{
    internal class Program
    {

        static void PrintTableCasino(Dictionary<string, Casino> data)
        {
            var str = $"|Game name\t|Player name\t|\n";

            foreach (var casino in data.Values)
            {
                str += $"|{casino.Game_name}\t|{casino.Player_name}\t|\n";
            }
            Console.WriteLine(str);
        }

        static void PrintTablePlayers(Dictionary<string, Players> data)
        {
            var str = $"|Name\t|Age\t|Balance\t|Bet amount\t|Game\t|\n";

            foreach (var playes in data.Values)
            {
                str += $"|{playes.Name}\t|{playes.Age}\t|{playes.Balance}\t|{playes.Bet_amount}\t|{playes.Game}\t|\n";
            }
            Console.WriteLine(str);
        }

        static void PrintTableGames(Dictionary<string, Games> data)
        {
            var str = $"|Name\t|Rate\t|\n";

            foreach (var games in data.Values)
            {
                str += $"|{games.Name}\t|{games.Rate}\t|\n";
            }
            Console.WriteLine(str);
        }

        static void Main(string[] args)
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = "AIzaSyCbxRBdgTsGyG2_Tt1PWzDIJg6QT1XlLvA",
                BasePath = "https://casion-database-default-rtdb.europe-west1.firebasedatabase.app/"
            };
            IFirebaseClient client = new FirebaseClient(config);
            while (true)
            {
                Console.WriteLine("1.Ввести данні\n2.Вивести данні");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        {
                            Console.WriteLine("1.Казино\n2.Гравці\n2.Ігри");
                            switch (int.Parse(Console.ReadLine()))
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Введіть через ентер гру та ім'я гравця");
                                        Casino casino = new Casino()
                                        {
                                            Game_name = Console.ReadLine(),
                                            Player_name = Console.ReadLine()
                                        };
                                        client.Set(@"Casino/" + casino.Game_name, casino);
                                    }
                                    break;
                                case 2:
                                    {
                                        Console.WriteLine("Введіть через ентер ім'я гравця, вік, баланс, сумму ставки та гру");
                                        Players players = new Players()
                                        {
                                            Name = Console.ReadLine(),
                                            Age = int.Parse(Console.ReadLine()),
                                            Balance = int.Parse(Console.ReadLine()),
                                            Bet_amount = int.Parse(Console.ReadLine()),
                                            Game = Console.ReadLine()
                                        };
                                        client.Set(@"Players/" + players.Name, players);
                                    }
                                    break;
                                case 3:
                                    {
                                        Console.WriteLine("Введіть через ентер назву гри та вашу ставку");
                                        Games games = new Games()
                                        {
                                            Name = Console.ReadLine(),
                                            Rate = int.Parse(Console.ReadLine())
                                        };
                                        client.Set(@"Games/" + games.Name, games);
                                    }
                                    break;
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("1.Казино\n2.Гравці\n2.Ігри");
                            switch (int.Parse(Console.ReadLine()))
                            {
                                case 1:
                                    {
                                        FirebaseResponse res = client.Get(@"Casino/");
                                        Dictionary<string, Casino> casinoData = JsonConvert.DeserializeObject<Dictionary<string, Casino>>(res.Body.ToString());
                                        PrintTableCasino(casinoData);
                                    }
                                    break;
                                case 2:
                                    {
                                        FirebaseResponse res = client.Get(@"Players/");
                                        Dictionary<string, Players> playersData = JsonConvert.DeserializeObject<Dictionary<string, Players>>(res.Body.ToString());
                                        PrintTablePlayers(playersData);
                                    }
                                    break;
                                case 3:
                                    {
                                        FirebaseResponse res = client.Get(@"Games/");
                                        Dictionary<string, Games> gamesData = JsonConvert.DeserializeObject<Dictionary<string, Games>>(res.Body.ToString());
                                        PrintTableGames(gamesData);
                                    }
                                    break;
                            };
                        }
                        break;
                }
            }
        }
    }
}
