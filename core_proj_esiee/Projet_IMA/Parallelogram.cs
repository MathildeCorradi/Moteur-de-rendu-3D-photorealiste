using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelogram : AbstractShape
    {
        Coord3D A;
        Coord3D B;
        Coord3D C;

        public Parallelogram(Coord3D a, Coord3D b, Coord3D c, float u, float v) : base(u, v)
        {
            A = a;
            B = b;
            C = c;
        }

        public Coord3D getIntersection()
        {
            throw new NotImplementedException();
        }
    }
}
