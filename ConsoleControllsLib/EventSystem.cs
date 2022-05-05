using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    static public class EventSystem
    {

        static public void Update()
        {
            foreach(var item in Updatables)
                item.Update(); 


        }

        static internal void AddUpdatable(Updatable newUpdatable) => Updatables.Add(newUpdatable);


        static private List<Updatable> Updatables = new List<Updatable>();

    }

    internal class Updatable
    {

        public Updatable()
        {
            EventSystem.AddUpdatable(this);
        }

        virtual public void Update() { }

    }
}
