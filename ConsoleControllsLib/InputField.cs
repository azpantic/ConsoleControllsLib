using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public class InputField : TextBox, IInteractable
    {
        public virtual void Interact()
        {



            if (Char.IsLetterOrDigit(((char)EventSystem.PressedKeyInfo.Key)))
                Text += (char)EventSystem.PressedKeyInfo.Key;

            if (EventSystem.PressedKeyInfo.Key == ConsoleKey.Spacebar)
                Text += " ";


            if (EventSystem.PressedKeyInfo.Key == ConsoleKey.Backspace && Text.Length > 0)
                Text = Text[..^1];



        }

        public bool IsSelected { get; set; }

        public new char[,] GetVisualBuffer()
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

            int strIndex = 0;


            if (IsSelected)
            {

                if (HaveBorder)
                {
                    buffer[0, 0] = '╔';
                    buffer[height - 1, 0] = '╚';
                    buffer[0, width - 1] = '╗';
                    buffer[height - 1, width - 1] = '╝';

                    for (int i = 1; i < height - 1; i++)
                        buffer[i, 0] = buffer[i, width - 1] = '║';

                    for (int i = 1; i < width - 1; i++)
                        buffer[0, i] = buffer[height - 1, i] = '═';


                }


                for (int i = 1; i < buffer.GetLength(0) - 1 && strIndex < Text.Length; i++)
                {
                    for (int j = 1; j < buffer.GetLength(1) - 1 && strIndex < Text.Length; j++)
                    {
                        buffer[i, j] = Text[strIndex++];
                    }
                }



                return buffer;
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

            for (int i = 1; i < buffer.GetLength(0) - 1 && strIndex < Text.Length; i++)
            {
                for (int j = 1; j < buffer.GetLength(1) - 1 && strIndex < Text.Length; j++)
                {
                    buffer[i, j] = Text[strIndex++];
                }
            }


            return buffer;



        }

    }
}
