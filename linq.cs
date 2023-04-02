using System;
using System.Linq;

class Program {
    static void Main(string[] args) {
        int[] numbers = { 1, 2, 3, 4, 5 };
        var result = from n in numbers
                     where n % 2 == 0
                     select n;
        foreach (int n in result) {
            Console.WriteLine(n);
        }
    }
}
