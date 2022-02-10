using GXPEngine;
using TiledMapParser;

    public class BoosterPlatform : Platform
    {
        public float speedMultiplier { get; private set; }

    public BoosterPlatform(TiledObject obj = null) : base(null, 1, 1)
        {
            Initialize(obj);
        }
        
        public BoosterPlatform(string fileName, float speedMultiplier,int cols, int rows, TiledObject obj = null) : base(fileName,cols,rows)
        {
            Initialize(obj);
            this.speedMultiplier = speedMultiplier;
        }

        void Initialize(TiledObject obj = null)
        {
            
        }

        void Update()
        {
            if (beenUsed)
            {
                Destroy();
            }
        }
    }
