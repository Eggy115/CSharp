using System;

class Program {
    static void Main(string[] args) {
        Box<int> intBox = new Box<int>(10);
        Console.WriteLine(intBox.Content);
        Box<string> stringBox = new Box<string>("Hello, world!");
        Console.WriteLine(stringBox.Content);
    }
}

class Box<T> {
    public T Content { get; set; }
    public Box(T content) {
        Content = content;
    }
}
