using System.Collections.Generic;

namespace GXPEngine
{
    public class PlatformSpawner : GameObject
    {
        private List<Platform> platforms = new List<Platform>();
        private Pivot platformParent;
        private float secondsPerPlatform;
        private float timer;
        private int percentageChanceOfCollapsable;
        private float secondsBeforeCollapse;
        public PlatformSpawner(float secondsPerPlatform, Pivot platformParent, int percentageChanceOfCollapsable, float secondsBeforeCollapse)
        {
            this.secondsPerPlatform = secondsPerPlatform;
            this.platformParent = platformParent;
            this.percentageChanceOfCollapsable = percentageChanceOfCollapsable;
            this.secondsBeforeCollapse = secondsBeforeCollapse;
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
          
            int r = Utils.Random(0, 100);
            var p = r < percentageChanceOfCollapsable ? new CollapsingPlatform("square.png",secondsBeforeCollapse,1,1) 
                : new Platform("square.png",1,1);
            p.SetOrigin(p.width / 2, p.height / 2);
                p.SetScaleXY(2, 2);
                p.SetXY(Utils.Random(0 + p.width / 2, game.width - p.width / 2), -150 - platformParent.y);
                platformParent.AddChildAt(p, 0);
                if (p.GetCollisions().Length > 0)
                {
                    p.Destroy();
                    Spawnplatform();
                }
            
        }
    }
}
