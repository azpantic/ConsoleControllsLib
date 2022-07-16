using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public class PasswordBox : InputField
    {
        public override void  Interact()
        {

            if (Char.IsLetterOrDigit(((char)EventSystem.PressedKeyInfo.Key)))
                PassString += (char)EventSystem.PressedKeyInfo.Key;

            if (EventSystem.PressedKeyInfo.Key == ConsoleKey.Spacebar)
                PassString += " ";


            if (EventSystem.PressedKeyInfo.Key == ConsoleKey.Backspace && Text.Length > 0)
            {
                PassString = PassString[..^1];

            }

            Text = new string('*', PassString.Length);

        }

        public string PassString { get; set; } = "";

    }
}
