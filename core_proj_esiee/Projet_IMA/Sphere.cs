using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere : AbstractShape
    {
        int radius;
        V3 center;

        int x, y;

        public Sphere(V3 center, int radius, Couleur shapeColor) : base(shapeColor)
        {
            center = new V3(center.X, center.Y, center.Z);
            this.radius = radius;
            this.x = (int)center.X;
            this.y = (int)center.Z;
        }

        public Sphere(float x, float y, float z, int radius, Couleur shapeColor) : base(shapeColor)
        {
            center = new V3(x, y, z);
            this.radius = radius;
            this.x = (int)x;
            this.y = (int)z;
        }

        public V3 getNormal(float u, float v)
        {

            return S(u,v); 
        }

        public V3 getIntersection()
        {
            throw new NotImplementedException();
        }

        public V3 S(float u, float v)
        {
            V3 pt = new V3();
            pt.X = IMA.Cosf(v) * IMA.Cosf(u);
            pt.Y = IMA.Cosf(v) * IMA.Sinf(u);
            pt.Z = IMA.Sinf(v);
            return pt;
        }

        public V3 spherePoint(float u, float v)
        {
            V3 pt = new V3(radius * S(u,v) + center);
            return pt;
        }

        public new void draw()
        {
            for(float u = 0; u <= IMA.DPI; u += 0.005f)
            {
                for(float v = -(IMA.PI2); v <= IMA.PI2; v += 0.005f)
                {
                    V3 pt = spherePoint(u, v);
                    BitmapEcran.DrawPixel((int)pt.X, (int)pt.Z, this.ShapeColor);
                }
            }
        }
    }
}

