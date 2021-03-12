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
        public Sphere(V3 center, int radius, Couleur shapeColor, Texture textureBump = null, float intensiteBump = 0) : base(shapeColor, textureBump, intensiteBump)
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
        public Sphere(float x, float y, float z, int radius, Couleur shapeColor, Texture textureBump = null, float intensiteBump = 0) : this(new V3(x, y, z), radius, shapeColor, textureBump, intensiteBump) { }

        public Sphere(float x, float y, float z, int radius, Texture texture, Texture textureBump = null, float intensiteBump = 0) : this(new V3(x, y, z), radius, texture, textureBump, intensiteBump) { }

        private void InitPoints(V3 center, int radius)
        {
            Center = new V3(center.X, center.Y, center.Z);
            Radius = radius;
            X2D = (int)center.X;
            Y2D = (int)center.Z;
        }

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

        private V3 FindPointDerU(float u, float v)
        {
            return new V3(
                IMA.Cosf(v) * (-IMA.Sinf(u)),
                IMA.Cosf(v) * IMA.Cosf(u),
                0
            );
        }

        private V3 FindPointDerV(float u, float v)
        {
            return new V3(
                (-IMA.Sinf(v)) * IMA.Cosf(u),
                (-IMA.Sinf(v)) * IMA.Sinf(u),
                IMA.Cosf(v)
            );
        }

        /// <summary>
        /// Recupere tout les points par rapport a u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns></returns>
        private V3 SpherePoints(float u, float v) => new V3(Radius * FindPoint(u, v) + Center);

        private V3 SpherePointsDeriveU(float u, float v) => new V3(Radius * FindPointDerU(u, v) + Center);

        private V3 SpherePointsDeriveV(float u, float v) => new V3(Radius * FindPointDerV(u, v) + Center);

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

        public override V3 GetNormalBump(V3 intersection = null)
        {
            IMA.InvertCoordSpherique(FindSpherePoint(intersection), Radius, out float u, out float v);
            V3 normal = GetNormal(intersection);
            normal.Normalize();
            float uTexture = u / IMA.DPI;
            float vTexture = -(v + IMA.PI2) / (IMA.PI2 + IMA.PI2);
            TextureBump.Bump(uTexture,vTexture, out float dhdu, out float dhdv);
            V3 T2 = FindPointDerU(u,v) ^ (dhdv * normal);
            V3 T3 = (dhdu * normal) ^ FindPointDerV(u, v);

            V3 normalBump = normal + (IntensiteBump * (T2+T3));
            return normalBump;
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
                v = -(v + IMA.PI2) / (IMA.PI2 + IMA.PI2);
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

        public override bool Equals(object obj)
        {
            return Equals(obj as Sphere);
        }

        public bool Equals(Sphere obj)
        {
            return obj != null &&
                obj.Radius == Radius &&
                obj.Center == Center;
        }

        public override int GetHashCode()
        {
            return Radius.GetHashCode() ^ Center.GetHashCode();
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
