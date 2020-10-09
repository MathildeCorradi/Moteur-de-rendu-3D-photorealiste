using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    interface IShape
    {
        Coord3D getIntersection();
        void draw();
    }
}
