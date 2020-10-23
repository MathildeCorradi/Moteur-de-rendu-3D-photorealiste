using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Triangle : Parallelogram
    {
        public Triangle(V3 a, V3 b, V3 c, Couleur shapeColor) : base(a, b, c, shapeColor)
        {
        }

        public V3 paraPoint(float u, float v)
        {
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            V3 pt = new V3(PointA + u * AB + v * AC);
            return pt;
        }

        public override void Draw()
        {
            for (float u = 0; u <= 1; u += 0.001f)
            {
                for (float v = 0; v <= 1; v += 0.001f)
                {
                    if (u <= 1 - v)
                    {
                        V3 pt = paraPoint(u, v);
                        BitmapEcran.DrawPixel((int)pt.X, (int)pt.Z, this.ShapeColor);
                    }
                    else break;
                }
            }
        }

        public override V3 GetIntersection(V3 positionCamera, V3 dirRayon)
        {
            V3 intersection = base.GetIntersection(positionCamera, dirRayon);
            return (intersection == null || U > 1 - V)?  null : intersection;
        }
    }
}
