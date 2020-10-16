using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    interface IShape
    {
        V3 getIntersection();
        void draw();
    }
}
