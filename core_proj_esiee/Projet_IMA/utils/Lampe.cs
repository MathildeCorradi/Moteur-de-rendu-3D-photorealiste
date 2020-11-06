using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA.utils
{
    class Lampe
    {
        Couleur couleur;
        V3 orientation;
        float affaiblissement;

        public Lampe(Couleur couleur, V3 orientation, float affaiblissement)
        {
            this.couleur = couleur;
            this.orientation = orientation;
            this.affaiblissement = affaiblissement;
        }

        public V3 Orientation { get => orientation; set => orientation = value; }
        public Couleur Couleur { get => couleur; set => couleur = value; }
        public float Affaiblissement { get => affaiblissement; set => affaiblissement = value; }
    }
}
