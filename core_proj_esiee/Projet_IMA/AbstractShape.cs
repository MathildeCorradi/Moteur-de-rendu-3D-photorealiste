using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    public abstract class AbstractShape : IShape
    {
        public Couleur ShapeColor { get => ShapeColor; set => ShapeColor = value; }

        public AbstractShape(Couleur shapeColor)
        {
            ShapeColor = shapeColor;
        }

        public abstract V3 GetIntersection();

        public abstract void Draw();
    }
}
