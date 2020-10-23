using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {

            /*Texture T1 = new Texture("brick01.jpg");
           
            int larg = 600;
            int haut = 300;
            float r_x = 1.5f;   // repetition de la texture en x
            float r_y = 1.0f;   // repetition de la texture en y
            float pas = 0.001f;

            for (float u = 0 ; u < 1 ; u+=pas)  // echantillonage fnt paramétrique
            for (float v = 0 ; v < 1 ; v+=pas)
                {
                    int x = (int) (u * larg + 10); // calcul des coordonnées planes
                    int y = (int) (v * haut + 15);

                    Couleur c = T1.LireCouleur(u * r_x, v * r_y);
                    
                    BitmapEcran.DrawPixel(x,y,c );
                   
                }

            // dessin sur l'image pour comprendre l'orientation axe et origine du Bitmap
            
            Couleur Red = new Couleur(1.0f, 0.0f, 0.0f);
            for (int i = 0; i < 1000; i++)
                BitmapEcran.DrawPixel(i, i, Red);

            Couleur Green = new Couleur(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < 1000; i++)
                BitmapEcran.DrawPixel(i, 1000-i, Green);

            // test des opérations sur les vecteurs

            V3 t = new V3(1, 0, 0);
            V3 r = new V3(0, 1, 0);
            V3 k = t + r;
            float p = k * t * 2;
            V3 n = t ^ r;
            V3 m = -t;*/

            List<IShape> ObjectsScene = new List<IShape>();
            int Width = BitmapEcran.GetWidth();
            int Height = BitmapEcran.GetHeight();
            V3 PositionCamera = new V3(Width/2, -Width, Height/2);


            //Parallelogram para = new Parallelogram(new V3(0, 50, 0), new V3(BitmapEcran.GetWidth(), 0, 0), new V3(100, 0, 100), new Couleur(0.55f, 0.27f, 0.07f));
            //Parallelogram para2 = new Parallelogram(new V3(BitmapEcran.GetWidth(), 0, 0), new V3(BitmapEcran.GetWidth(), 0, BitmapEcran.GetHeight()), new V3(BitmapEcran.GetWidth() - 100, 0, 100), new Couleur(0.96f, 0.76f, 0.96f));
            //Parallelogram para3 = new Parallelogram(new V3(BitmapEcran.GetWidth(), 0, BitmapEcran.GetHeight()), new V3(0, 0, BitmapEcran.GetHeight()), new V3(BitmapEcran.GetWidth() - 100, 0, BitmapEcran.GetHeight() - 100), new Couleur(0.8f, 0.1f, 0.1f));
            //Parallelogram para4 = new Parallelogram(new V3(0, 0, BitmapEcran.GetHeight()), new V3(0, 0, 0), new V3(100, 0, BitmapEcran.GetHeight() - 100), new Couleur(0.2f, 0f, 0.7f));
            //Parallelogram para5 = new Parallelogram(new V3(0, 50, 0), new V3(BitmapEcran.GetWidth() / 2, 0, 0), new V3(100, 0, 100), new Couleur(0.55f, 0.27f, 0.07f));
            //Parallelogram para6 = new Parallelogram(new V3(100, 50, 100), new V3(BitmapEcran.GetWidth() - 100, 0, 100), new V3(100, 0, BitmapEcran.GetHeight() - 100), new Couleur(0.05f, 0.7f, 1f));
            Sphere sphr = new Sphere(500, 100, 150, 70, new Couleur(0.77f, 1f, 0.52f)); // vert pomme
            Sphere sphr2 = new Sphere(500, 200, 150, 150, new Couleur(0.8f, 0f, 0f)); // rouge
            Sphere sphr3 = new Sphere(500, 300, 150, 230, new Couleur(0f, 0f, 0.9f)); // bleu

            var r0 = new Parallelogram(new V3(0, 50, 0), new V3(0, 50, 100), new V3(BitmapEcran.GetWidth(), 50, 0), new Couleur(0.55f, 0.27f, 0.07f));
            var r1 = new Parallelogram(new V3(100, 10, 10), new V3(100, 250, 250), new V3(350, 250, 400), new Couleur(.77f, 1f, .52f));

            Triangle t0 = new Triangle(new V3(0, 50, 0), new V3(0, 50, 100), new V3(BitmapEcran.GetWidth(), 50, 0), new Couleur(0.55f, 0.27f, 0.07f));
            Triangle t1 = new Triangle(new V3(100, 10, 10), new V3(100, 250, 250), new V3(350, 250, 400), new Couleur(.77f, 1f, .52f));

            ObjectsScene.Add(t0);
            ObjectsScene.Add(t1);
            /*ObjectsScene.Add(r0);
            ObjectsScene.Add(r1);
            ObjectsScene.Add(sphr);
            ObjectsScene.Add(sphr2);
            ObjectsScene.Add(sphr3);*/

            /*para.Draw();
            para2.Draw();
            para3.Draw();
            para4.Draw();
            para5.Draw();
            para6.Draw();
            sphr.Draw();
            sphr2.Draw();
            sphr3.Draw();*/

            // A decommenter a la fin de refactoring
            for (int x_ecran = 0; x_ecran <= Width; x_ecran++) {
                for (int y_ecran = 0; y_ecran <= Height; y_ecran++)
                {
                    V3 PositionPixelScene = new V3(x_ecran, 0, y_ecran);
                    V3 DirectionRayon = PositionPixelScene - PositionCamera;
                    Couleur PixelColor = BitmapEcran.RayCast(PositionCamera, DirectionRayon, ObjectsScene);
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, PixelColor);
                } 
            }
            

        }
    }
}
