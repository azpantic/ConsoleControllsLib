using System;
using ConsoleControllsLib;

class Program
{

    static void Main()
    {




        Window window = new Window() { FramePerSecond = 15 };
        window.SetWindowResizable(true);
        window.WindowSize = (100, 50);

        Screen screen = new Screen(window);

        Table table = new Table();

        table = new Table()
        {

            Position = (0.25, 0.25),
            Size = (0.5, 0.5),

            Childrens = {
            new Button()
            {
                Position = (0.1, 0.45),
                Size = (0.1, 0.1),
                Text = "Up",
                OnClick = (object sender, EventArgs args) =>
                {
                    var t = table.Position;
                    t.X -= 0.05;

                    table.Position = t;
                }


            },
            new Button()
            {
                Position = (0.45, 0.1),
                Size = (0.1, 0.1),
                Text = "Left",
                OnClick = (object sender, EventArgs args) =>
                {
                    var t = table.Position;
                    t.Y -= 0.05;

                    table.Position = t;
                }

            },
            new Button()
            {
                Position = (0.45, 0.8),
                Size = (0.1, 0.1),
                Text = "Right",
                OnClick = (object sender, EventArgs args) =>
                {
                    var t = table.Position;
                    t.Y += 0.05;

                    table.Position = t;
                }

            },
            new Button()
            {
                Position = (0.8, 0.45),
                Size = (0.1, 0.1),
                Text = "Down",
                OnClick = (object sender, EventArgs args) =>
                {
                    var t = table.Position;
                    t.X += 0.05;

                    table.Position = t;
                }

            }
            }
        };

        screen.AddChild(table);


        ((IParent)screen).Init();
    

        window.ActiveScreen = screen;

        window.Run();

    }

}