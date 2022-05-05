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
        static public bool IsExist { get; private set; } = false;

        public Window()
        {
            IsExist = true;
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
        public void SetWindowSize(int WindowHeight, int WindowWidth)
        {
            Console.WindowHeight = WindowHeight;
            Console.WindowWidth = WindowWidth;
        }

        public void Run()
        {
            if (activeScreen == null)
                throw new Exception("Active screen was null");
            


            EventSystem.Update();
            activeScreen?.Update();          
        }

        public Screen activeScreen { get; set; }


    }

    struct WindowSettings
    {

        public bool isResizable { get; set; }

        public (int, int) WindowSize { get; set; }


    }

}