namespace Projet_IMA
{
    /// <summary>
    /// Vecteur a trois dimension
    /// </summary>
    public class V3
    {
        #region attributs

        /// <summary>
        /// Position x du vecteur
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Position y du vecteur
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Position z du vecteur
        /// </summary>
        public float Z { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Nouveau vecteur a partir d un vecteur existant
        /// </summary>
        /// <param name="v">Le vecteur a copie</param>
        public V3(V3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        /// <summary>
        /// Nouveau vecteur a partir de points
        /// </summary>
        /// <param name="x">Point x</param>
        /// <param name="y">Point y</param>
        /// <param name="z">Point z</param>
        public V3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Meme constructeur que le precedent mais avec des entiers
        /// </summary>
        /// <param name="x">Point x</param>
        /// <param name="y">Point y</param>
        /// <param name="z">Point z</param>
        public V3(int x, int y, int z) : this((float)x, y, z) { }

        #endregion

        #region methodes

        /// <summary>
        /// Calcul la norme du vecteur
        /// </summary>
        /// <returns>La norme du vecteur</returns>
        public float Norm()
        {
            return IMA.Sqrtf(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Calcul la norme du vecteur
        /// Pas sur que ce soit ca a voir
        /// </summary>
        /// <returns>La norme du vecteur ?</returns>
        public float NormWithoutSquare()
        {
            return X * X + Y * Y + Z * Z;
        }

        /// <summary>
        /// Normalise un vecteur pour avoir un vecteur unitaire
        /// </summary>
        public void Normalize()
        {
            float norm = Norm();
            if (norm == 0) return;
            X /= norm;
            Y /= norm;
            Z /= norm;
        }

        public string toString()
        {
            return X + " " + Y + " " + Z;
        }

        #endregion

        #region operateurs

        /// <summary>
        /// Addition de deux vecteurs
        /// </summary>
        /// <param name="a">Vecteur 1</param>
        /// <param name="b">Vecteur 2</param>
        /// <returns>Le nouveau vecteur</returns>
        public static V3 operator +(V3 a, V3 b)
        {
            V3 v = new V3(0, 0, 0);
            v.X = a.X + b.X;
            v.Y = a.Y + b.Y;
            v.Z = a.Z + b.Z;
            return v;
        }

        /// <summary>
        /// Soustraction de deux vecteurs
        /// </summary>
        /// <param name="a">Vecteur 1</param>
        /// <param name="b">Vecteur 2</param>
        /// <returns>Le nouveau vecteur</returns>
        public static V3 operator -(V3 a, V3 b)
        {
            V3 v = new V3(0, 0, 0);
            v.X = a.X - b.X;
            v.Y = a.Y - b.Y;
            v.Z = a.Z - b.Z;
            return v;
        }

        /// <summary>
        /// Inverse
        /// </summary>
        /// <param name="a">Le vecteur</param>
        /// <returns>Le vecteur inverse</returns>
        public static V3 operator -(V3 a)
        {
            V3 v = new V3(0, 0, 0);
            v.X = -a.X;
            v.Y = -a.Y;
            v.Z = -a.Z;
            return v;
        }

        /// <summary>
        /// Produit vectoriel
        /// </summary>
        /// <param name="a">Vecteur 1</param>
        /// <param name="b">Vecteur 2</param>
        /// <returns>Le nouveau vecteur</returns>
        public static V3 operator ^(V3 a, V3 b)
        {
            V3 v = new V3(0, 0, 0);
            v.X = a.Y * b.Z - a.Z * b.Y;
            v.Y = a.Z * b.X - a.X * b.Z;
            v.Z = a.X * b.Y - a.Y * b.X;
            return v;
        }

        /// <summary>
        /// Produit scalaire
        /// </summary>
        /// <param name="a">Vecteur 1</param>
        /// <param name="b">Vecteur 2</param>
        /// <returns>Le nouveau vecteur</returns>
        public static float operator *(V3 a, V3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }


        /// <summary>
        /// Multiplication d un vecteur selon un scalaire
        /// </summary>
        /// <param name="a">La valeur</param>
        /// <param name="b">Le vecteur</param>
        /// <returns></returns>
        public static V3 operator *(float a, V3 b)
        {
            V3 v = new V3(0, 0, 0);
            v.X = b.X * a;
            v.Y = b.Y * a;
            v.Z = b.Z * a;
            return v;
        }

        /// <summary>
        /// Division d un vecteur selon un scalaire
        /// </summary>
        /// <param name="b">Le vecteur</param>
        /// <param name="a">La valeur</param>
        /// <returns>Le nouveau vecteur</returns>
        public static V3 operator /(V3 b, float a)
        {
            V3 v = new V3(0, 0, 0);
            v.X = b.X / a;
            v.Y = b.Y / a;
            v.Z = b.Z / a;
            return v;
        }

        /// <summary>
        /// Calcul le produit scalaire entre 2 vecteurs
        /// </summary>
        /// <param name="a">Vecteur 1</param>
        /// <param name="b">Vecteur 2</param>
        /// <returns>Le produit scalaire</returns>
        public static float produitScalaire(ref V3 u, ref V3 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }

        #endregion
    }
}
