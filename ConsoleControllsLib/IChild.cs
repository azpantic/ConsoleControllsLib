using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public interface IChild : IElement
    {

        public IParent parent { get; set; }

        public char[,] GetVisualBuffer();

        public void Update();

      
    }
}
