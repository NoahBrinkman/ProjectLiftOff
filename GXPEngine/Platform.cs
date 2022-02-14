using GXPEngine;
using TiledMapParser;

    public class Platform : AnimationSprite
    {
        public bool beenUsed = false;

        public Platform(string imageFile, int cols = 1, int rows = 1) : base(imageFile, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            collider.isTrigger = true;
        }

        public void Use()
        {
            if(beenUsed) return;
            Level Level = (Level) SceneManager.instance.GetActiveScene();
            Level.score += 100;
            beenUsed = true;
        }

        void Update()
        {
            if(y > game.height + parent.y) LateDestroy();
        }
    }
