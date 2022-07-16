using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public interface IParent : IElement
    {

        public void Init()
        {
            foreach (var child in Childrens)
            {
                child.parent = this;

                if (child is IParent)
                    ((IParent)child).Init();

                if (child is IInteractable)
                {
                    Interactables.Add((IInteractable)child);
                    ((IInteractable)child).Init();
                }

            }

        }

        public void AddChild(IChild child)
        {
            Childrens.Add(child);
            child.parent = this;

            if (child is IInteractable)
                Interactables.Add((IInteractable)child);
        }

        public List<IChild> Childrens { get; set; }

        public List<IInteractable> Interactables { get; set; }

        public List<IInteractable> GetInteractables()
        {
            List<IInteractable> interactables = new List<IInteractable>(Interactables);

            foreach (IParent item in Childrens.Where(i => i is IParent))
                interactables = interactables.Concat(item.GetInteractables()).ToList();

            return interactables;

        }

    }
}
