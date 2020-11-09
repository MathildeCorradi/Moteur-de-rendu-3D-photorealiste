using System;

namespace Projet_IMA
{
    class IMA
    {
        #region attributs

        /// <summary>
        /// Valeur 2pi
        /// </summary>
        static public float DPI = (float) (Math.PI * 2);

        /// <summary>
        /// Valeur pi
        /// </summary>
        static public float PI  = (float) (Math.PI);
        

        /// <summary>
        /// Valeur pi/2
        /// </summary>
        static public float PI2 = (float) (Math.PI / 2);
        
        /// <summary>
        /// Valeur pi/4
        /// </summary>
        static public float PI4 = (float) (Math.PI / 4);
        
        /// <summary>
        /// Objet random pour generer des nombres aleatoirement
        /// </summary>
        static public Random Rng;

        #endregion

        #region methodes

        /// <summary>
        /// Calcul le cosinus d un angle theta
        /// </summary>
        /// <param name="theta">L angle a calculer</param>
        /// <returns>Le cosinus de l angle</returns>
        static public float Cosf(float theta) => (float) Math.Cos(theta);
        
        /// <summary>
        /// Calcul le sinus d un angle theta
        /// </summary>
        /// <param name="theta">L angle a calculer</param>
        /// <returns>Le sinus de l angle</returns>
        static public float Sinf(float theta) => (float) Math.Sin(theta);
        
        /// <summary>
        /// Calcul la racine carre d une valeur
        /// </summary>
        /// <param name="v">La valeur</param>
        /// <returns>La racine carre de la valeur</returns>
        static public float Sqrtf(float v) => (float) Math.Sqrt(v);

        /// <summary>
        /// Initialise l objet Random
        /// </summary>
        static public void InitRand() => Rng = new Random();
        
        /// <summary>
        /// Pas trop compris deso
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static public float RandNP(float v) =>((float) Rng.NextDouble() -0.5f) * 2 * v;
        
        /// <summary>
        /// Recupere un nombre aleatoire multiplier par la valeur
        /// </summary>
        /// <param name="v">La valeur</param>
        /// <returns>Le nombre aleatoire</returns>
        static public float RandP(float v) => ((float) Rng.NextDouble()) * v;

        /// <summary>
        /// Permet d inverser les coordonnees d un cercle
        /// </summary>
        /// <param name="point">Un point du cercle</param>
        /// <param name="radius">Le rayon du cercle</param>
        /// <param name="u">Angle u</param>
        /// <param name="v">Angle v</param>
        static public  void InvertCoordSpherique(V3 point, float radius, out float u, out float v)
        {
            point /= radius;
            if (point.Z >= 1) {
                u = PI2;
                v = 0;
            }
            else if (point.Z <= -1) {
                u = -PI2;
                v = 0;
            } else {
                v = (float) Math.Asin(point.Z);
                float t = point.X / Cosf(v);
                if (t <= -1) {
                    u = PI;
                } else if (t >= 1) { 
                    u = 0;
                } else {
                    u = (point.Y < 0)? (float)(2 * PI - Math.Acos(t)) : (float)Math.Acos(t);
                }
            }
        }
        #endregion
    }
}
