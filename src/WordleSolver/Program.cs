using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordList = ReadWordList();
            // var mostUsed = SortMostUsedLetters(wordList);

            // Console.WriteLine("most used letters:");

            //foreach (var c in mostUsed)
            //{
            //    float percent = c.Value / (float)wordList.Length;
            //    Console.WriteLine($"{c.Key}: {percent}");
            //}

            // var word1 = FindWordContaining(wordList, new List<char>() { mostUsed[0].Key, mostUsed[1].Key, mostUsed[2].Key, mostUsed[3].Key, mostUsed[4].Key });

            // var word2 = FindWordContaining(wordList, new List<char>() { mostUsed[5].Key, mostUsed[6].Key, mostUsed[7].Key, mostUsed[8].Key, mostUsed[10].Key });

            Console.WriteLine("Enter what  you know (use ? for unknown letters):");
            var input = Console.ReadLine();

            var matches = wordList.Where(w => input[0] == '?' || input[0] == w[0])
                .Where(w => input[1] == '?' || input[1] == w[1])
                .Where(w => input[2] == '?' || input[2] == w[2])
                .Where(w => input[3] == '?' || input[3] == w[3])
                .Where(w => input[4] == '?' || input[4] == w[4]);

            matches = matches.ToList(); // force evaluation

            Console.WriteLine("Enter unmatched letters:");
            input = Console.ReadLine();

            foreach(char c in input)
                matches = matches.Where(w => w.Contains(c));

            matches = matches.ToList();

            Console.WriteLine("Enter letters known to not be in the word");
            input = Console.ReadLine();
            foreach (char c in input)
                matches = matches.Where(w => !w.Contains(c));

            matches = matches.ToList();

            Console.WriteLine("Matches:");
            foreach (var word in matches)
                Console.WriteLine(word);

        }

        private static string FindWordContaining(string[] words, List<char> letters)
        {
            foreach (var word in words)
            {
                bool possibleMatch = true;

                foreach (var letter in letters)
                {
                    if (!word.Contains(letter))
                    {
                        possibleMatch = false;
                        break;
                    }
                }

                if (!possibleMatch)
                    continue;

                return word;
            }

            return null;
        }

        private static string[] ReadWordList()
        {
            var d = Environment.CurrentDirectory;
            var words = File.ReadAllLines("wordlist.txt");
            return words;
        }

        private static List<KeyValuePair<char, int>> SortMostUsedLetters(string[] wordList)
        {
            List<KeyValuePair<char, int>> table = new List<KeyValuePair<char, int>>();

            // a = 97, z = 122
            for (int i = 97; i <= 122; i++)
            {
                var count = 0;
                foreach (string word in wordList)
                {
                    if (word.Contains((char)i))
                        count++;
                }
                table.Add(new((char)i, count));
            }

            var sorted = table.OrderByDescending(kvp => kvp.Value);
            return sorted.ToList();
        }
    }
}
