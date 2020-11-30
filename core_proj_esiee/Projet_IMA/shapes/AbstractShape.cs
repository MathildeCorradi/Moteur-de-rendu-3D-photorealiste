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

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la shape
        /// </summary>
        /// <param name="shapeColor"></param>
        public AbstractShape(Couleur shapeColor)
        {
            ShapeColor = shapeColor;
        }

        public AbstractShape(Texture texture)
        {
            Texture = texture;
        }

        #endregion

        #region methodes

        public abstract V3 GetIntersection(V3 positionCamera, V3 dirRayon);

        public abstract Couleur GetColor(V3 intersection);

        public abstract V3 GetNormal(V3 intersection = null);

        #endregion
    }
}
