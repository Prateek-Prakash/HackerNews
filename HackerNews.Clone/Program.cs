using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Clone.API;
using HackerNews.Database;
using HackerNews.Database.Tables;

namespace HackerNews.Clone
{
    class Program
    {
        static void Main(string[] args)
        {
            long maxID = Clone(1, true);

            Beep(5);

            SetConsoleColor(ConsoleColor.White);
            Console.WriteLine($"INFO: Finished At {maxID} - Press Enter To Exit");
            Console.ReadKey(true);
        }

        static long Clone(long startID, bool shouldUpdate)
        {
            List<long> itemsArray = new List<long>();

            if (!shouldUpdate)
            {
                using (HackerNewsDB hackerNewsDB = new HackerNewsDB())
                {
                    itemsArray = hackerNewsDB.Items.Where(X => X.ID >= startID).Select(X => X.ID).ToList();
                    SetConsoleColor(ConsoleColor.White);
                    Console.WriteLine($"INFO: Retrieved {itemsArray.Count} IDs");
                }
            }
            else
            {
                SetConsoleColor(ConsoleColor.White);
                Console.WriteLine($"INFO: Entries Will Be Updated");
            }

            Console.WriteLine($"INFO: Starting At {startID} - Press Enter To Continue");
            Console.ReadKey(true);

            long maxID = HackerNewsAPI.GetMaxID();

            ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 12 };
            Parallel.For(startID, maxID + 1, parallelOptions, itemID =>
            {
                bool itemRemoved = false;

                if (!itemsArray.Contains(itemID))
                {
                    using (HackerNewsDB hackerNewsDB = new HackerNewsDB())
                    {
                        try
                        {
                            if (shouldUpdate)
                            {
                                Item existingItem = hackerNewsDB.Items.FirstOrDefault(X => X.ID == itemID);
                                if (existingItem != null)
                                {
                                    hackerNewsDB.Items.Remove(existingItem);
                                    hackerNewsDB.SaveChanges();

                                    Item newItem = HackerNewsAPI.GetItem(itemID);
                                    hackerNewsDB.Items.Add(newItem);
                                    hackerNewsDB.SaveChanges();

                                    SetConsoleColor(ConsoleColor.Blue);
                                    Console.WriteLine($"DEBUG: Updated Item {itemID}");

                                    itemRemoved = true;
                                }
                            }
                            
                            if (!itemRemoved)
                            {
                                Item newItem = HackerNewsAPI.GetItem(itemID);
                                hackerNewsDB.Items.Add(newItem);
                                hackerNewsDB.SaveChanges();

                                SetConsoleColor(ConsoleColor.Green);
                                Console.WriteLine($"DEBUG: Added Item {itemID}");
                            }
                        }
                        catch
                        {
                            SetConsoleColor(ConsoleColor.Red);
                            Console.WriteLine($"ERROR: Failed Item {itemID}");
                        }
                    }
                }
                else
                {
                    SetConsoleColor(ConsoleColor.Yellow);
                    Console.WriteLine($"DEBUG: Skipped Item {itemID}");
                }
            });

            return maxID;
        }

        static void SetConsoleColor(ConsoleColor consoleColor)
        {
            if (Console.ForegroundColor != consoleColor)
            {
                Console.ForegroundColor = consoleColor;
            }
        }

        static void Beep(int beepCount)
        {
            for (int i = 0; i < beepCount; i++)
            {
                Console.Beep();
            }
        }
    }
}
