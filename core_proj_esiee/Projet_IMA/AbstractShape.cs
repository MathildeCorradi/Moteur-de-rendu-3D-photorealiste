using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class AbstractShape : IShape
    {
        Couleur shapeColor;

        public AbstractShape(Couleur shapeColor)
        {
            this.ShapeColor = shapeColor;
        }

        public Couleur ShapeColor { get => shapeColor; set => shapeColor = value; }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public V3 getIntersection()
        {
            throw new NotImplementedException();
        }
    }
}
