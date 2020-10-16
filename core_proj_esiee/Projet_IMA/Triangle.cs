using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Triangle : Parallelogram
    {
        public Triangle(Coord3D a, Coord3D b, Coord3D c, float u, float v, Couleur shapeColor) : base(a, b, c, u, v, shapeColor)
        {
        }
    }
}
