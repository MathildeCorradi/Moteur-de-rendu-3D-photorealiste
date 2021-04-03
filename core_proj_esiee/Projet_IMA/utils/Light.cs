namespace Projet_IMA.utils
{
    class Light
    {
        /// <summary>
        /// Couleur de la lumiere
        /// </summary>
        MyColor color;

        /// <summary>
        /// Orientation de la lumiere
        /// </summary>
        V3 orientation;

        /// <summary>
        /// Valeur de l affaiblissement de la lumiere
        /// </summary>
        float fading;

        public Light(MyColor color, V3 orientation, float fading)
        {
            this.color = color;
            this.orientation = orientation;
            this.fading = fading;
            Orientation.Normalize();
        }

        public V3 Orientation { get => orientation; set => orientation = value; }

        public MyColor Color { get => color; set => color = value; }

        public float Fading { get => fading; set => fading = value; }
    }
}
