using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public class Window
    {


        static public Window instanse;

        public static Screen? _ActiveScreen { get => instanse.ActiveScreen; set => instanse.ActiveScreen = value; }

        public Window()
        {
            instanse = this;
            Console.CursorVisible = false;
        }

        #region MakeWindowUnresizable 

        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public void SetWindowResizable(bool state)
        {

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
        }

        #endregion

        /// <summary>
        /// Set Window size in char
        /// </summary>
        /// <param name="WindowHeight"> Height in char </param>
        /// <param name="WindowWidth"> Width in char </param>

        public (int width, int height) WindowSize
        {
            get
            {
                return (Console.BufferWidth, Console.BufferHeight);
            }

            set
            {
                Console.WindowHeight = value.height;
                Console.BufferHeight = value.height;
                Console.WindowWidth = value.width;
                Console.BufferWidth = value.width;
            }

        }
        public int FramePerSecond { get; set; }


        public void Run()
        {
            

            while (true)
            {
                Update();
                EventSystem.Update();
            };

        }

        private void Update()
        {

            if (ActiveScreen == null)
                throw new Exception("Active screen was null");

            ActiveScreen.Update();

        }

        public Screen? ActiveScreen { get; set; } = null;

        #region SomeHelpFunc

        static public void MergeBuffers(char[,] main, char[,] target, (int X, int Y) position)
        {

            for (int mainX = position.X < 0 ? 0  : position.X, targetX = 0; mainX < main.GetLength(0) && targetX < target.GetLength(0); mainX++, targetX++)
            {
                for (int mainY = position.Y < 0 ? 0 : position.Y, targetY = 0; mainY < main.GetLength(1) && targetY < target.GetLength(1); mainY++, targetY++)
                {
                    main[mainX, mainY] = target[targetX, targetY];
                }
            }
        }

        #endregion
    }

}