using GXPEngine;
using TiledMapParser;
    public class CollapsingPlatform : Platform
    {
        private float secondsBeforeCollapse;
        private float timer;

        public CollapsingPlatform(TiledObject obj = null) : base("square.png", 1, 1)
        {
            Initialize(obj);
        }
        public CollapsingPlatform(string fileName, float secondsBeforeCollapse,int cols,int rows, TiledObject obj = null) : base(fileName,cols,rows)
        {         
            Initialize(obj);
            this.secondsBeforeCollapse = secondsBeforeCollapse;
            timer = secondsBeforeCollapse;
        }

        void Initialize(TiledObject obj = null)
        {
            SetColor(255, 255, 0);
            if(obj != null)
            {
                secondsBeforeCollapse = obj.GetFloatProperty("secondsBeforeCollapse", 2.5f);
            }
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