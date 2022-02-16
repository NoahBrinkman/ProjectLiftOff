using System;
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
        private int percentageForBouncy = 10;
        public PlatformSpawner(float secondsPerPlatform, Pivot platformParent, int percentageChanceOfCollapsable,
            float secondsBeforeCollapse, int percentageForBooster = 10,int percentageForObstacle = 10, int percentageForBouncy = 10)
        {
            this.secondsPerPlatform = secondsPerPlatform;
            this.platformParent = platformParent;
            this.percentageChanceOfCollapsable = percentageChanceOfCollapsable;
            this.secondsBeforeCollapse = secondsBeforeCollapse;
            this.percentageForBooster = percentageForBooster;
            this.percentageForObstacle = percentageForObstacle;
            this.percentageForBouncy = percentageForBouncy;
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
                var p = new Platform("Meteor.png");
                if (r <= percentageChanceOfCollapsable)
                {
                    p = new CollapsingPlatform("CollapsableAstroid.png", secondsBeforeCollapse,13,1);
                }else if (r <= (percentageChanceOfCollapsable + percentageForBooster))
                {
                    p = new BoosterPlatform("BoosterShot.png", 1.6f);
                    p.SetScaleXY(0.32f);
                    Console.WriteLine("Hi");
                    platformParent.AddChildAt(p,0);
                }else if (r <= (percentageChanceOfCollapsable + percentageForBooster + percentageForObstacle))
                {
                    p = new ObstaclePlatform("Obstacle.png",2,1);
                    platformParent.AddChild(p);
                }else if (r <= (percentageChanceOfCollapsable + percentageForBooster + percentageForObstacle +
                                percentageForBouncy))
                {
                    p = new BouncyPlatform("BouncingPlatform.png");
                    platformParent.AddChild(p);
                }

                p.SetOrigin(p.width / 2, p.height / 2);
                p.SetXY(Utils.Random(0 + p.width / 2, game.width - p.width / 2), -150 - platformParent.y);
                if (!(p is BoosterPlatform) && !(p is ObstaclePlatform)&& !(p is BouncyPlatform))
                {
                    p.SetScaleXY(0.12f);
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
