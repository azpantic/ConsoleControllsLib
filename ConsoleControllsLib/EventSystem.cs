using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ConsoleControllsLib
{
    static public class EventSystem
    {

        static public void Update()
        {

            if (ActiveScreen != Window._ActiveScreen)
            {

                if (Window._ActiveScreen.Interactables.Count != 0)
                {

                    if (ActiveScreen != null)
                        ActiveScreen.Interactables[ActiveInteractableIndex].IsSelected = false;

                    ActiveScreen = Window._ActiveScreen;
                    ActiveInteractableIndex = 0;
                    ActiveScreen.Interactables[ActiveInteractableIndex].IsSelected = true;
                }
            }

            var key = Console.ReadKey();

            pressedKey = (key.Key, key.Modifiers);

            if (pressedKey.Key == ConsoleKey.Tab)
            {
                ActiveScreen.Interactables[ActiveInteractableIndex].IsSelected = false;
                ActiveInteractableIndex = (ActiveInteractableIndex + 1) % ActiveScreen.Interactables.Count;
                ActiveScreen.Interactables[ActiveInteractableIndex].IsSelected = true;
            }

            if (ActiveScreen != null && ActiveScreen.Interactables.Count != 0)
                ActiveScreen.Interactables[ActiveInteractableIndex].Interact();

        }

        static public (ConsoleKey Key, ConsoleModifiers Modifier) PressedKeyInfo => pressedKey;


        static private (ConsoleKey Key, ConsoleModifiers Modifier) pressedKey;

        static public void AddInteractable(IInteractable interactable) => Interactables.Add(interactable);


        static private List<IInteractable> Interactables = new List<IInteractable>();
        static private int ActiveInteractableIndex = 0;


        static private Screen ActiveScreen = null;

    }

}
