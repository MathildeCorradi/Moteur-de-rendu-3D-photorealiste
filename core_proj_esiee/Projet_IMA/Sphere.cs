using System;

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

        #region methodes

        /// <summary>
        /// Constructeur d une sphere
        /// </summary>
        /// <param name="center">Le point du centre</param>
        /// <param name="radius">Le rayon de la sphere</param>
        /// <param name="shapeColor">La couleur de la sphere</param>
        public Sphere(V3 center, int radius, Couleur shapeColor) : base(shapeColor)
        {
            center = new V3(center.X, center.Y, center.Z);
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
        public Sphere(float x, float y, float z, int radius, Couleur shapeColor) : this(new V3(x, y, z), radius, shapeColor) { }

        /// <summary>
        /// Calcul la normale de la sphere
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns>La normale de la sphere</returns>
        public V3 GetNormal(float u, float v) => FindPoint(u, v);

        public override V3 GetIntersection()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trouve un point par rapport a u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns>Un point</returns>
        private V3 FindPoint(float u, float v)
        {
            return new V3
            {
                X = IMA.Cosf(v) * IMA.Cosf(u),
                Y = IMA.Cosf(v) * IMA.Sinf(u),
                Z = IMA.Sinf(v)
            };
        }

        /// <summary>
        /// Recupere tout les points par rapport a u et v
        /// </summary>
        /// <param name="u">L angle u</param>
        /// <param name="v">L angle v</param>
        /// <returns></returns>
        private V3 SpherePoints(float u, float v) => new V3(Radius * FindPoint(u, v) + Center);

        public override void Draw()
        {
            for (float u = 0; u <= IMA.DPI; u += 0.005f)
            {
                for (float v = -(IMA.PI2); v <= IMA.PI2; v += 0.005f)
                {
                    V3 pt = SpherePoints(u, v);
                    BitmapEcran.DrawPixel((int)pt.X, (int)pt.Z, this.ShapeColor);
                }
            }
        }
    }

    #endregion
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
