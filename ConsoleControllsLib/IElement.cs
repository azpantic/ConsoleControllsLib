using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControllsLib
{
    public interface IElement
    {
        public (double X ,double Y) Position { get; set; }

        public (int Width, int Height) SizeInPixel { get; }

        public (double Width, double Height) Size { get; set; }

        public object Data { get; set; }

    }
}
