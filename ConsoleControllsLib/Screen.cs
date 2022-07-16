using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public class Screen : IParent  
    {

       

        public Screen(Window parentWindow)
        {
            this.parentWindow = parentWindow;
            buffer = new char[parentWindow.WindowSize.height - 1, parentWindow.WindowSize.width];
        }

        char[,] buffer;
        private Window parentWindow;

        public void Update()
        {

            // Clear Buffer
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = ' ';
                }
            }

            foreach (var children in Childrens)
            {
                children.Update();
                (int X, int Y) position = ((int)(children.Position.X * SizeInPixel.Height), (int)(children.Position.Y * SizeInPixel.Width));
                Window.MergeBuffers(buffer, children.GetVisualBuffer(), position);
            }

            // Print
            string temp = "";

            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                    temp += buffer[i, j];
            }

            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.Write(temp);


        }

        #region IParent

        public void Init()
        {
            
            foreach (var child in Childrens)
            {
                child.parent = this;

                if (child is IParent)
                {
                    ((IParent)child).Init();
                    Interactables = Interactables.Concat(((IParent)child).GetInteractables()).ToList();
                }


                if (child is IInteractable)
                {
                    Interactables.Add((IInteractable)child);
                    
                    ((IInteractable)child).Init();
                }

            }


        }

        public List<IChild> Childrens { get; set; } = new List<IChild>();

        public List<IInteractable> Interactables { get; set; } = new List<IInteractable>();

        public void AddChild(IChild child)
        {
            Childrens.Add(child);
            child.parent = this;
        }

        #endregion

        #region IElement

        public (double X, double Y) Position { get; set; }

        public (int Width, int Height) SizeInPixel { get => parentWindow.WindowSize; }

        public (double Width, double Height) Size { get; set; }

        public object Data { get; set; } = null;

        #endregion

    }
}
