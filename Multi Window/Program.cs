using System;
using ConsoleControllsLib;

class Program
{



    static void Main()
    {




        Window window = new Window() { FramePerSecond = 15 };
        window.SetWindowResizable(true);
        window.WindowSize = (80, 40);

        Screen screen = new Screen(window);

        List<Screen> screens = new List<Screen>();

        int ActiveScreenIndex = 0;

        screen.AddChild(

            new Table()
            {
                Position = (0.2, 0.1),
                Size = (0.8, 0.7),

                Childrens = {

                    new TextBox()
                    {
                        Position = (0.1, 0.1),
                        Size = (0.8, 0.1),
                        Text = "Введите ваш текст"
                    },

                    new InputField()
                    {
                        Position = (0.3 , 0.1),
                        Size = (0.8 , 0.3)
                    },

                    new Button()
                    {
                        Position = (0.8, 0.1),
                        Size = (0.3, 0.1),
                        Text = "Пред окно",
                        OnClick  = (object sender , EventArgs arg) =>
                        {
                            ActiveScreenIndex = (ActiveScreenIndex - 1) < 0 ? 0 : ActiveScreenIndex - 1;
                            window.ActiveScreen = screens[ActiveScreenIndex];
                        }
                    },

                    new Button()
                    {
                        Position = (0.8, 0.4),
                        Size = (0.2, 0.1),
                        Text = "Новое окно",

                         OnClick  = (object sender , EventArgs arg) =>
                         {
                            Screen screen1 = new Screen(window);

                             screen1.AddChild(
                                    new Table()
                                    {
                                        Size = (0.8 , 0.8),
                                        Position = (0.1 , 0.1),

                                        Childrens = {

                                            new TextBox()
                                            {
                                                Size = (0.5 , 0.5),
                                                Position = (0.1 , 0.1),
                                                Text ="Это новый экран"
                                            },

                                            new Button()
                                            {
                                                Position = (0.8, 0.1),
                                                Size = (0.3, 0.1),
                                                Text = "Пред окно",
                                                OnClick  = (object sender , EventArgs arg) =>
                                                {
                                                    ActiveScreenIndex = (ActiveScreenIndex - 1) < 0 ? 0 : ActiveScreenIndex - 1;
                                                    window.ActiveScreen = screens[ActiveScreenIndex];
                                                }
                                            },
                                            
                                            new Button()
                                            {
                                                Position = (0.8, 0.5),
                                                Size = (0.3, 0.1),
                                                Text = "Удалить",
                                                OnClick  = (object sender , EventArgs arg) =>
                                                {
                                                    screens.RemoveAt(1);
                                                    ActiveScreenIndex = 0;
                                                    window.ActiveScreen = screens[ActiveScreenIndex];

                                                }
                                            },

                                        }
                                    }
                                 );

                             screen1.Init();

                             
                             screens.Add(screen1);
                             ActiveScreenIndex  = screens.Count- 1;

                             window.ActiveScreen = screens[ActiveScreenIndex];
                         
                         }
                    },

                    new Button()
                    {
                        Position = (0.8, 0.6),
                        Size = (0.3, 0.1),
                        Text = "След окно",
                         OnClick  = (object sender , EventArgs arg) =>
                        {
                            ActiveScreenIndex = (ActiveScreenIndex + 1) % screens.Count;
                            window.ActiveScreen = screens[ActiveScreenIndex];
                        }
                    },

                }
            }

       );

        ((IParent)screen).Init();

        screens.Add(screen);

        window.ActiveScreen = screens[ActiveScreenIndex];

        window.Run();

    }

}