using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Emitter : GameObject
    {
        
        /* count amount per frame
         Over the span of a second, if you want to spawn for example 10 objects, every how many seconds do you need to spawn an object?
        With that in mind, keep track of how much time has passed overall since your particle system became alive, 
        and every time you pass the interval to spawn a particle, spawn one 
        You never really want to rely on fps and framerate, because then you're frame locking your game. 
        The solution to that is Time.deltaTime. Do you know what the value it returns to you means?
        */
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
        
        private float particlesperMS;

        private bool hasColorOverLifeTime = false;
        //private bool hasVelocityOverLiftTime = false;

        private float timeSinceLastParticle = 0;
        private float millisecondsPerParticle;
        
        public Emitter(string fileName, int columns, int rows, int frames, int particlesPerSecond, float particleLiftTime, BlendMode blendMode)
        {
            this.particleFileName = fileName;
            this.cols = columns;
            this.rows = rows;
            this.frames = frames;
            this.particlesPerSecond = particlesPerSecond;
            this.particleLifeTime = particleLiftTime; 
            millisecondsPerParticle =  1000 / (float)particlesPerSecond;
            particlesperMS = 1f / millisecondsPerParticle;
            // particlesperMS = 1f / soemthing;
        }
        
        void Update()
        {
            timeSinceLastParticle += Time.deltaTime;
            while (timeSinceLastParticle >= millisecondsPerParticle)
            {
                SpawnParticle();
                timeSinceLastParticle -= millisecondsPerParticle;
            }
        }

        void SpawnParticle()
        {
            Particle p;
            if (hasColorOverLifeTime)
            {
                p = new Particle(particleFileName, rows, cols, frames, startingColor, particleLifeTime,
                    Utils.Random(minScale, maxScale),
                    0,
                    Utils.Random(minSpeedPerParticle, maxSpeedPerParticle), Utils.Random(minAngle, maxAngle));
                p.SetColorOverLifeTime(startingColor, endColor);
            }
            else
            {
                p = new Particle(particleFileName, rows, cols, frames, particleLifeTime,
                    Utils.Random(minScale, maxScale),
                    0,
                    Utils.Random(minSpeedPerParticle, maxSpeedPerParticle), Utils.Random(minAngle, maxAngle));
            }

            p.SetXY(Utils.Random(minimumRangeX, maximumRangeX), Utils.Random(minimumRangeY, maximumRangeY));
            game.AddChild(p);
        }
        
        public Emitter SetVelocity(float minAngle, float maxAngle, float minSpeedPps, float maxSpeedPps)
        {
            this.minAngle = minAngle;
            this.maxAngle = maxAngle;
            this.minSpeedPerParticle = minSpeedPps;
            this.maxSpeedPerParticle = maxSpeedPps;
            return this;
        }
        
        //Needs actual implementation
        public Emitter SetVelocityOverLifeTime(float minAngle, float maxAngle, float startSpeed, float endSpeed)
        {
            //hasVelocityOverLiftTime = true;
            this.minAngle = minAngle;
            this.maxAngle = maxAngle;
            //this.minSpeedPerParticle = minSpeedPps;
           // this.maxSpeedPerParticle = maxSpeedPps;
            return this;
        }
        
        public Emitter SetColorOverLifeTime(Color startingColor, Color EndingColor)
        {
            hasColorOverLifeTime = true;
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
        
    }
}