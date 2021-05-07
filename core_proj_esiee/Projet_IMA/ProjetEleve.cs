using Projet_IMA.utils;
using System.Collections.Generic;
namespace Projet_IMA
{
    static class ProjetEleve
    {
        #region constants

        private static readonly float WindowHeight = Screen.GetHeight();
        private static readonly float WindowWidth = Screen.GetWidth();

        #endregion

        #region positions

        private static readonly V3 BasGauche = new V3(0, 400, 0);
        private static readonly V3 BasDroite = new V3(WindowWidth, 400, 0);
        private static readonly V3 HautGauche = new V3(0, 400, WindowHeight);
        private static readonly V3 HautDroite = new V3(WindowWidth, 400, WindowHeight);

        #endregion

        #region disposition scene

        private static List<IShape> GetSceneObjects() => new List<IShape>
        {
            new Sphere(600, 20, 200, 90, new Texture("gold.jpg"), new Texture("gold_Bump.jpg"), 1),
            new Sphere(700, 20, 200, 70, new Texture("lead.jpg")),
            new Sphere(500, 300, 20, 100, MyColor.SPHERE_YELLOW),
            new Sphere(200, 300, 300, 200, new MyColor(0.5f,0.5f,0.5f), null, 0, 1f),

            new Parallelogram(new V3(0, -WindowWidth-1, 0), new V3(WindowWidth, -WindowWidth-1, 0), new V3(0, -WindowWidth-1, WindowHeight), new MyColor(0f,0f,1f), true),
            new Parallelogram(new V3(0, -WindowWidth-1, 0), new V3(WindowWidth, -WindowWidth-1, 0), BasGauche, MyColor.GROUND, true, null, 0, 0f), //Sol
            new Parallelogram(HautGauche, HautDroite, new V3(0, -WindowWidth-1, WindowHeight), MyColor.CEILLING, true), //Planfond
            new Parallelogram(BasGauche, BasDroite, HautGauche, MyColor.WALL_BACK, true, new Texture("bump4.jpg"), 0.01f), //Mur derrière
            new Parallelogram(BasDroite, new V3(WindowWidth, -WindowWidth-1, 0), HautDroite, MyColor.WALL_RIGHT, true), //mur droit
            new Parallelogram(new V3(0, -WindowWidth-1, 0), BasGauche, new V3(0, -WindowWidth-1, WindowHeight), MyColor.WALL_LEFT, true), //mur gauche

            // new Triangle(new V3(1, 1, 1), new V3(WindowWidth, 1, 1), BasGauche, new Texture("carreau.jpg"), true),
        };

        private static List<Light> GetSceneLights()
        {
            // For the final scene put colors in MyColor file and convert this method to lambda expression
            MyColor keyColor = new MyColor(0.8f, 0.8f, 0.8f);
            MyColor fillColor = new MyColor(0.2f, 0.2f, 0.2f);
            return new List<Light>
            {
                new Light(keyColor, new V3(1, -1, 1), 1f),
                new Light(fillColor, new V3(-1, -1, -1), 1f),
            };
        }

        #endregion

        public static void Display()
        {
            var camera = new V3(WindowWidth / 2, -WindowWidth, WindowHeight / 2);
            var sceneObjects = GetSceneObjects();
            var sceneLights = GetSceneLights();

            for (int xScreen = 0; xScreen <= WindowWidth; xScreen++)
            {
                for (int yScreen = 0; yScreen <= WindowHeight; yScreen++)
                {
                    V3 currentPixelPosition = new V3(xScreen, 0, yScreen);
                    V3 rayDirection = currentPixelPosition - camera;
                    MyColor PixelColor = Screen.RayCast(camera, rayDirection, sceneObjects, sceneLights);
                    Screen.DrawPixel(xScreen, yScreen, PixelColor);
                }
            }
        }
    }
}
