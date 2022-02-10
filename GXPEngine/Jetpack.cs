using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
    public class Jetpack : AnimationSprite 
    {
        
        public Jetpack(TiledObject obj) : base("triangle.png", 1, 1)
        {
            Initiazlize(obj);
        }

        public Jetpack(string imageFile, int cols, int rows, TiledObject obj) : base(imageFile, cols, rows)
        {
            Initiazlize(obj);
        }

    void Initiazlize(TiledObject obj = null)
        {
            SetOrigin(width / 2, height / 2);
            collider.isTrigger = true;
        }


        public void ShootUp()
        {
           
        }

    }

