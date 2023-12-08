namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        static void Part1()
        {
            var lines = File.ReadAllLines("Input.txt");
            Console.WriteLine("Sum of part no's: " + SumPartNumbers(lines));
        }

        static void Part2()
        {
            var lines = File.ReadAllLines("Input.txt");
            Console.WriteLine("Sum of gear ratios: " + SumGearRatios(lines));
        }
        
        static int SumPartNumbers(string[] schematic)
        {
            HashSet<string> countedNumbers = new HashSet<string>();
            int sum = 0;

            for (int y = 0; y < schematic.Length; y++)
            {
                for (int x = 0; x < schematic[y].Length; x++)
                {
                    if (IsSymbol(schematic[y][x]))
                    {
                        sum += SumAdjacentNumbers(schematic, x, y, countedNumbers);
                    }
                }
            }

            return sum;
        }

        static int SumAdjacentNumbers(string[] schematic, int x, int y, HashSet<string> countedNumbers)
        {
            int sum = 0;
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dy == 0 && dx == 0) continue; // Skip the symbol itself

                    int adjY = y + dy;
                    int adjX = x + dx;

                    if (adjY >= 0 && adjY < schematic.Length && adjX >= 0 && adjX < schematic[adjY].Length)
                    {
                        if (char.IsDigit(schematic[adjY][adjX]) && !countedNumbers.Contains($"{adjY},{adjX}"))
                        {
                            sum += ExtractAndMarkNumber(schematic, adjX, adjY, countedNumbers);
                        }
                    }
                }
            }
            return sum;
        }

        static int ExtractAndMarkNumber(string[] schematic, int x, int y, HashSet<string> countedNumbers)
        {
            string number = "";
            int initialX = x;
            while (x < schematic[y].Length && char.IsDigit(schematic[y][x]))
            {
                number += schematic[y][x];
                countedNumbers.Add($"{y},{x}");
                x++;
            }

            // Check if the number extends to the left of the initial digit
            x = initialX - 1;
            while (x >= 0 && char.IsDigit(schematic[y][x]))
            {
                number = schematic[y][x] + number;
                countedNumbers.Add($"{y},{x}");
                x--;
            }

            return int.TryParse(number, out int result) ? result : 0;
        }
        
        static int SumGearRatios(string[] schematic)
        {
            int sum = 0;
            HashSet<string> countedNumbers = new HashSet<string>();

            for (int y = 0; y < schematic.Length; y++)
            {
                for (int x = 0; x < schematic[y].Length; x++)
                {
                    if (schematic[y][x] == '*')
                    {
                        var adjacentNumbers = FindAdjacentNumbers(schematic, x, y, countedNumbers);
                        if (adjacentNumbers.Count == 2)
                        {
                            sum += adjacentNumbers[0] * adjacentNumbers[1];
                        }
                    }
                }
            }

            return sum;
        }

        static List<int> FindAdjacentNumbers(string[] schematic, int x, int y, HashSet<string> countedNumbers)
        {
            List<int> numbers = new List<int>();
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dy == 0 && dx == 0) continue;

                    int adjY = y + dy;
                    int adjX = x + dx;

                    if (adjY >= 0 && adjY < schematic.Length && adjX >= 0 && adjX < schematic[adjY].Length)
                    {
                        if (char.IsDigit(schematic[adjY][adjX]) && !countedNumbers.Contains($"{adjY},{adjX}"))
                        {
                            numbers.Add(ExtractAndMarkNumber(schematic, adjX, adjY, countedNumbers));
                        }
                    }
                }
            }
            return numbers;
        }

        static bool IsSymbol(char ch)
        {
            return !char.IsDigit(ch) && ch != '.';
        }
    }
}