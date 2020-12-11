using System;

namespace Projet_IMA
{
    /// <summary>
    /// Parallelogram former a l aide de deux vecteurs
    /// Un vecteur AB pour la premiere base
    /// Un vecteur AC pour la secondde base
    /// </summary>
    class Parallelogram : AbstractShape
    {
        #region attributs

        /// <summary>
        /// Le point en bas a gauche
        /// </summary>
        public V3 PointA { get; set; }

        /// <summary>
        /// Le point en bas a droite
        /// </summary>
        public V3 PointB { get; set; }

        /// <summary>
        /// Le point en haut a gauche
        /// </summary>
        public V3 PointC { get; set; }

        protected V3 Normal { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="pointA">Le point A</param>
        /// <param name="pointB">Le point B</param>
        /// <param name="pointC">Le point C</param>
        /// <param name="shapeColor">La color d objet</param>
        public Parallelogram(V3 pointA, V3 pointB, V3 pointC, Couleur shapeColor, Texture textureBump = null) : base(shapeColor, textureBump)
        {
            InitPoints(pointA, pointB, pointC);
        }

        public Parallelogram(V3 pointA, V3 pointB, V3 pointC, Texture texture, Texture textureBump = null) : base(texture, textureBump)
        {
            InitPoints(pointA, pointB, pointC);
        }

        private void InitPoints(V3 pointA, V3 pointB, V3 pointC)
        {
            PointA = pointA;
            PointB = pointB;
            PointC = pointC;
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            Normal = (AB ^ AC) / (AB ^ AC).Norm();
        }

        #endregion

        #region methodes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns></returns>
        public V3 ParaPoint(float u, float v)
        {
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            return new V3(PointA + u * AB + v * AC);
        }

        public V3 ParaPointDerU(float u, float v)
        {
            V3 AB = PointB - PointA;
            return AB;
        }

        public V3 ParaPointDerV(float u, float v)
        {
            V3 AC = PointC - PointA;
            return AC;
        }

        public override V3 GetIntersection(V3 positionCamera, V3 dirRayon)
        {
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            float t = ((PointA - positionCamera) * Normal) / (dirRayon * Normal);
            if (t < 0)
            {
                return null;
            }
            V3 intersection = positionCamera + t * dirRayon;
            V3 AI = intersection - PointA;
            float u = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
            float v = ((Normal ^ AB) * AI) / (AC ^ AB).Norm();
            return (IsValidIntersection(u, v)) ? intersection : null;
        }

        private bool IsValidIntersection(float u, float v)
        {
            return
                u >= 0 && u <= 1
                && v >= 0 && v <= 1;
        }

        public override V3 GetNormal(V3 intersection = null)
        {
            return Normal;
        }

       public override V3 GetNormalBump(V3 intersection = null)
        {
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            V3 AI = intersection - PointA;
            float u = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
            float v = ((Normal ^ AB) * AI) / (AC ^ AB).Norm();
            V3 normal = GetNormal();
            TextureBump.Bump(u, v, out float dhdu, out float dhdv);
            V3 T2 = ParaPointDerU(u, v) ^ (dhdv * normal);
            V3 T3 = (dhdu * normal) ^ ParaPointDerV(u, v);

            V3 normalBump = normal + (5 * (T2 + T3));
            return normalBump;
        }


        public override Couleur GetColor(V3 intersection)
        {
            Couleur couleur;
            if (Texture != null)
            {
                V3 AB = PointB - PointA;
                V3 AC = PointC - PointA;
                V3 AI = intersection - PointA;
                float u = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
                float v = ((Normal ^ AB) * AI) / (AC ^ AB).Norm();
                couleur = Texture.ReadColor(u, v);
            }
            else
            {
                couleur = ShapeColor;
            }
            return couleur;
        }

        public override bool hasBump()
        {
            if (TextureBump == null)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
