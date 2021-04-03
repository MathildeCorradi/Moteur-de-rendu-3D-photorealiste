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
        public Sphere(V3 center, int radius, MyColor shapeColor, Texture textureBump = null, float intensiteBump = 0) : base(shapeColor, textureBump, intensiteBump)
        {
            InitPoints(center, radius);
        }

        public Sphere(V3 center, int radius, Texture texture, Texture textureBump = null, float intensiteBump = 0) : base(texture, textureBump, intensiteBump)
        {
            InitPoints(center, radius);
        }

        /// <summary>
        /// Constructeur d une sphere avec coordonnees
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="radius"></param>
        /// <param name="shapeColor"></param>
        public Sphere(float x, float y, float z, int radius, MyColor shapeColor, Texture textureBump = null, float intensiteBump = 0) : this(new V3(x, y, z), radius, shapeColor, textureBump, intensiteBump) { }

        /// <summary>
        /// Constructeur d une sphere avec une texture
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="radius"></param>
        /// <param name="texture"></param>
        /// <param name="textureBump"></param>
        /// <param name="intensiteBump"></param>
        public Sphere(float x, float y, float z, int radius, Texture texture, Texture textureBump = null, float intensiteBump = 0) : this(new V3(x, y, z), radius, texture, textureBump, intensiteBump) { }

        /// <summary>
        /// On initialise notre sphere a partir des infos des constructeurs
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        private void InitPoints(V3 center, int radius)
        {
            Center = new V3(center.X, center.Y, center.Z);
            Radius = radius;
            X2D = (int)center.X;
            Y2D = (int)center.Z;
        }

        #endregion

        #region methodes

        /// <summary>
        /// Calcul de l intersection entre l objet et un rayon lumineux
        /// On resout une equation du second degres avec delta = b^2 - 4ac 
        /// </summary>
        /// <param name="positionCamera"></param>
        /// <param name="dirRayon"></param>
        /// <returns></returns>
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
            // t1 < 0 && t2 < 0
            else
            {
                return null;
            }
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
                Tools.Cosf(v) * Tools.Cosf(u),
                Tools.Cosf(v) * Tools.Sinf(u),
                Tools.Sinf(v)
            );
        }

        private V3 FindPointDerU(float u, float v)
        {
            return new V3(
                Tools.Cosf(v) * (-Tools.Sinf(u)),
                Tools.Cosf(v) * Tools.Cosf(u),
                0
            );
        }

        private V3 FindPointDerV(float u, float v)
        {
            return new V3(
                (-Tools.Sinf(v)) * Tools.Cosf(u),
                (-Tools.Sinf(v)) * Tools.Sinf(u),
                Tools.Cosf(v)
            );
        }

        /// <summary>
        /// Calcul la normale a partir d une intersection
        /// A partir de cette intersection on va calculer u et v
        /// </summary>
        /// <param name="intersection">L intersection</param>
        /// <returns>La normal par rapport a l intersection</returns>
        public override V3 GetNormal(V3 intersection = null)
        {
            Tools.InvertCoordSpherique(FindSpherePoint(intersection), Radius, out float u, out float v);
            return GetNormal(u, v);
        }

        /// <summary>
        /// Calcul d une normale avec un bump
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        public override V3 GetNormalBump(V3 intersection = null)
        {
            Tools.InvertCoordSpherique(FindSpherePoint(intersection), Radius, out float u, out float v);
            V3 normal = GetNormal(intersection);
            normal.Normalize();
            float uTexture = u / Tools.TAU;
            float vTexture = -(v + Tools.PI2) / (Tools.PI2 + Tools.PI2);
            BumpTexture.Bump(uTexture,vTexture, out float dhdu, out float dhdv);
            V3 T2 = FindPointDerU(u,v) ^ (dhdv * normal);
            V3 T3 = (dhdu * normal) ^ FindPointDerV(u, v);

            V3 normalBump = normal + (BumpIntensity * (T2+T3));
            return normalBump;
        }

        /// <summary>
        /// Calcul la normale de la sphere a partir de u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns>La normale de la sphere</returns>
        public V3 GetNormal(float u, float v) => FindPoint(u, v);

        public override MyColor GetColor(V3 intersection)
        {
            MyColor couleur;
            if (Texture != null)
            {
                Tools.InvertCoordSpherique(FindSpherePoint(intersection), Radius, out float u, out float v);
                u /= Tools.TAU;
                v = -(v + Tools.PI2) / (Tools.PI2 + Tools.PI2);
                couleur = Texture.ReadColor(u, v);
            }
            else
            {
                couleur = ShapeColor;
            }
            return couleur;
        }

        public override bool Equals(object obj)
        {
            return obj is Sphere sphere &&
                   Radius == sphere.Radius &&
                   EqualityComparer<V3>.Default.Equals(Center, sphere.Center);
        }

        public override int GetHashCode()
        {
            int hashCode = 1866836079;
            hashCode = hashCode * -1521134295 + Radius.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<V3>.Default.GetHashCode(Center);
            return hashCode;
        }

        #endregion
    }
}

/*
 * Equation intersection :
 * (OR + t * DR - C)² = r²
 * OR² + OR * t * DR - OR * C + OR * t * DR + t² * DR² - t * DR * C - OR * C - t * DR * C + C² = r²
 * DR² * t² + 2 * t * OR * DR - 2 * t * DR * C  + OR² - 2 * OR * C + C² - r² = 0
 * DR² * t2 + 2 * DR * (OR - C) * t + OR² - 2 * OR * C + C² - r² = 0
 *
 * Equation sous la forme ax² + bx + c = 0 avec 
 * a = DR² (DR : direction rayon)
 * b = 2 * DR * (OR - C)  (OR : origine rayon, C : centre sphere)
 * c = OR² + C² - r² - 2 * OR * C (r :  rayon cercle)
 */
