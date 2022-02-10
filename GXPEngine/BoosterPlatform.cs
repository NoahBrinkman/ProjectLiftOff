using GXPEngine;
using TiledMapParser;

    public class BoosterPlatform : Platform
    {
        public float speedMultiplier { get; private set; }
        
        public BoosterPlatform(string fileName, float speedMultiplier,int cols = 1, int rows = 1, TiledObject obj = null) : base(fileName,cols,rows)
        {

            this.speedMultiplier = speedMultiplier;
        }


        void Update()
        {
            if (beenUsed)
            {
                Destroy();
            }
        }
    }
