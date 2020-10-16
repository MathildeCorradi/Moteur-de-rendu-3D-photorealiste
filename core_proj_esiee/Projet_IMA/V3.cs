using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    struct V3
    {
        private float x;		// coordonnées du vecteur
        private float y;
        private float z;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }

        public float Norm()		// retourne la norme du vecteur
        {
            return (float) IMA.Sqrtf(X * X + Y * Y + Z * Z);
        }

        public float Norme2()
        {
            return X * X + Y * Y + Z * Z;
        }

        public void Normalize()	// normalise le vecteur
        {
            float n = Norm();
            if (n == 0) return;
            x /= n;
            y /= n;
            z /= n;
        }

        public V3(V3 t)
        {
            x = t.X;
            y = t.Y;
            z = t.Z;
        }

        public V3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        V3(int _x, int _y, int _z)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
        }

        public static V3 operator +(V3 a, V3 b)
        {
            V3 t = new V3(0, 0, 0);
            t.X = a.X + b.X;
            t.Y = a.Y + b.Y;
            t.Z = a.Z + b.Z;
            return t;
        }

        public static V3 operator -(V3 a, V3 b)
        {
            V3 t = new V3(0, 0, 0);
            t.X = a.X - b.X;
            t.Y = a.Y - b.Y;
            t.Z = a.Z - b.Z;
            return t;
        }

        public static V3 operator -(V3 a)
        {
            V3 t = new V3(0, 0, 0);
            t.X = -a.X;
            t.Y = -a.Y;
            t.Z = -a.Z;
            return t;
        }

        public static V3 operator ^ (V3 a, V3 b)  // produit vectoriel
        {
            V3 t = new V3(0, 0, 0);
            t.X = a.Y * b.Z - a.Z * b.Y;
            t.Y = a.Z * b.X - a.X * b.Z;
            t.Z = a.X * b.Y - a.Y * b.X;
            return t;
        }

        public static float operator * (V3 a,V3 b)         // produit scalaire
        {
            return a.X*b.X+a.Y*b.Y+a.Z*b.Z;
        }

       

        public static V3 operator *(float a, V3 b)
        {
            V3 t = new V3(0, 0, 0);
            t.X = b.X*a;
            t.Y = b.Y*a;
            t.Z=  b.Z*a;
            return t;
        }

        public static V3 operator *(V3 b, float a)
        {
            V3 t = new V3(0, 0, 0);
            t.X = b.X*a;
            t.Y = b.Y*a;
            t.Z=  b.Z*a;
            return t;
        }

        public static V3 operator /(V3 b, float a)
        {
            V3 t = new V3(0, 0, 0);
            t.X = b.X/a;
            t.Y = b.Y/a;
            t.Z=  b.Z/a;
            return t;
        }

        public static float prod_scal(ref V3 u, ref V3 v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }
    }
}
