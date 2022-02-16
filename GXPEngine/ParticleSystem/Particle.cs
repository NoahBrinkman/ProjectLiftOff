using System;
using System.Drawing;

namespace GXPEngine
{

    public struct ParticleColor
    {
        public float r;
        public float g;
        public float b;
        public float a;
        
        public ParticleColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.b = b;
            this.g = g;
            this.a = a;
        }
        
        
        
        public ParticleColor LerpColor(ParticleColor s, ParticleColor e, float t)
        {
            return new ParticleColor(
                MathFunctions.Lerp(s.r /255, e.r /255, t),
                MathFunctions.Lerp(s.g/255, e.g/255, t),
                MathFunctions.Lerp(s.b/255, e.b/255, t),
                MathFunctions.Lerp(s.a, e.a, t));
        }
    }
    
    public class Particle : AnimationSprite
    {
        private float lifeTime = 0;
        private float totalLifeTime;
        private float velocityX;
        private float velocityY;
        private ParticleColor startcolor;
        private ParticleColor endColor;
        private bool hasColorOverLifeTime = false;
        public Particle(string fileName, int rows, int cols,int frames,
            float lifeTime = 1, float scale = 1, float velocityX = 0, float velocityY = 0, float rotation = 0) 
            : base(fileName,cols,rows,frames,false,false)
        {
            this.rotation = rotation;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
            this.totalLifeTime= lifeTime;
            this.lifeTime = 0;
            SetScaleXY(scale);
        }
        public Particle(string fileName, int rows, int cols,int frames, Color startColor,
            float lifeTime = 1, float scale = 1, float velocityX = 0, float velocityY = 0, float rotation = 0) 
            : base(fileName,cols,rows,frames,false,false)
        {
            this.rotation = rotation;
            this.velocityX = velocityX;
            this.velocityY = velocityY;
            this.totalLifeTime= lifeTime;
            this.lifeTime = 0;
            SetColor(startColor.R,startColor.G,startColor.B);
            alpha = startColor.A / 255;
            SetScaleXY(scale);
        }
        void Update()
        {
            SetCycle(0,frameCount);
            Move(velocityX,velocityY);

            if (hasColorOverLifeTime)
            {
                ParticleColor p = new ParticleColor(255,255,2555,255);
                p = p.LerpColor(startcolor, endColor, lifeTime / totalLifeTime);
                SetParticleColor(p);
            } 
            
            if (lifeTime >= totalLifeTime)
            {
                LateDestroy();
            }

            lifeTime += (float)Time.deltaTime /1000;
            
        }

        public void SetColorOverLifeTime(Color startingColor, Color endingColor)
        {
            //SetColor(startColor.R,startColor.G,startColor.B);
            hasColorOverLifeTime = true;
            this.startcolor = new ParticleColor(startingColor.R, startingColor.G,startingColor.B,startingColor.A);
            this.endColor = new ParticleColor(endingColor.R, endingColor.G,endingColor.B,endingColor.A);

        }

        private void SetParticleColor(ParticleColor pColor)
        {
            SetColor(pColor.r,pColor.g,pColor.b);
            alpha = pColor.a / 255;
        }
    }
}