namespace Projet_IMA
{
    /// <summary>
    /// Classe abstraite pour mettre en commun les elements
    /// de chaque forme
    /// </summary>
    public abstract class AbstractShape : IShape
    {
        #region attributs

        /// <summary>
        /// La couleur de la forme
        /// </summary>
        public Couleur ShapeColor { get; set; }

        public Texture Texture { get; set; }

        public Texture TextureBump { get; set; }

        public float IntensiteBump { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la shape
        /// </summary>
        /// <param name="shapeColor"></param>
        public AbstractShape(Couleur shapeColor, Texture textureBump, float intensiteBump)
        {
            ShapeColor = shapeColor;
            TextureBump = textureBump;
            IntensiteBump = intensiteBump;
        }

        public AbstractShape(Texture texture, Texture textureBump, float intensiteBump)
        {
            Texture = texture;
            TextureBump = textureBump;
            IntensiteBump = intensiteBump;
        }

        #endregion

        #region methodes

        public abstract V3 GetIntersection(V3 positionCamera, V3 dirRayon);

        public abstract Couleur GetColor(V3 intersection);

        public abstract V3 GetNormal(V3 intersection = null);

        public abstract V3 GetNormalBump(V3 intersection = null);

        public abstract bool hasBump();

        public virtual bool isLightFlag()
        {
            return false;
        }

        #endregion
    }
}
