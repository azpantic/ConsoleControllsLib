using System;
using ConsoleControllsLib;

//class Program
//{
//    public delegate double MathDelegate(double value1, double value2);

//    public static double Add(double value1, double value2)
//    {
//        return value1 + value2;
//    }
//    public static double Subtract(double value1, double value2)
//    {
//        return value1 - value2;
//    }

//    public static void Main()
//    {
//        // лаьтерантивное объявление делигата
//        Func<int, int, int> _mathDelegate = (x, y) =>
//        {
//            return x + y;
//        };

//        // Не предполагает возвращаемого значения
//        Action<int, int> __mathDelegate = (x, y) =>
//        {
//            Console.WriteLine(x + y);
//        };




//        MathDelegate mathDelegate = Add;
//        var result = mathDelegate(5, 2); //7

//        mathDelegate = Subtract;
//        result = mathDelegate(5, 2); // 3
//    }
//}


class Program
{

    static void Main()
    {




        Window window = new Window() { FramePerSecond = 15 };
        window.SetWindowResizable(true);
        window.WindowSize = (100, 50);

        Screen screen = new Screen(window);
        Screen screen2 = new Screen(window);

        // 1 параметр - вертикальное перемещение
        // 2 параметр - горизонтальное

        screen.AddChild(new Table()
        {
            Position = (0.1, 0.1),
            Size = (0.8, 0.8),

            Childrens =
            {

                new PasswordBox()
                {
                    Position = (0.2, 0.2),
                    Size = (0.6 , 0.3)
                    
                },

                new Button()
                {
                    Position = (0.6, 0.2),
                    Size = (0.6 , 0.3) ,
                    Text = "Change Screen",

                    OnClick = (object sender , EventArgs args) =>
                    {
                        Button instance  = sender as Button;

                         window.ActiveScreen = screen2;

                    }

                }

            }
        });

        screen2.AddChild(new Table()
        {
            Position = (0.1, 0.1),
            Size = (0.8, 0.8),

            Childrens =
            {

                new TextBox()
                {
                    Position = (0.2, 0.2),
                    Size = (0.6 , 0.3) ,
                    Text = "Screen2"
                },

                new Button()
                {
                    Position = (0.6, 0.2),
                    Size = (0.6 , 0.3) ,
                    Text = "Change Screen",

                    OnClick = (object sender , EventArgs args) =>
                    {
                        Button instance  = sender as Button;

                         window.ActiveScreen = screen;

                    }

                }

            }
        });

       


        ((IParent)screen).Init();
        ((IParent)screen2).Init();

        window.ActiveScreen = screen;

        window.Run();

    }

}