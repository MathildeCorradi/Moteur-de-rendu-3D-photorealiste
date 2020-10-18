﻿using System.Drawing;

namespace Projet_IMA
{
    /// <summary>
    /// Represente une couleur
    /// C est a dire 3 champs RGB compris entre 0 et 1
    /// </summary>
    public struct Couleur
    {
        #region attributs

        /// <summary>
        /// Couleur rouge
        /// </summary>
        public float Red { get; set; }

        /// <summary>
        /// Couleur vert
        /// </summary>
        public float Green { get; set; }

        /// <summary>
        /// Couleur bleue
        /// </summary>
        public float Blue { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Construit une couleur a partir des donnees RGB
        /// </summary>
        /// <param name="red">Niveau de rouge</param>
        /// <param name="green">Niveau de vert</param>
        /// <param name="blue">Niveau de bleue</param>
        public Couleur(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// Permet de copier une couleur
        /// </summary>
        /// <param name="color">La couleur a copier</param>
        public Couleur(Couleur color)
        {
            Red = color.Red;
            Green = color.Green;
            Blue = color.Blue;
        }

        #endregion

        #region methodes

        /// <summary>
        /// Permet de convertir une couleur au format RGB([0, 255])
        /// au format RGB([0, 1])
        /// </summary>
        /// <param name="red">Niveau de rouge</param>
        /// <param name="green">Niveau de vert</param>
        /// <param name="blue">Niveau de bleue</param>
        public void From255(byte red, byte green, byte blue)
        {
            Red = (float)(red / 255.0);
            Green = (float)(green / 255.0);
            Blue = (float)(blue / 255.0);
        }

        /// <summary>
        /// Permet d obtenir dans 3 variable passer en parametre le niveau
        /// de chaque couleur au format RGB([0, 255])
        /// </summary>
        /// <param name="red">Niveau de rouge</param>
        /// <param name="green">Niveau de vert</param>
        /// <param name="blue">Niveau de bleue</param>
        public void To255(out byte red, out byte green, out byte blue)
        {
            red = (byte)(Red * 255);
            green = (byte)(Green * 255);
            blue = (byte)(Blue * 255);
        }

        /// <summary>
        /// Permet de transposer une couleur
        /// </summary>
        /// <param name="objectTarget">L objet couleur qu on veut utiliser</param>
        /// <param name="color">La couleur qu on souhaite obtenir</param>
        static public void Transpose(ref Couleur objectTarget, Color color)
        {
            objectTarget.Red = (float)(color.R / 255.0);
            objectTarget.Green = (float)(color.G / 255.0);
            objectTarget.Blue = (float)(color.B / 255.0);
        }

        /// <summary>
        /// Permet d obtenir l objet courrant en structure Color
        /// </summary>
        /// <returns>La Couleur en structure Color</returns>
        public Color Convertion()
        {
            Check();
            byte red, green, blue;
            To255(out red, out green, out blue);
            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Permet de verifier si la structure est valide
        /// dans le cas contraire un fix celle ci
        /// </summary>
        public void Check()
        {
            if (Red > 1.0) Red = 1.0f;
            if (Green > 1.0) Green = 1.0f;
            if (Blue > 1.0) Blue = 1.0f;
        }

        /// <summary>
        /// Convertit la couleur en niveau de gris
        /// Utile pour le bump map
        /// </summary>
        /// <returns>Le niveau de gros</returns>
        public float GreyLevel()
        {
            return (Red + Green + Blue) / 3.0f;
        }

        #endregion

        #region operateurs

        /// <summary>
        /// Addition de deux couleurs
        /// </summary>
        /// <param name="a">Couleur 1</param>
        /// <param name="b">Couleur 2</param>
        /// <returns></returns>
        public static Couleur operator +(Couleur a, Couleur b)
        {
            return new Couleur(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue);
        }

        /// <summary>
        /// Soustraction de deux couleurs
        /// </summary>
        /// <param name="a">Couleur 1</param>
        /// <param name="b">Couleur 2</param>
        /// <returns></returns>
        public static Couleur operator -(Couleur a, Couleur b)
        {
            return new Couleur(a.Red - b.Red, a.Green - b.Green, a.Blue - b.Blue);
        }

        /// <summary>
        /// Inverse d une couleur
        /// </summary>
        /// <param name="a">La couleur</param>
        /// <returns></returns>
        public static Couleur operator -(Couleur a)
        {
            return new Couleur(-a.Red, -a.Green, -a.Blue);
        }

        /// <summary>
        /// Multiplication de deux couleurs
        /// </summary>
        /// <param name="a">Couleur 1</param>
        /// <param name="b">Couleur 2</param>
        /// <returns></returns>
        public static Couleur operator *(Couleur a, Couleur b)
        {
            return new Couleur(a.Red * b.Red, a.Green * b.Green, a.Blue * b.Blue);
        }

        /// <summary>
        /// Multiplication d une couleur d apres une valeur k
        /// </summary>
        /// <param name="a">La valeur</param>
        /// <param name="b">La couleur</param>
        /// <returns></returns>
        public static Couleur operator *(float a, Couleur b)
        {
            return new Couleur(a * b.Red, a * b.Green, a * b.Blue);
        }

        /// <summary>
        /// Division d une couleur d apres une valeur k
        /// </summary>
        /// <param name="b">La couleur</param>
        /// <param name="a">La valeur</param>
        /// <returns></returns>
        public static Couleur operator /(Couleur b, float a)
        {
            return new Couleur(b.Red / a, b.Green / a, b.Blue / a);
        }

        #endregion
    }
}
