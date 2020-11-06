namespace Projet_IMA
{
    /// <summary>
    /// Interface qui represente toutes les formes
    /// pouvant etre present dans une scene
    /// </summary>
    interface IShape
    {
        /// <summary>
        /// Permet d obtenir l intersection d un vecteur
        /// </summary>
        /// <returns>Le point d intersection</returns>
        V3 GetIntersection(V3 positionCamera, V3 dirRayon);

        /// <summary>
        /// Permet d obtenir la couleur de la forme
        /// </summary>
        /// <returns>Un objet Couleur</returns>
        Couleur GetColor();
    }
}
