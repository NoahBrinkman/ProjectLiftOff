namespace GXPEngine
{
    public class CollapsingPlatform : Platform
    {
        private float secondsBeforeCollapse;
        private float timer;
        
        public CollapsingPlatform(string fileName, float secondsBeforeCollapse) : base(fileName)
        {
            this.secondsBeforeCollapse = secondsBeforeCollapse;
            SetColor(255,255,0);
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
}