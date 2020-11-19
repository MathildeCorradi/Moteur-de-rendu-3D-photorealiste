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

        public float U { get; set; }

        public float V { get; set; }

        private V3 Normal { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="pointA">Le point A</param>
        /// <param name="pointB">Le point B</param>
        /// <param name="pointC">Le point C</param>
        /// <param name="shapeColor">La color d objet</param>
        public Parallelogram(V3 pointA, V3 pointB, V3 pointC, Couleur shapeColor) : base(shapeColor)
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
            U = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
            V = ((Normal ^ AB) * AI) / (AC ^ AB).Norm();
            return (IsValidIntersection())? intersection : null;
        }

        private bool IsValidIntersection()
        {
            return
                U >= 0 && U <= 1
                && V >= 0 && V <= 1;
        }

        public override V3 GetNormal(V3 intersection = null)
        {
            return Normal; 
        }

        #endregion
    }
}
