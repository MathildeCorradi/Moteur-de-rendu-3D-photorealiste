namespace Projet_IMA
{
    class Triangle : Parallelogram
    {
        public Triangle(V3 a, V3 b, V3 c, MyColor shapeColor, bool ignoreShadow, Texture textureBump = null, float intensiteBump = 0, float coefReflexion = 0, float coefRefraction = 0) : base(a, b, c, shapeColor, ignoreShadow, textureBump, intensiteBump, coefReflexion, coefRefraction)
        {
        }

        public Triangle(V3 a, V3 b, V3 c, Texture texture, bool ignoreShadow, Texture textureBump = null, float intensiteBump = 0, float coefReflexion = 0, float coefRefraction = 0) : base(a, b, c, texture, ignoreShadow, textureBump, intensiteBump, coefReflexion, coefRefraction)
        {
        }

        public override V3 GetIntersection(V3 positionCamera, V3 dirRayon)
        {
            V3 intersection = base.GetIntersection(positionCamera, dirRayon);
            if (intersection == null) return null;
            V3 AI = intersection - PointA;
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            float u = ((AC ^ Normal) * AI) / (AB ^ AC).Norm();
            float v = ((Normal ^ AB) * AI) / (AC ^ AB).Norm();
            return (u > 1 - v) ? null : intersection;
        }
    }
}
