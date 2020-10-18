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

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la 
        /// </summary>
        /// <param name="shapeColor"></param>
        public AbstractShape(Couleur shapeColor)
        {
            ShapeColor = shapeColor;
        }

        #endregion

        #region methodes

        public abstract V3 GetIntersection();

        public abstract void Draw();

        #endregion
    }
}
