using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Triangle : Parallelogram
    {
        public Triangle(V3 a, V3 b, V3 c, Couleur shapeColor, Texture texture = null) : base(a, b, c, shapeColor, texture)
        {
        }

        public V3 ParaPoint(float u, float v)
        {
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            V3 pt = new V3(PointA + u * AB + v * AC);
            return pt;
        }

        public override V3 GetIntersection(V3 positionCamera, V3 dirRayon)
        {
            V3 intersection = base.GetIntersection(positionCamera, dirRayon);
            return (intersection == null || U > 1 - V) ? null : intersection;
        }
    }
}
