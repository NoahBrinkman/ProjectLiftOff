using System.Collections.Generic;

namespace GXPEngine
{
    public class PlatformSpawner : GameObject
    {
        private List<Platform> platforms = new List<Platform>();
        private Pivot platformParent;
        private float secondsPerPlatform;
        private float timer;
        public PlatformSpawner(float secondsPerPlatform, Pivot platformParent)
        {
            this.secondsPerPlatform = secondsPerPlatform;
            this.platformParent = platformParent;
            timer = secondsPerPlatform;
        }

        void Update()
        {
            timer -= (float)Time.deltaTime / 1000;
            if (timer <= 0)
            {
                Spawnplatform();
                timer = secondsPerPlatform;
            }
        }
        
        private void Spawnplatform()
        {
            Platform p = new Platform("square.png");
            p.SetOrigin(p.width/2,p.height/2);
            p.SetScaleXY(2,2);
            p.SetXY(Utils.Random(0 + p.width / 2, game.width - p.width / 2), -150 - platformParent.y);
            platformParent.AddChildAt(p, 0);
        }

    }
}