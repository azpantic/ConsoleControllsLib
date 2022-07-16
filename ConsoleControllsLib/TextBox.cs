using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public class TextBox : IChild 
    {

        
        public bool HaveBorder = true;

        public string Text { get; set; } = "";


        #region IChild

        public IParent parent { get; set; }


        public char[,] GetVisualBuffer()
        {
            Text = Text.Replace('\n', '\0');


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

            if (HaveBorder)
            {
                buffer[0, 0] = '┌';
                buffer[height - 1, 0] = '└';
                buffer[0, width - 1] = '┐';
                buffer[height - 1, width - 1] = '┘';

                for (int i = 1; i < height - 1; i++)
                    buffer[i, 0] = buffer[i, width - 1] = '│';

                for (int i = 1; i < width - 1; i++)
                    buffer[0, i] = buffer[height - 1, i] = '─';


            }

            int strIndex = 0;
            for (int i = 1; i < buffer.GetLength(0) - 1 && strIndex < Text.Length; i++)
            {
                for (int j = 1; j < buffer.GetLength(1) - 1 && strIndex < Text.Length; j++)
                {
                    buffer[i, j] = Text[strIndex++];
                }
            }


            return buffer;
        }

        public void Update()
        {

        }

        #endregion

        //#region IParent
        
        //public List<IChild> Childrens { get; set; } = new List<IChild>();
        //public void AddChild(IChild child)
        //{
        //    Childrens.Add(child);
        //    child.parent = this;
        //}

        //#endregion

        #region IElement

        public (double X, double Y) Position { get; set; }

        public (int Width, int Height) SizeInPixel { get => ((int)(parent.SizeInPixel.Width * Size.Width), (int)(parent.SizeInPixel.Height * Size.Height)); }

        public (double Width, double Height) Size { get; set; }

        public object Data { get; set; } = null;

        #endregion
    }
}
