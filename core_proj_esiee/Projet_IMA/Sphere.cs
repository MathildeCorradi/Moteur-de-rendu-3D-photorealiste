using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere : AbstractShape
    {
        int radius;
        Coord3D center;

        public Sphere(Coord3D center, int radius, float u, float v, Couleur shapeColor) : base(u, v, shapeColor)
        {
            center = new Coord3D(center.X, center.Y, center.Z);
            this.radius = radius;
        }

        public Sphere(float x, float y, float z, int radius, float u, float v, Couleur shapeColor) : base(u, v, shapeColor)
        {
            center = new Coord3D(x, y, z);
            this.radius = radius;
        }


        /*public V3 getNormal()
        {

            return; 
        }*/

        public Coord3D getIntersection()
        {
            throw new NotImplementedException();
        }
    }
}

