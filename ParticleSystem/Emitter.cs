using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Emitter : GameObject
    {
        private int particlesPerSecond;
        private float particleLifeTime;
        private string particleFileName;
        private int cols;
        private int rows;
        private int frames;
        private float minAngle =0;
        private float maxAngle = 0;
        private float minSpeedPerParticle = 0;
        private float maxSpeedPerParticle = 0;
        private float minScale = 1;
        private float maxScale = 1;
        private Color startingColor;
        private Color endColor;

        private int minimumRangeX;
        private int maximumRangeX;
        private int minimumRangeY;
        private int maximumRangeY;
        
        private int fps;
        private float timer = 1;
        private int frameCounter = 0;
        public Emitter(string fileName, int columns, int rows, int frames, int particlesPerSecond, float particleLiftTime, BlendMode blendMode)
        {
            this.particleFileName = fileName;
            this.cols = columns;
            this.rows = rows;
            this.frames = frames;
            this.particlesPerSecond = particlesPerSecond;
            this.particleLifeTime = particleLiftTime;
        }

        void Update()
        {
            Random r = new Random();
            Particle p = new Particle(particleFileName, rows, cols, frames, particleLifeTime,
                    RandomFloat(minScale, maxScale),
                    RandomFloat(minSpeedPerParticle, maxSpeedPerParticle),
                    RandomFloat(minSpeedPerParticle, maxSpeedPerParticle), RandomFloat(minAngle, maxAngle));
            p.SetXY(r.Next(minimumRangeX, maximumRangeX),r.Next(minimumRangeY,maximumRangeY));
            AddChild(p);
        }
        
        public Emitter SetVelocity(float minAngle, float maxAngle, float minSpeedPps, float maxSpeedPps)
        {
            this.minAngle = minAngle;
            this.maxAngle = maxAngle;
            this.minSpeedPerParticle = minSpeedPps;
            this.maxSpeedPerParticle = maxSpeedPps;
            return this;
        }

        public Emitter SetColorOverLifeTime(Color startingColor, Color EndingColor)
        {
            this.startingColor = startingColor;
            this.endColor = EndingColor;
            return this;
        }

        public Emitter SetRandomScale(float minScale, float maxScale)
        {
            this.minScale = minScale;
            this.maxScale = maxScale;
            return this;
        }

        public Emitter SetSpawnPosition(int x, int y, int minimumRangeX, int maximumRangeX, int minimumRangeY, int maximumRangeY)
        {
            this.x = x;
            this.y = y;
            this.minimumRangeX = minimumRangeX;
            this.minimumRangeY = minimumRangeY;
            this.maximumRangeX = maximumRangeX;
            this.maximumRangeY = maximumRangeY;
            return this;
        }

        private float RandomFloat(float min, float max)
        {
            Random random = new Random();
            double range = max - min;
            double sample = random.NextDouble();
            double scaled = (sample * range) + min;
            return (float)scaled;
        }
        
    }
}