using Projet_IMA.utils;
using System.Collections.Generic;
namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            #region Dimensions
            float windowHeight = BitmapEcran.GetHeight();
            float windowWidth = BitmapEcran.GetWidth();
            #endregion

            #region Colors
            Couleur keyColor = new Couleur(0.8f, 0.8f, 0.8f);
            Couleur fillColor = new Couleur(0.2f, 0.0f, 0.2f);
            #endregion

            #region Lightings / Lamps
            Lampe keyLamp = new Lampe(keyColor, new V3(1, -1, 1), 1f);
            Lampe fillLamp = new Lampe(fillColor, new V3(-1, -1, -1), 1f);

            keyLamp.Orientation.Normalize();
            fillLamp.Orientation.Normalize();

            List<Lampe> sceneLamps = new List<Lampe>()
            {
                keyLamp,
                fillLamp
            };
            #endregion

            #region Positions
            V3 bottomLeft = new V3(0, 400, 0);
            V3 bottomRight = new V3(windowWidth, 400, 0);
            V3 topLeft = new V3(0, 400, windowHeight);
            V3 topRight = new V3(windowWidth, 400, windowHeight);
            #endregion

            #region Spheres
            var sphere1 = new Sphere(600, 20, 200, 90, new Texture("gold.jpg"), new Texture("gold_Bump.jpg"), 1);
            var sphere2 = new Sphere(700, 20, 200, 70, new Texture("lead.jpg"));
            var sphere3 = new Sphere(500, 300, 20, 100, Couleur.SPHERE_YELLOW);
            var sphere4 = new Sphere(200, 300, 300, 200, new Couleur(1f,1f,1f), new Texture("bump4.jpg"), 2);
            #endregion

            #region Walls
            var groundWall = new Parallelogram(new V3(0, 0, 0), new V3(windowWidth, 0, 0), bottomLeft, Couleur.GROUND, true);
            var ceillingWall = new Parallelogram(topLeft, topRight, new V3(0, 0, windowHeight), Couleur.CEILLING, true);
            var backWall = new Parallelogram(bottomLeft, bottomRight, topLeft, Couleur.WALL_BACK, true,  new Texture("bump4.jpg"), 0.01f);
            var rightWall = new Parallelogram(bottomRight, new V3(windowWidth, 0, 0), topRight, Couleur.WALL_RIGHT,true, new Texture("bump38.jpg"), 0.1f);
            var leftWall = new Parallelogram(new V3(0, 0, 0), bottomLeft, new V3(0, 0, windowHeight), Couleur.WALL_LEFT, true);
            #endregion

            #region Scene Objects
            List<IShape> sceneObjects = new List<IShape>()
            {
                rightWall,
                ceillingWall,
                leftWall,
                backWall,
                groundWall,
                sphere1,
                sphere2,
                sphere3,
                sphere4
            };
            #endregion

            #region Drawing
            V3 cameraPosition = new V3(windowWidth / 2, -windowWidth, windowHeight / 2);
            for (int xScreen = 0; xScreen <= windowWidth; xScreen++)
            {
                for (int yScreen = 0; yScreen <= windowHeight; yScreen++)
                {
                    V3 pixelPosition = new V3(xScreen, 0, yScreen);
                    V3 rayonDirection = pixelPosition - cameraPosition;
                    Couleur pixelColor = BitmapEcran.RayCast(cameraPosition, rayonDirection, sceneObjects, sceneLamps);
                    BitmapEcran.DrawPixel(xScreen, yScreen, pixelColor);
                }
            }
            #endregion

        }
    }
}