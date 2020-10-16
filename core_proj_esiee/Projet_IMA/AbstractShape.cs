using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class AbstractShape : IShape
    {
        float u, v;
        Couleur shapeColor;

        public AbstractShape(float u, float v, Couleur shapeColor)
        {
            this.u = u;
            this.v = v;
            this.ShapeColor = shapeColor;
        }

        public float U { get => u; set => u = value; }
        public float V { get => v; set => v = value; }
        public Couleur ShapeColor { get => shapeColor; set => shapeColor = value; }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public Coord3D getIntersection()
        {
            throw new NotImplementedException();
        }
    }
}
