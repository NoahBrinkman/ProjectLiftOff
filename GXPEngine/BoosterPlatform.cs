using System.Drawing;

namespace GXPEngine
{
    public class BoosterPlatform : Platform
    {
        public float speedMultiplier { get; private set; }
        private Emitter emitter;
        public BoosterPlatform(string fileName, float speedMultiplier, int percentageToSpawn = 10) : base(fileName)
        {
            this.speedMultiplier = speedMultiplier;
            rotation = Utils.Random(-60, 60);
            emitter = new Emitter("ArrowParticle.png", 1,1,1, 2, 3, BlendMode.NORMAL);
            emitter.SetColorOverLifeTime(Color.HotPink, Color.FromArgb(0,Color.DeepPink));
            emitter.SetVelocity(rotation, rotation, -.1f, -.5f);
            emitter.rotation -= rotation;
            emitter.AttachParticleFromEmitter();
            emitter.SetRandomScale(3, 6);
            emitter.SetSpawnPosition(x + width / 2, y + height / 2, 
                x - width / 2 - 20, x + width / 2 + 10, y , y + height - 200);
            AddChild(emitter);
            SetScaleXY(.75f);
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