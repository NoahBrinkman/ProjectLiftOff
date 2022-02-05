using System.Drawing;

namespace GXPEngine
{
    public class Particle : AnimationSprite
    {
        private float lifeTime;
        private float velocityX;
        private float velocityY;
        public Particle(string fileName, int rows, int cols,int frames,
            float lifeTime = 1, float scale = 1, float velocityX = 0, float velocityY = 0, float rotation = 0) 
            : base(fileName,cols,rows,frames,false,false)
        {
            this.rotation = rotation;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
            this.lifeTime = lifeTime;
            SetScaleXY(scale);
        }

        void Update()
        {
            Move(velocityX,velocityY);
            if (lifeTime <= 0)
            {
                LateDestroy();
            }

            lifeTime -= (float)Time.deltaTime /1000;
            
        }
    }
}