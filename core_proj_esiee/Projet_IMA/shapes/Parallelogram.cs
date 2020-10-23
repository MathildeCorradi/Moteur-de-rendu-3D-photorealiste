﻿using System;

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

        public override void Draw()
        {
            for (float u = 0; u <= 1; u += 0.001f)
            {
                for (float v = 0; v <= 1; v += 0.001f)
                {
                    V3 pt = ParaPoint(u, v);
                    BitmapEcran.DrawPixel((int)pt.X, (int)pt.Z, ShapeColor);
                }
            }
        }

        public override V3 GetIntersection(V3 positionCamera, V3 dirRayon)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
