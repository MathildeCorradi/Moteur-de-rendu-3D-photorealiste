namespace Projet_IMA
{
    class Triangle : Parallelogram
    {
        public Triangle(V3 a, V3 b, V3 c, Couleur shapeColor, Texture textureBump = null) : base(a, b, c, shapeColor, textureBump)
        {
        }

        public Triangle(V3 a, V3 b, V3 c, Texture texture, Texture textureBump = null) : base(a, b, c, texture, textureBump)
        {
        }

        public V3 paraPoint(float u, float v)
        {
            V3 AB = PointB - PointA;
            V3 AC = PointC - PointA;
            return new V3(PointA + u * AB + v * AC);
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
