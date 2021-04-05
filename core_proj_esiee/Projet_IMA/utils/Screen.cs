using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Projet_IMA.utils;

namespace Projet_IMA
{
    enum DisplayMode { SLOW_MODE, FULL_SPEED};

    class Screen
    {
        /// <summary>
        /// Nombre de pixel où on force l affichage
        /// </summary>
        const int refreshRate = 1000;
        static int currentRate = 0;
        
        static private Bitmap B;
        static private DisplayMode DisplayMode;
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
 
        static void DrawFastPixel(int x, int y, MyColor c)
        {
            unsafe
            {
                c.Check();
                c.To255(out byte RR, out byte VV, out byte BB);

                byte* ptr = (byte*)data.Scan0;
                ptr[(x * 3) + y * stride    ] = BB;
                ptr[(x * 3) + y * stride + 1] = VV;
                ptr[(x * 3) + y * stride + 2] = RR;
            }
        }

        static void DrawSlowPixel(int x, int y, MyColor c)
        {
            Color cc = c.Convert();
            B.SetPixel(x, y, cc);
            
            Program.MyForm.PictureBoxInvalidate();
            currentRate++;
            if (currentRate > refreshRate)  // force l'affichage à l'écran tous les 1000pix
            {
               Program.MyForm.PictureBoxRefresh();
               currentRate = 0;
            }
         }

        /// /////////////////   public methods ///////////////////////

        static public void RefreshScreen(MyColor c)
        {
            if (Program.MyForm.FastMode())
            {
                DisplayMode = DisplayMode.SLOW_MODE;
                Graphics g = Graphics.FromImage(B);
                Color cc = c.Convert();
                g.Clear(cc);
            }
            else
            {
                DisplayMode = DisplayMode.FULL_SPEED;
                data = B.LockBits(new Rectangle(0, 0, B.Width, B.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                stride = data.Stride;
                for (int x = 0; x < Largeur; x++)
                    for (int y = 0; y < Hauteur; y++)
                        DrawFastPixel(x, y, c);
            }
        }
        
        public static void DrawPixel(int x, int y, MyColor c)
        {
            int x_ecran = x;
            int y_ecran = Hauteur - y;

            if ((x_ecran >= 0) && (x_ecran < Largeur) && (y_ecran >= 0) && (y_ecran < Hauteur))
                if (DisplayMode == DisplayMode.SLOW_MODE) DrawSlowPixel(x_ecran, y_ecran, c);
                else DrawFastPixel(x_ecran, y_ecran, c);
        }
        
        static public void Show()
        {
            if (DisplayMode == DisplayMode.FULL_SPEED)
                B.UnlockBits(data);

            Program.MyForm.PictureBoxInvalidate();
        }

        private static MyColor Illumination(List<Light> lights, IShape shape, V3 intersection, V3 rayDirection)
        {
            var shapeColor = shape.GetColor(intersection);
            var pixelColor = shapeColor * new MyColor(.1f, .1f, .1f); // Modele de réflexion ambiant

            V3 normal = shape.GetNormal(intersection);
            float coeffDiffuseLight1 = normal * lights[0].Orientation;
            float coeffDiffuseLight2 = normal * lights[1].Orientation;
            if (coeffDiffuseLight1 >= 0 && !IsIntersect(intersection, lights[0].Orientation, shape))
            {
                pixelColor += coeffDiffuseLight1 * (shapeColor * lights[0].Color); // Modele diffus
                V3 rayReflected = -lights[0].Orientation + 2 * (normal * lights[0].Orientation) * normal;
                rayDirection.Normalize();
                rayReflected.Normalize();
                float coeffSpecular = (float)Math.Pow(rayReflected * (-rayDirection), 98);
                pixelColor += coeffSpecular * lights[0].Color; // Modele speculaire
            }
            // TODO: voir difference de comportement selon key et fill pour pouvoir mettre X lumieres
            if (coeffDiffuseLight2 >= 0 && !IsIntersect(intersection, lights[1].Orientation, shape))
            {
                pixelColor += coeffDiffuseLight2 * (shapeColor * lights[1].Color); // Modele diffus
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
                    if (!shape.IgnoreShadow()) { 
                        intersection = shape.GetIntersection(position, direction);
                        if (intersection != null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static MyColor RayCast(V3 positionCamera, V3 directionRayon, List<IShape> objectsScene, List<Light> lamps)
        {
            // @TODO: Bouger ca c est pas propre :o)
            objects = objectsScene;
            MyColor pixelColor = new MyColor(0,0,0);
            IShape mostClosestShape = null;
            V3 intersection;
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
