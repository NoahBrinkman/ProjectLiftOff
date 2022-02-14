﻿namespace GXPEngine
{
    public class BouncyPlatform : Platform
    {
        public BouncyPlatform(string imageFile, int cols = 1, int rows = 1) : base(imageFile, cols, rows)
        {
            SetOrigin(width / 2, height / 2);
            SetScaleXY(1,2);
            collider.isTrigger = true;
        }
    }
}