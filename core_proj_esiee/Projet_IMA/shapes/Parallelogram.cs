using System;
using System.Collections.Generic;

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

        /// <summary>
        /// La normal de l objet
        /// </summary>
        protected V3 Normal { get; set; }

        /// <summary>
        /// Valeur pour savoir si on ignore son ombre
        /// </summary>
        private readonly bool ignoreShadow;

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="pointA">Le point A</param>
        /// <param name="pointB">Le point B</param>
        /// <param name="pointC">Le point C</param>
        /// <param name="shapeColor">La color d objet</param>
        public Parallelogram(V3 pointA, V3 pointB, V3 pointC, MyColor shapeColor, bool ignoreShadow, Texture textureBump = null, float intensiteBump = 0) : base(shapeColor, textureBump, intensiteBump)
        {
            InitPoints(pointA, pointB, pointC);
            this.ignoreShadow = ignoreShadow;
        }

        public Parallelogram(V3 pointA, V3 pointB, V3 pointC, Texture texture, bool ignoreShadow, Texture textureBump = null, float intensiteBump = 0) : base(texture, textureBump, intensiteBump)
        {
            InitPoints(pointA, pointB, pointC);
            this.ignoreShadow = ignoreShadow;
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

        private bool IsValidIntersection(float u, float v) => u >= 0 && u <= 1 && v >= 0 && v <= 1;

        public override V3 GetNormal(V3 intersection = null)
        {
            V3 normal = Normal;
            if (!HasBump())
            {
                normal.Normalize();
                return normal;
            }

            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            V3 AI = intersection - PointA;
            float u = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
            float v = ((Normal ^ AB) * AI) / (AC ^ AB).Norm();

            BumpTexture.Bump(u, v, out float dhdu, out float dhdv);
            V3 T2 = AB ^ (dhdv * normal);
            V3 T3 = (dhdu * normal) ^ AC;

            V3 normalBump = normal + (BumpIntensity * (T2 + T3));
            normalBump.Normalize();
            return normalBump;
        }

        public override MyColor GetColor(V3 intersection)
        {
            MyColor couleur;
            if (Texture != null)
            {
                // Recuperation de u et v pour recuperer la couleur dans la texture
                V3 AB = PointB - PointA;
                V3 AC = PointC - PointA;
                V3 AI = intersection - PointA;
                float u = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
                float v = -((Normal ^ AB) * AI) / (AC ^ AB).Norm();
                couleur = Texture.ReadColor(u, v);
            }
            else
            {
                couleur = ShapeColor;
            }
            return couleur;
        }

        public override bool IgnoreShadow() => ignoreShadow;

        public override bool Equals(object obj)
        {
            return obj is Parallelogram parallelogram &&
                   EqualityComparer<V3>.Default.Equals(PointA, parallelogram.PointA) &&
                   EqualityComparer<V3>.Default.Equals(PointB, parallelogram.PointB) &&
                   EqualityComparer<V3>.Default.Equals(PointC, parallelogram.PointC);
        }

        public override int GetHashCode()
        {
            int hashCode = 839654199;
            hashCode = hashCode * -1521134295 + EqualityComparer<V3>.Default.GetHashCode(PointA);
            hashCode = hashCode * -1521134295 + EqualityComparer<V3>.Default.GetHashCode(PointB);
            hashCode = hashCode * -1521134295 + EqualityComparer<V3>.Default.GetHashCode(PointC);
            return hashCode;
        }

        #endregion
    }
}
