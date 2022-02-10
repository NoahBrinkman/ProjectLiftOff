using GXPEngine;
using TiledMapParser;

    public class Platform : AnimationSprite
    {
        float gravity = 0.3f;
        public bool beenUsed = false;

        public Platform(string imageFile, int cols = 1, int rows = 1, TiledObject obj = null) : base(imageFile, cols, rows)
        {
        }

        void Initiazlize(TiledObject obj = null)
        {
            SetOrigin(width / 2, height / 2);
            collider.isTrigger = true;
        }

        void Update()
        {
            if(y > game.height + parent.y) LateDestroy();
        }
    }
