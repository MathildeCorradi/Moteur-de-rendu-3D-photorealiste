﻿using Projet_IMA.utils;
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
            float windowEight = BitmapEcran.GetHeight();
            float windowWidth = BitmapEcran.GetWidth();

            List<IShape> objectsScene = new List<IShape>();
            V3 positionCamera = new V3(windowWidth / 2, -windowWidth, windowEight / 2);
            Couleur lightColor = new Couleur(1f, 1f, 1f);
            Lampe lamp = new Lampe(lightColor, new V3(1, -1, 1), 1f);
            lamp.Orientation.Normalize();

            V3 basGauche = new V3(10,400,0);
            V3 basDroite = new V3(windowWidth-10,400,0);
            V3 hauteGauche = new V3(10,400, windowEight);
            V3 hauteDroite = new V3(windowWidth-10, 400, windowEight);



            var ground = new Parallelogram(new V3(0, 0, 0), basGauche, new V3(windowWidth, 0, 0), Couleur.GROUND);
            var ceilling = new Parallelogram(new V3(0, 0, windowEight), new V3(windowWidth, 0, windowEight), hauteGauche, Couleur.CEILLING);
            var wallBack = new Parallelogram(basGauche, hauteGauche, basDroite, Couleur.WALL_BACK);
            var wallRight = new Parallelogram(basDroite, hauteDroite, new V3(windowWidth, 0, 0), Couleur.WALL_RIGHT);
            var wallLeft = new Parallelogram(new V3(0, 0, 0), new V3(0, 0, windowEight), basGauche, Couleur.WALL_LEFT);
            var sphr = new Sphere(600, 20, 200, 90, Couleur.SPHERE_BLUE);
            var sphr2 = new Sphere(700, 20, 200, 70, Couleur.SPHERE_LIME);
            var sphr3 = new Sphere(500, 300, 20, 100, Couleur.SPHERE_YELLOW);

            objectsScene.Add(wallRight);
            objectsScene.Add(ceilling);
            objectsScene.Add(wallLeft);
            objectsScene.Add(wallBack);
            objectsScene.Add(ground);
            objectsScene.Add(sphr);
            objectsScene.Add(sphr2);
            objectsScene.Add(sphr3);

            for (int xScreen = 0; xScreen <= windowWidth; xScreen++)
            {
                for (int yScreen = 0; yScreen <= windowEight; yScreen++)
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
