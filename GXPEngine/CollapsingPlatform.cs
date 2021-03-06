using GXPEngine;
using TiledMapParser;
    public class CollapsingPlatform : Platform
    {
        private float secondsBeforeCollapse;
        private float timer;
        private float gravity = 0.3f;
        public CollapsingPlatform(string fileName, float secondsBeforeCollapse,int cols = 1,int rows = 1, TiledObject obj = null) : base(fileName,cols,rows)
        {
            SetColor(255, 255, 0);
            this.secondsBeforeCollapse = secondsBeforeCollapse;
            timer = secondsBeforeCollapse;
        }

        void Update()
        {
            if (beenUsed)
            {
                timer -= (float)Time.deltaTime / 1000;
                if (timer <= 0)
                {
                    LateDestroy();
                }
            }
        }
        
    }