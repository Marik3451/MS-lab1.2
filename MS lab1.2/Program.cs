using System;
using System.Collections.Generic;

namespace DiscreteRandomSequenceGenerator
{
    class Program
    {
        static (List<int>, Dictionary<int, double>) DiscreteRandomSequence(List<int> values, List<double> probability, int m)
        {
            List<int> realizations = new List<int>();
            List<double> cumulativeProbabilities = new List<double>();
            Dictionary<int, int> dictOfRealizations = new Dictionary<int, int>();

            foreach (double prob in probability)
            {
                double cumulativeProb = cumulativeProbabilities.Count > 0
                    ? cumulativeProbabilities[cumulativeProbabilities.Count - 1] + prob
                    : prob;

                cumulativeProbabilities.Add(cumulativeProb);
            }

            foreach (int value in values)
            {
                dictOfRealizations.Add(value, 0);
            }

            Random random = new Random();

            for (int i = 0; i < m; i++)
            {
                double randomNumber = random.NextDouble();
                int realization = 0;

                for (int j = 0; j < cumulativeProbabilities.Count; j++)
                {
                    if (randomNumber <= cumulativeProbabilities[j])
                    {
                        realization = values[j];
                        break;
                    }
                }

                dictOfRealizations[realization]++;
                realizations.Add(realization);
            }

            Dictionary<int, double> normalizedDict = new Dictionary<int, double>();
            int sumOfAll = realizations.Count;

            foreach (var pair in dictOfRealizations)
            {
                normalizedDict.Add(pair.Key, (double)pair.Value / sumOfAll);
            }

            return (realizations, normalizedDict);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Завдання №2. Варіант - 10");
            Console.WriteLine();

            List<int> values = new List<int> { 3, 6, 9 };
            List<double> probability = new List<double> { 0.25, 0.35, 0.4 };

            Console.Write("Введіть значення M - кількість реалізацій: ");
            int m = int.Parse(Console.ReadLine());

            var task2 = DiscreteRandomSequence(values, probability, m);
            List<int> seq2 = task2.Item1;
            Dictionary<int, double> dict2 = task2.Item2;

            Console.WriteLine("Послідовність реалізацій:");
            foreach (int realization in seq2)
            {
                Console.WriteLine(realization);
            }

            Console.WriteLine("Cловник з кількістю здійснених реалізацій:");
            foreach (var pair in dict2)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
