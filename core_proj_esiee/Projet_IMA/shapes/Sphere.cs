using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    /// <summary>
    /// Une sphere en 3D
    /// </summary>
    class Sphere : AbstractShape
    {
        #region attributs

        /// <summary>
        /// La rayon de la sphere
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// Le point au centre du cercle
        /// </summary>
        public V3 Center { get; set; }

        /// <summary>
        /// Coordonnees 2D x de la sphere
        /// </summary>
        public int X2D { get; set; }

        /// <summary>
        /// Coordonnees 2D y de la sphere
        /// </summary>
        public int Y2D { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur d une sphere
        /// </summary>
        /// <param name="center">Le point du centre</param>
        /// <param name="radius">Le rayon de la sphere</param>
        /// <param name="shapeColor">La couleur de la sphere</param>
        public Sphere(V3 center, int radius, Couleur shapeColor, Texture texture = null) : base(shapeColor, texture)
        {
            Center = new V3(center.X, center.Y, center.Z);
            Radius = radius;
            X2D = (int)center.X;
            Y2D = (int)center.Z;
        }

        /// <summary>
        /// Constructeur d une sphere avec coordonnees
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="radius"></param>
        /// <param name="shapeColor"></param>
        public Sphere(float x, float y, float z, int radius, Couleur shapeColor, Texture texture = null) : this(new V3(x, y, z), radius, shapeColor, texture) { }

        #endregion

        #region methodes

        public override V3 GetIntersection(V3 positionCamera, V3 dirRayon)
        {
            float a, b, c, delta, t1, t2;
            a = dirRayon * dirRayon;
            b = 2 * dirRayon * (positionCamera - Center);
            c = (positionCamera * positionCamera) + (Center * Center) - (Radius * Radius) - 2 * positionCamera * Center;
            delta = b * b - 4 * a * c;
            if (delta <= 0) return null;
            t1 = (-b - (float)Math.Sqrt(delta)) / (2 * a);
            t2 = (-b + (float)Math.Sqrt(delta)) / (2 * a);
            if (t1 > 0 && t2 > 0)
            {
                V3 intersectionPoint = positionCamera + t1 * dirRayon;
                return intersectionPoint;
            }
            else if (t1 < 0 && t2 > 0)
            {
                V3 intersectionPoint = positionCamera + t2 * dirRayon;
                return intersectionPoint;
            }
            else return null; // t1 < 0 && t2 < 0
        }

        private V3 FindSpherePoint(V3 intersection)
        {
            return new V3(
                intersection.X - Center.X,
                intersection.Y - Center.Y,
                intersection.Z - Center.Z
            );
        }

        /// <summary>
        /// Trouve un point par rapport a u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns>Un point</returns>
        private V3 FindPoint(float u, float v)
        {
            return new V3(
                IMA.Cosf(v) * IMA.Cosf(u),
                IMA.Cosf(v) * IMA.Sinf(u),
                IMA.Sinf(v)
            );
        }

        /// <summary>
        /// Recupere tout les points par rapport a u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns></returns>
        private V3 SpherePoints(float u, float v) => new V3(Radius * FindPoint(u, v) + Center);

        /// <summary>
        /// Calcul la normale a partir d une intersection
        /// A partir de cette intersection on va calculer u et v
        /// </summary>
        /// <param name="intersection">L intersection</param>
        /// <returns>La normal par rapport a l intersection</returns>
        public override V3 GetNormal(V3 intersection = null)
        {
            IMA.InvertCoordSpherique(FindSpherePoint(intersection), Radius, out float u, out float v);
            return GetNormal(u, v);
        }

        /// <summary>
        /// Calcul la normale de la sphere a partir de u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns>La normale de la sphere</returns>
        public V3 GetNormal(float u, float v) => FindPoint(u, v);

        public override Couleur GetColor(V3 intersection)
        {
            Couleur couleur;
            if (Texture != null)
            {
                IMA.InvertCoordSpherique(FindSpherePoint(intersection), Radius, out float u, out float v);
                u = u / IMA.DPI;
                v = (v + IMA.PI2) / (IMA.PI2 + IMA.PI2);
                couleur = Texture.ReadColor(u, v);
            }
            else
            {
                couleur = ShapeColor;
            }
            return couleur;
        }

        #endregion
    }
}

/*Equation intersection :
 * (OR + t * DR - C)² = r²
 * OR² + OR * t * DR - OR * C + OR * t * DR + t² * DR² - t * DR * C - OR * C - t * DR * C + C² = r²
 * DR² * t² + 2 * t * OR * DR - 2 * t * DR * C  + OR² - 2 * OR * C + C² - r² = 0
 * DR² * t2 + 2 * DR * (OR - C) * t + OR² - 2 * OR * C + C² - r² = 0
 *
 *Equation sous la forme ax² + bx + c =0 avec 
 * a = DR² (DR : direction rayon)
 * b = 2 * DR * (OR - C)  (OR : origine rayon, C : centre sphere)
 * c = OR² + C² - r² - 2 * OR * C (r :  rayon cercle)
 */
