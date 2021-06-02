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

        public float CoefRefraction { get; set; }

        public float CoefReflexion { get; set; }

        public float IndiceFresnel { get; set; }

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la shape
        /// </summary>
        /// <param name="shapeColor"></param>
        public AbstractShape(MyColor shapeColor, Texture bumpTexture, float bumpIntensity, float coefReflexion, float coefRefraction, float indiceFresnel )
        {
            ShapeColor = shapeColor;
            BumpTexture = bumpTexture;
            BumpIntensity = bumpIntensity;
            CoefRefraction = coefRefraction;
            CoefReflexion = coefReflexion;
            IndiceFresnel = indiceFresnel; 
        }

        public AbstractShape(Texture texture, Texture bumpTexture, float bumpIntensity, float coefReflexion, float coefRefraction, float indiceFresnel)
        {
            Texture = texture;
            BumpTexture = bumpTexture;
            BumpIntensity = bumpIntensity;
            CoefRefraction = coefRefraction;
            CoefReflexion = coefReflexion;
            IndiceFresnel = indiceFresnel;
        }

        #endregion

        #region methodes

        public abstract V3 GetIntersection(V3 positionCamera, V3 dirRayon);

        public abstract MyColor GetColor(V3 intersection);

        public float GetCoefReflexion()
        {
            return CoefReflexion;
        }

        public float GetCoefRefraction()
        {
            return CoefRefraction;
        }

        public float GetIndiceFresnel()
        {
            return IndiceFresnel;
        }

        public abstract V3 GetNormal(V3 intersection = null);
        public bool HasBump() => BumpTexture != null;

        public virtual bool IgnoreShadow() => false;

        #endregion
    }
}
