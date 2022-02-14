﻿using GXPEngine;
using TiledMapParser;
    public class CollapsingPlatform : Platform
    {
        private float secondsBeforeCollapse;
        private float timer;
        public CollapsingPlatform(string fileName, float secondsBeforeCollapse,int cols = 1,int rows = 1) : base(fileName,cols,rows)
        {
            SetColor(255, 255, 0);
            this.secondsBeforeCollapse = secondsBeforeCollapse;
            timer = secondsBeforeCollapse;
            SetCycle(0,frameCount);
        }

        void Update()
        {
            if (beenUsed)
            {
                timer -= (float)Time.deltaTime / 1000;
                
                Animate(.1f);
                if (timer <= 0)
                {
                    LateDestroy();
                }
            }
        }
        
    }