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
        MyColor GetColor(V3 intersection);

        /// <summary>
        /// Permet d obtenir le coefficient de reflexion de la forme
        /// </summary>
        /// <returns>Le coefficient de reflexion</returns>
        float GetCoefReflexion();

        V3 GetNormal(V3 intersection = null);
        
        /// <summary>
        /// Permet d ignorer l objet lors de l illumination et
        /// donc ne pas prendre en compte son ombre
        /// </summary>
        /// <returns>
        /// Vrai s il on l ignore sinon faux
        /// </returns>
        bool IgnoreShadow();

        /// <summary>
        /// Verifie si l objet possede un bump
        /// </summary>
        /// <returns>
        /// Vrai s il possede un bump sinon fauxx
        /// </returns>
        bool HasBump();
    }
}
