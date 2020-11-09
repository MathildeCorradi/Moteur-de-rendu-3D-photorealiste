using Projet_IMA.utils;
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

            List<IShape> ObjectsScene = new List<IShape>();
            int Width = BitmapEcran.GetWidth();
            int Height = BitmapEcran.GetHeight();
            V3 PositionCamera = new V3(Width/2, -Width, Height/2);
            Couleur lightColor = new Couleur(1f, 1f, 1f);
            Lampe lamp = new Lampe(lightColor, new V3(1,-1,1), 1f);

            Parallelogram para = new Parallelogram(new V3(0, 100, 100), new V3(BitmapEcran.GetWidth(), 100, 100), new V3(100, 100, 200), new Couleur(0.55f, 0.27f, 0.07f));
            Parallelogram para2 = new Parallelogram(new V3(BitmapEcran.GetWidth(), 100, 0), new V3(BitmapEcran.GetWidth(), 100, BitmapEcran.GetHeight()), new V3(BitmapEcran.GetWidth() - 100, 100, 100), new Couleur(0.96f, 0.76f, 0.96f));
            Parallelogram para3 = new Parallelogram(new V3(BitmapEcran.GetWidth(), 100, BitmapEcran.GetHeight()), new V3(0, 100, BitmapEcran.GetHeight()), new V3(BitmapEcran.GetWidth() - 100, 100, BitmapEcran.GetHeight() - 100), new Couleur(0.8f, 0.1f, 0.1f));
            Parallelogram para4 = new Parallelogram(new V3(0, 100, BitmapEcran.GetHeight()), new V3(0, 100, 0), new V3(100, 100, BitmapEcran.GetHeight() - 100), new Couleur(0.2f, 0f, 0.7f));
            Parallelogram para5 = new Parallelogram(new V3(0, 150, 0), new V3(BitmapEcran.GetWidth() / 2, 150, 0), new V3(100, 150, 100), new Couleur(0.55f, 0.27f, 0.07f));
            //Parallelogram para6 = new Parallelogram(new V3(100, 50, 100), new V3(BitmapEcran.GetWidth() - 100, 0, 100), new V3(100, 0, BitmapEcran.GetHeight() - 100), new Couleur(0.05f, 0.7f, 1f));
            Sphere sphr = new Sphere(650, 20, 200, 150, new Couleur(0.77f, 1f, 0.52f)); // vert pomme
            Sphere sphr2 = new Sphere(700, 20, 200, 150, new Couleur(0.8f, 0f, 0f)); // rouge
            //Sphere sphr3 = new Sphere(500, 300, 150, 230, new Couleur(0f, 0f, 0.9f)); // bleu

            var r0 = new Parallelogram(new V3(0, 50, 0), new V3(0, 50, 100), new V3(BitmapEcran.GetWidth(), 50, 0), new Couleur(0.55f, 0.27f, 0.07f));
            var r1 = new Parallelogram(new V3(100, 10, 10), new V3(100, 250, 250), new V3(350, 250, 400), new Couleur(.77f, 1f, .52f));

            //Triangle t0 = new Triangle(new V3(0, 50, 0), new V3(0, 50, 100), new V3(BitmapEcran.GetWidth(), 50, 0), new Couleur(0.55f, 0.27f, 0.07f));
            //Triangle t1 = new Triangle(new V3(100, 10, 10), new V3(100, 250, 250), new V3(350, 250, 400), new Couleur(.77f, 1f, .52f));

            //ObjectsScene.Add(t0);
            //ObjectsScene.Add(t1);
            ObjectsScene.Add(para);
            ObjectsScene.Add(para2);
            ObjectsScene.Add(para3);
            ObjectsScene.Add(para4);
            ObjectsScene.Add(r0);
            ObjectsScene.Add(r1);
            ObjectsScene.Add(sphr);
            ObjectsScene.Add(sphr2);
            //ObjectsScene.Add(sphr3);

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
