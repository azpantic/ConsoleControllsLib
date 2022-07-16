using System;
using ConsoleControllsLib;

namespace Hospital
{
    internal class Program
    {

        


        
        static void Main(string[] args)
        {
            Window window = new Window();
            window.SetWindowResizable(true);
            window.WindowSize = (100, 50);

            App app = new App();

            

            Window._ActiveScreen = app.AuthScreen;

            window.Run();

        }
    }
}