using System;

namespace GXPEngine
{
    public class ObstaclePlatform : Platform
    {
        private Level level;
        public ObstaclePlatform(string imageFile, int cols = 1, int rows = 1) : base(imageFile,cols,rows)
        {
            collider.isTrigger = false;
            SetScaleXY(1,2);
            SetColor(125,0,0);
            level = (Level)SceneManager.instance.GetActiveScene();
        }

        void Update()
        {
            
            if (!level.levelIsLost)
            {
                Move(0,.6f);
            }
            if(y > game.height + parent.y) LateDestroy();
        }

    }
}