using System.Drawing;
using System.Drawing.Imaging;

namespace Projet_IMA
{
    /// <summary>
    /// Represente une texture
    /// Les fichiers textures sont dans le repertoire textures
    /// du projet
    /// </summary>
    class Texture
    {
        #region attributs

        /// <summary>
        /// Hauteur de la texture
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Longueur de la texture
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Tableau a 2 dimensions representant les couleurs
        /// de la textures
        /// </summary>
        Couleur[,] Color { get; set; }

        #endregion

        #region constructeur

        /// <summary>
        /// Constructeur de la texture
        /// Permet de lire un fichier se trouvant dans le dossier
        /// textures du projet
        /// </summary>
        /// <param name="filename">Le nom du fichier a charger</param>
        public Texture(string filename)
        {
            string basePath = System.IO.Path.GetFullPath("..\\..");
            string fullPath = System.IO.Path.Combine(basePath, "textures", filename);
            Bitmap bitmap = new Bitmap(fullPath);

            Height = bitmap.Height;
            Width = bitmap.Width;
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bitmapData.Stride;
            Color = new Couleur[Width, Height];

            unsafe
            {
                byte* ptr = (byte*)bitmapData.Scan0;
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        byte red, green, blue;
                        blue = ptr[(x * 3) + y * stride];
                        green = ptr[(x * 3) + y * stride + 1];
                        red = ptr[(x * 3) + y * stride + 2];
                        Color[x, y].From255(red, green, blue);
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
            bitmap.Dispose();
        }

        #endregion

        #region methodes

        /// <summary>
        /// Permet de lire une couleur avec u et v
        /// compris entre 0 et 1
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns>La couleur</returns>
        public Couleur ReadColor(float u, float v)
        {
            return Interpol(Width * u, Height * v);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="dhdu"></param>
        /// <param name="dhdv"></param>
        public void Bump(float u, float v, out float dhdu, out float dhdv)
        {
            float x = u * Height;
            float y = v * Width;

            float vv = Interpol(x, y).GreyLevel();
            float vx = Interpol(x + 1, y).GreyLevel();
            float vy = Interpol(x, y + 1).GreyLevel();

            dhdu = vx - vv;
            dhdv = vy - vv;
        }

        /// <summary>
        /// Permet d obtenir une couleur d apres u et v
        /// </summary>
        /// <param name="Lu"></param>
        /// <param name="Hv"></param>
        /// <returns></returns>
        private Couleur Interpol(float Lu, float Hv)
        {
            int x = (int)Lu;  // plus grand entier <=
            int y = (int)Hv;

            //  float cx = Lu - x; // reste
            //  float cy = Hv - y;

            x %= Width;
            y %= Height;
            if (x < 0) x += Width;
            if (y < 0) y += Height;

            return Color[x, y];

            /*
            int xpu = (x + 1) % Largeur;
            int ypu = (y + 1) % Hauteur;

            float ccx = cx * cx;
            float ccy = cy * cy;

            return
              C[x, y] * (1 - ccx) * (1 - ccy)
            + C[xpu, y] * ccx * (1 - ccy)
            + C[x, ypu] * (1 - ccx) * ccy
            + C[xpu, ypu] * ccx * ccy;
            */
        }

        #endregion
    }
}
