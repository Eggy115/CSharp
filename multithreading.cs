using System;
using System.Threading;

class Program {
    static void Main(string[] args) {
        Thread t = new Thread(new ThreadStart(DoWork));
        t.Start();
        t.Join();
        Console.WriteLine("Finished");
    }

    static void DoWork() {
        for (int i = 0; i < 10; i++) {
            Console.WriteLine("Working...");
            Thread.Sleep(1000);
        }
    }
}
