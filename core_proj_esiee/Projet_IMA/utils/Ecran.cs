﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Projet_IMA.utils;

namespace Projet_IMA
{
    enum ModeAff { SLOW_MODE, FULL_SPEED};

    class BitmapEcran
    {
        const int refresh_every = 1000; // force l'affiche tous les xx pix
        static int nb_pix = 0;                 // comptage des pixels
        
        static private Bitmap B;
        static private ModeAff Mode;
        static private int Largeur;
        static private int Hauteur;
        static private int stride;
        static private BitmapData data;


        static public Bitmap Init(int largeur, int hauteur)
        {
            Largeur = largeur;
            Hauteur = hauteur;
            B = new Bitmap(largeur, hauteur);
            return B;
        }
 
        static void DrawFastPixel(int x, int y, Couleur c)
        {
            unsafe
            {
                byte RR, VV, BB; 
                c.Check();
                c.To255(out RR, out  VV, out  BB);
                
                byte* ptr = (byte*)data.Scan0;
                ptr[(x * 3) + y * stride    ] = BB;
                ptr[(x * 3) + y * stride + 1] = VV;
                ptr[(x * 3) + y * stride + 2] = RR;
            }
        }

        static void DrawSlowPixel(int x, int y, Couleur c)
        {
            Color cc = c.Convertion();
            B.SetPixel(x, y, cc);
            
            Program.MyForm.PictureBoxInvalidate();
            nb_pix++;
            if (nb_pix > refresh_every)  // force l'affichage à l'écran tous les 1000pix
            {
               Program.MyForm.PictureBoxRefresh();
               nb_pix = 0;
            }
         }

        /// /////////////////   public methods ///////////////////////

        static public void RefreshScreen(Couleur c)
        {
            if (Program.MyForm.Checked())
            {
                Mode = ModeAff.SLOW_MODE;
                Graphics g = Graphics.FromImage(B);
                Color cc = c.Convertion();
                g.Clear(cc);
            }
            else
            {
                Mode = ModeAff.FULL_SPEED;
                data = B.LockBits(new Rectangle(0, 0, B.Width, B.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                stride = data.Stride;
                for (int x = 0; x < Largeur; x++)
                    for (int y = 0; y < Hauteur; y++)
                        DrawFastPixel(x, y, c);
            }
        }
        
        public static void DrawPixel(int x, int y, Couleur c)
        {
            int x_ecran = x;
            int y_ecran = Hauteur - y;

            if ((x_ecran >= 0) && (x_ecran < Largeur) && (y_ecran >= 0) && (y_ecran < Hauteur))
                if (Mode == ModeAff.SLOW_MODE) DrawSlowPixel(x_ecran, y_ecran, c);
                else DrawFastPixel(x_ecran, y_ecran, c);
        }
        
        static public void Show()
        {
            if (Mode == ModeAff.FULL_SPEED)
                B.UnlockBits(data);

            Program.MyForm.PictureBoxInvalidate();
        }

        private static Couleur Illumination(Lampe lamp, IShape shape, V3 intersection, V3 directionRayon)
        {
            Couleur pixelColor = new Couleur(0, 0, 0);
            Couleur shapeColor = shape.GetColor(intersection);
            float coeffDiffus;

            pixelColor = shapeColor * new Couleur(0.3f, 0.3f, 0.3f); // modèle de réflexion ambiant

            V3 normal = shape.GetNormal(intersection);
            normal.Normalize();
            coeffDiffus = normal * lamp.Orientation;
            if (coeffDiffus < 0) { return pixelColor; }
            pixelColor += coeffDiffus * (shapeColor * lamp.Couleur); // Modèle diffus 
            
            
            V3 rayonReflechi = -lamp.Orientation + 2 * (normal * lamp.Orientation) * normal;
            directionRayon.Normalize();
            rayonReflechi.Normalize();
            float coeffSpeculaire = (float)Math.Pow(rayonReflechi * (-directionRayon), 98);
            pixelColor += coeffSpeculaire * lamp.Couleur; // Modèle spéculaire
            return pixelColor;
        }

        public static Couleur RayCast(V3 positionCamera, V3 directionRayon, List<IShape> objectsScene, Lampe lamp)
        {
            Couleur pixelColor = new Couleur(0,0,0);
            IShape mostClosestShape = null;
            V3 intersection = new V3(0,0,0);
            float mostClosestY = float.MaxValue;
            V3 mostClosestIntersection = null;
            foreach (IShape shape in objectsScene)
            {
                intersection = shape.GetIntersection(positionCamera, directionRayon);
                if (intersection != null)
                {
                    if (intersection.Y < mostClosestY)
                    {
                        mostClosestY = intersection.Y;
                        mostClosestShape = shape;
                        mostClosestIntersection = intersection;
                    }
                }
            }
            if (mostClosestShape != null)
            {
                pixelColor = Illumination(lamp, mostClosestShape, mostClosestIntersection, directionRayon);
            }
            return pixelColor;
        }

        static public int GetWidth() { return Largeur; }
        static public int GetHeight() { return Hauteur; }
    }
}
