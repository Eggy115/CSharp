using System;
using System.Windows;
using System.Windows.Controls;

class Program : Application {
    [STAThread]
    static void Main() {
        Program app = new Program();
        app.Run();
    }

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        MainWindow window = new MainWindow();
        window.Show();
    }
}

class MainWindow : Window {
    public MainWindow() {
        Title = "Hello, world!";
        Width = 400;
        Height = 300;
        Button button = new Button();
        button.Content = "Click me!";
        button.Click += Button_Click;
        Content = button;
    }

    void Button_Click(object sender, RoutedEventArgs e) {
        MessageBox.Show("Hello, world!");
    }
}
