namespace GXPEngine
{
    public class BoosterPlatform : Platform
    {
        public float speedMultiplier { get; private set; }
        
        public BoosterPlatform(string fileName, float speedMultiplier) : base(fileName)
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
}