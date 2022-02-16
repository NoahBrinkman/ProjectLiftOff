using System;
using System.Drawing;

namespace GXPEngine
{
    public class ObstaclePlatform : Platform
    {
        private Level level;
        private Emitter emitter;
        public ObstaclePlatform(string imageFile, int cols = 1, int rows = 1) : base(imageFile,cols,rows)
        {
            collider.isTrigger = false;
            SetScaleXY(0.64f);
            emitter = new Emitter("StarParticle.png",1,1,1,10,.5f,BlendMode.NORMAL);
            AddChild(emitter);
            emitter.SetVelocity(120, 240, .5f, 1).SetRandomScale(3f, 4f).
                SetColorOverLifeTime(Color.FromArgb(255,Color.LightPink),Color.FromArgb(0,Color.DeepPink));
            level = (Level)SceneManager.instance.GetActiveScene();
            SetCycle(0,2);
        }

        void Update()
        {
            Animate(.01f);
            emitter.SetSpawnPosition
                (x + width / 2, y + height / 2 + parent.y, x - width / 2 + 5, x + width / 2 + 5, y +parent.y + height / 4, y + parent.y +height / 2);
            if (!level.levelIsLost)
            {
                Move(0,.6f);
            }
            if(y > game.height + parent.y) LateDestroy();
        }

    }
}