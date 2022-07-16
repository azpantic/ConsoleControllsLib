using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public interface IInteractable : IChild
    {
        public void Interact();

        public void Init()
        {
            EventSystem.AddInteractable(this);
        }

        public bool IsSelected { get; set; }

    }
}
