using GXPEngine;
using TiledMapParser;

    public class Platform : AnimationSprite
    {
        public bool beenUsed = false;
        public Platform(TiledObject obj = null) : base("square.png", 1, 1)
        {
            Initiazlize(obj);
        }

        public Platform(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
        {
            Initiazlize(obj);
        }


        void Initiazlize(TiledObject obj = null)
        {
            SetOrigin(width / 2, height / 2);
            collider.isTrigger = true;
        }
    }
