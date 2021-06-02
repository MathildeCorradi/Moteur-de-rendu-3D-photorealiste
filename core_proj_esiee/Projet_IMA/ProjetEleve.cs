using Projet_IMA.utils;
using System.Collections.Generic;
namespace Projet_IMA
{
    static class ProjetEleve
    {
        #region constants

        private static readonly float WindowHeight = Screen.GetHeight();
        private static readonly float WindowWidth = Screen.GetWidth();

        private static readonly int REFLEXION_NUMBER = 5;
        private static readonly int REFRACTION_NUMBER = 5;

        #endregion

        #region positions

        private static readonly V3 BasGauche = new V3(0, 400+200, 0);
        private static readonly V3 BasDroite = new V3(WindowWidth, 400+200, 0);
        private static readonly V3 HautGauche = new V3(0, 400+200, WindowHeight);
        private static readonly V3 HautDroite = new V3(WindowWidth, 400+200, WindowHeight);

        #endregion

        #region disposition scene

        private static List<IShape> GetSceneObjects() => new List<IShape>
        {
            //new Sphere(600, 20, 200, 90, new Texture("gold.jpg"), new Texture("gold_Bump.jpg"), 1),
            //new Sphere(700, 20, 200, 70, new Texture("lead.jpg")),
            //new Sphere(500, 300, 20, 100, MyColor.SPHERE_YELLOW),
            new Sphere(200, 300, 200, 200, new MyColor(0.8f,0.5f,0.5f), null, 0, 1f),
            new Sphere(800, 300, 200, 50, new MyColor(0.5f,0.5f,0.8f), null, 0, .6f),
            new Sphere(500, 400, 300, 100, new MyColor(1f, .6f, .8f), new Texture("bump4.jpg"), 5f, 0.1f),
            new Sphere(700, 500, 200, 70, new MyColor(1f, 1f, 1f), null, 0, 0, 0.001f, Fresnel.WATER),

            new Parallelogram(new V3(0, -WindowWidth-1, 0), new V3(WindowWidth, -WindowWidth-1, 0), new V3(0, -WindowWidth-1, WindowHeight), new MyColor(.5f,.5f,.5f), true),
            new Parallelogram(new V3(0, -WindowWidth-1, 0), new V3(WindowWidth, -WindowWidth-1, 0), BasGauche, new Texture("tiles4.jpg"), true, new Texture("tiles5.jpg"), 0.001f, 0.3f, 0), //Sol
            new Parallelogram(HautGauche, HautDroite, new V3(0, -WindowWidth-1, WindowHeight), MyColor.CEILLING, true, null, 0, 0.1f), //Plafond
            new Parallelogram(BasGauche, BasDroite, HautGauche, new Texture("luxury2.png"), true, null, 0, 0.06f, 0), //Mur derrière
            new Parallelogram(BasDroite, new V3(WindowWidth, -WindowWidth-1, 0), HautDroite, MyColor.WALL_RIGHT, true, new Texture("stone3.jpg"), 0.002f, 0.1f, 0), //mur droit
            new Parallelogram(new V3(0, -WindowWidth-1, 0), BasGauche, new V3(0, -WindowWidth-1, WindowHeight), MyColor.WALL_LEFT, true, new Texture("wall2.jpg"), 0.001f, 0.1f, 0) //mur gauche

            // new Triangle(new V3(1, 1, 1), new V3(WindowWidth, 1, 1), BasGauche, new Texture("carreau.jpg"), true),
        };

        private static List<Light> GetSceneLights()
        {
            // For the final scene put colors in MyColor file and convert this method to lambda expression
            MyColor keyColor = new MyColor(0.8f, 0.8f, 0.8f);
            MyColor fillColor = new MyColor(0.4f, 0.4f, 0.4f);
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
                    MyColor PixelColor = Screen.RayCast(camera, rayDirection, sceneObjects, sceneLights, REFLEXION_NUMBER, REFRACTION_NUMBER);
                    Screen.DrawPixel(xScreen, yScreen, PixelColor);
                }
            }
        }
    }
}
