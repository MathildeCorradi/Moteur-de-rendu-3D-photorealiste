using System;
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

        static private List<IShape> objects;


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

        private static Couleur Illumination(List<Lampe> lamps, IShape shape, V3 intersection, V3 directionRayon)
        {
            Couleur pixelColor = new Couleur(0, 0, 0);
            Couleur shapeColor = shape.GetColor(intersection);

            pixelColor = shapeColor * new Couleur(0.1f, 0.1f, 0.1f); // modèle de réflexion ambiant





            if (shape.hasBump())
            {
                V3 normalBump = shape.GetNormalBump(intersection);
                normalBump.Normalize();
                float coeffDiffusBump = normalBump * lamps[0].Orientation;
                float coeffDiffusBump2 = normalBump * lamps[1].Orientation;
                if (coeffDiffusBump >= 0 && !IsIntersect(intersection, lamps[0].Orientation, shape))
                {
                    pixelColor += coeffDiffusBump * (shapeColor * lamps[0].Couleur); // Modèle diffus avec dump

                    V3 rayonReflechi = -lamps[0].Orientation + 2 * (normalBump * lamps[0].Orientation) * normalBump;
                    directionRayon.Normalize();
                    rayonReflechi.Normalize();
                    float coeffSpeculaire = (float)Math.Pow(rayonReflechi * (-directionRayon), 98);
                    pixelColor += coeffSpeculaire * lamps[0].Couleur; // Modèle spéculaire
                }
                if (coeffDiffusBump2 >= 0 && !IsIntersect(intersection, lamps[1].Orientation, shape))
                {
                    pixelColor += coeffDiffusBump2 * (shapeColor * lamps[1].Couleur); // Modèle diffus avec dump
                }
            }
            else
            {
                V3 normal = shape.GetNormal(intersection);
                normal.Normalize();
                float coeffDiffus = normal * lamps[0].Orientation;
                float coeffDiffus2 = normal * lamps[1].Orientation;
                if (coeffDiffus >= 0 && !IsIntersect(intersection, lamps[0].Orientation, shape))
                {
                    pixelColor += coeffDiffus * (shapeColor * lamps[0].Couleur); // Modèle diffus sans dump

                    V3 rayonReflechi = -lamps[0].Orientation + 2 * (normal * lamps[0].Orientation) * normal;
                    directionRayon.Normalize();
                    rayonReflechi.Normalize();
                    float coeffSpeculaire = (float)Math.Pow(rayonReflechi * (-directionRayon), 98);
                    pixelColor += coeffSpeculaire * lamps[0].Couleur; // Modèle spéculaire

                }
                if (coeffDiffus2 >= 0 && !IsIntersect(intersection, lamps[1].Orientation, shape))
                {
                    pixelColor += coeffDiffus2 * (shapeColor * lamps[1].Couleur); // Modèle diffus sans dump

                }
            }
            
            return pixelColor;
        }

        private static bool IsIntersect(V3 position, V3 direction, IShape currentShape)
        {
            V3 intersection;
            foreach (IShape shape in objects)
            {
                if (!currentShape.Equals(shape))
                {
                    intersection = shape.GetIntersection(position, direction);
                    if (intersection != null)
                    {
                        return true;
                    }
                }
                else
                {
                    //if (shape.GetType().ToString() == "Projet_IMA.Sphere") throw new Exception("sphere equals");
                }
            }
            return false;
        }

        public static Couleur RayCast(V3 positionCamera, V3 directionRayon, List<IShape> objectsScene, List<Lampe> lamps)
        {
            // @TODO: Bouger ca c est pas propre :o)
            objects = objectsScene;
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
                pixelColor = Illumination(lamps, mostClosestShape, mostClosestIntersection, directionRayon);
            }
            return pixelColor;
        }

        static public int GetWidth() { return Largeur; }
        static public int GetHeight() { return Hauteur; }
    }
}
