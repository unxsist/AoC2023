using System.Text.RegularExpressions;

namespace MyApp
{
    class Game
    {
        public int ID;
        public int highestBlue = 0;
        public int highestRed = 0;
        public int highestGreen = 0;
        public int minimumBlue = 0;
        public int minimumRed = 0;
        public int minimumGreen = 0;

        public Game(string rawData)
        {
            Regex r_gameId = new Regex("Game ([0-9]*):.*", RegexOptions.Compiled);
            Regex r_green = new Regex("([0-9]*).green", RegexOptions.Compiled);
            Regex r_red = new Regex("([0-9]*).red", RegexOptions.Compiled);
            Regex r_blue = new Regex("([0-9]*).blue", RegexOptions.Compiled);
            
            this.ID = int.Parse(r_gameId.Match(rawData).Groups[1].Value);

            var yields = rawData
                .Replace($"Game {this.ID}:", "")
                .Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var yield in yields)
            {
                if (r_green.IsMatch(yield))
                {
                    int green = int.Parse(r_green.Match(yield).Groups[1].Value);
                    
                    this.highestGreen = green > this.highestGreen ? green : this.highestGreen;
                    this.minimumGreen = (this.minimumGreen < green) ? green : this.minimumGreen;
                } 
                
                if (r_blue.IsMatch(yield))
                {
                    int blue = int.Parse(r_blue.Match(yield).Groups[1].Value);
                    
                    this.highestBlue = blue > this.highestBlue ? blue : this.highestBlue;
                    this.minimumBlue = (this.minimumBlue < blue) ? blue : this.minimumBlue;
                } 
                
                if (r_red.IsMatch(yield))
                {
                    int red = int.Parse(r_red.Match(yield).Groups[1].Value);
                    
                    this.highestRed = red > this.highestRed ? red : this.highestRed;
                    this.minimumRed = (this.minimumRed < red) ? red : this.minimumRed;
                } 
            }
        }
    }
    
    internal class Program
    {
        const int RED_CUBES = 12;
        const int GREEN_CUBES = 13;
        const int BLUE_CUBES = 14;
        
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }
    
        static List<Game> GetGames()
        {
            List<Game> games = new List<Game>();
    
            String[] gameData = File.ReadAllLines("Input.txt");

            foreach (var game in gameData)
            {
                games.Add(new Game(game));
            }

            return games;
        }

        static void Part1()
        {
            var sum = 0; 
            
            foreach (var game in GetGames()) 
            {
                if (game.highestRed <= RED_CUBES && game.highestGreen <= GREEN_CUBES && game.highestBlue <= BLUE_CUBES)
                {
                    sum += game.ID;
                }    
            }
            
            Console.WriteLine($"Sum of possible games: {sum}");
        }

        static void Part2()
        {
            var sum = 0;

            foreach (var game in GetGames())
            {
                sum += (game.minimumRed * game.minimumBlue * game.minimumGreen);
            }
            
            Console.WriteLine($"Sum of power of minimum cubes: {sum}");
        }
    }
}