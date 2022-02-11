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
        private int percentageForBooster;
        private int percentageForObstacle;
        public PlatformSpawner(float secondsPerPlatform, Pivot platformParent, int percentageChanceOfCollapsable,
            float secondsBeforeCollapse, int percentageForBooster = 10,int percentageForObstacle = 10)
        {
            this.secondsPerPlatform = secondsPerPlatform;
            this.platformParent = platformParent;
            this.percentageChanceOfCollapsable = percentageChanceOfCollapsable;
            this.secondsBeforeCollapse = secondsBeforeCollapse;
            this.percentageForBooster = percentageForBooster;
            this.percentageForObstacle = percentageForObstacle;
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
            {

                int r = Utils.Random(0, 100);
                var p = new Platform("square.png");
                if (r < percentageChanceOfCollapsable) p = new CollapsingPlatform("square.png", secondsBeforeCollapse);
                else if (r > percentageChanceOfCollapsable && r < percentageChanceOfCollapsable + percentageForBooster)
                {
                    p = new BoosterPlatform("triangle.png", 1.6f);
                    p.alpha = .5f;
                    p.rotation = Utils.Random(-60, 60);
                }else if(r > percentageChanceOfCollapsable + percentageForBooster && r < percentageChanceOfCollapsable + percentageForBooster + percentageForObstacle)

                {
                    p = new ObstaclePlatform("square.png");
                    platformParent.AddChild(p);
                }

                p.SetOrigin(p.width / 2, p.height / 2);
                p.SetXY(Utils.Random(0 + p.width / 2, game.width - p.width / 2), -150 - platformParent.y);
                if (!(p is BoosterPlatform) && !(p is ObstaclePlatform))
                {
                    p.SetScaleXY(2, 2);
                    platformParent.AddChildAt(p, 0);
                }

                if (p.GetCollisions().Length > 0)
                {
                    p.Destroy();
                    //Spawnplatform();
                }

            }
        }
    }
}
