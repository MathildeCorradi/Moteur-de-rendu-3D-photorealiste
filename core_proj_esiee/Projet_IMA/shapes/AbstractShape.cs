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
        public MyColor ShapeColor { get; set; }

        public Texture Texture { get; set; }

        public Texture BumpTexture { get; set; }

        public float BumpIntensity { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la shape
        /// </summary>
        /// <param name="shapeColor"></param>
        public AbstractShape(MyColor shapeColor, Texture bumpTexture, float bumpIntensity)
        {
            ShapeColor = shapeColor;
            BumpTexture = bumpTexture;
            BumpIntensity = bumpIntensity;
        }

        public AbstractShape(Texture texture, Texture bumpTexture, float bumpIntensity)
        {
            Texture = texture;
            BumpTexture = bumpTexture;
            BumpIntensity = bumpIntensity;
        }

        #endregion

        #region methodes

        public abstract V3 GetIntersection(V3 positionCamera, V3 dirRayon);

        public abstract MyColor GetColor(V3 intersection);

        // @TODO: Voir si on peux factoriser les GetNormal

        public abstract V3 GetNormal(V3 intersection = null);

        public abstract V3 GetNormalBump(V3 intersection = null);

        public bool HasBump() => BumpTexture != null;

        public virtual bool IgnoreShadow() => false;

        #endregion
    }
}
