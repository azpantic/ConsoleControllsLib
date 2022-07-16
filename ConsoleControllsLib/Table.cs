using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public class Table : IParent, IChild
    {

        #region IChild

        public char[,] GetVisualBuffer()
        {
            int height = Convert.ToInt32(Size.Height * parent.SizeInPixel.Height);
            int width = Convert.ToInt32(Size.Width * parent.SizeInPixel.Width);

            char[,] buffer = new char[height, width];



            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = ' ';
                }
            }

            buffer[0, 0] = '┌';
            buffer[height - 1, 0] = '└';
            buffer[0, width - 1] = '┐';
            buffer[height - 1, width - 1] = '┘';

            for (int i = 1; i < height - 1; i++)
                buffer[i, 0] = buffer[i, width - 1] = '│';

            for (int i = 1; i < width - 1; i++)
                buffer[0, i] = buffer[height - 1, i] = '─';




            foreach (var children in Childrens)
            {

                (int X, int Y) position = ((int)(children.Position.X * SizeInPixel.Height), (int)(children.Position.Y * SizeInPixel.Width));
                Window.MergeBuffers(buffer, children.GetVisualBuffer(), position);
            }

            return buffer;
        }

        public IParent parent { get; set; }

        public void Update()
        {
           
        }


        #endregion


        #region IParent

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

        public (int Width, int Height) SizeInPixel { get => ((int)(parent.SizeInPixel.Width * Size.Width), (int)(parent.SizeInPixel.Height * Size.Height)); }

        public (double Width, double Height) Size { get; set; }

        public object Data { get; set; } = null;

        #endregion

    }
}
