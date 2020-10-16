using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelogram : AbstractShape
    {
        protected V3 A;
        protected V3 B;
        protected V3 C;

        //int xA, yA, xB, yB, xC, yC;

        public Parallelogram(V3 a, V3 b, V3 c, Couleur shapeColor) : base(shapeColor)
        {
            A = a;
            /*this.xA = (int)a.X;
            this.yA = (int)a.Z;
            this.xB = (int)b.X;
            this.yB = (int)b.Z;
            this.xC = (int)c.X;
            this.yC = (int)c.Z;*/
            B = b;
            C = c;
        }

        public V3 paraPoint(float u, float v)
        {
            V3 AB = B - A;
            V3 AC = C - A;
            V3 pt = new V3(A + u * AB + v * AC);
            return pt;
        }

        public new void draw()
        {
            for (float u = 0; u <= 1; u += 0.002f)
            {
                for (float v = 0; v <= 1; v += 0.002f)
                {
                    V3 pt = paraPoint(u, v);
                    BitmapEcran.DrawPixel((int)pt.X, (int)pt.Z, this.ShapeColor);
                }
            }
        }

        public V3 getIntersection()
        {
            throw new NotImplementedException();
        }
    }
}
