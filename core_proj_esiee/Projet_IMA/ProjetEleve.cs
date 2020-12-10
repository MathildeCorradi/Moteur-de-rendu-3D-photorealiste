using Projet_IMA.utils;
using System.Collections.Generic;
namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            float windowHeight = BitmapEcran.GetHeight();
            float windowWidth = BitmapEcran.GetWidth();

            List<IShape> objectsScene = new List<IShape>();
            V3 positionCamera = new V3(windowWidth / 2, -windowWidth, windowHeight / 2);
            Couleur lightColor = new Couleur(1f, 1f, 1f);
            Lampe lamp = new Lampe(lightColor, new V3(1, -1, 1), 1f);
            lamp.Orientation.Normalize();

            V3 basGauche = new V3(0, 400, 0);
            V3 basDroite = new V3(windowWidth, 400, 0);
            V3 hauteGauche = new V3(0, 400, windowHeight);
            V3 hauteDroite = new V3(windowWidth, 400, windowHeight);

            var sphr = new Sphere(600, 20, 200, 90, new Texture("gold.jpg"), new Texture("gold_Bump.jpg"));
            var sphr2 = new Sphere(700, 20, 200, 70, new Texture("lead.jpg"));
            var sphr3 = new Sphere(500, 300, 20, 100, Couleur.SPHERE_YELLOW);

            var ground = new Parallelogram(new V3(0, 0, 0), new V3(windowWidth, 0, 0), basGauche, Couleur.GROUND);
            var ceilling = new Parallelogram(hauteGauche, hauteDroite, new V3(0, 0, windowHeight), Couleur.CEILLING);
            var wallBack = new Parallelogram(basGauche, basDroite, hauteGauche, Couleur.WALL_BACK);
            var wallRight = new Parallelogram(basDroite, new V3(windowWidth, 0, 0), hauteDroite, Couleur.WALL_RIGHT);
            var wallLeft = new Parallelogram(new V3(0, 0, 0), basGauche, new V3(0, 0, windowHeight), Couleur.WALL_LEFT);

            objectsScene.Add(wallRight);
            objectsScene.Add(ceilling);
            objectsScene.Add(wallLeft);
            objectsScene.Add(wallBack);
            objectsScene.Add(ground);
            objectsScene.Add(sphr);
            objectsScene.Add(sphr2);
            objectsScene.Add(sphr3);

            // objectsScene.Add(new Triangle(new V3(0, 0, 0), new V3(windowWidth, 0, 0), basGauche, new Texture("carreau.jpg")));

            for (int xScreen = 0; xScreen <= windowWidth; xScreen++)
            {
                for (int yScreen = 0; yScreen <= windowHeight; yScreen++)
                {
                    V3 positionPixelScene = new V3(xScreen, 0, yScreen);
                    V3 directionRayon = positionPixelScene - positionCamera;
                    Couleur PixelColor = BitmapEcran.RayCast(positionCamera, directionRayon, objectsScene, lamp);
                    BitmapEcran.DrawPixel(xScreen, yScreen, PixelColor);
                }
            }

        }
    }
}