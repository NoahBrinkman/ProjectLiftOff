using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

    // When colliding with this object load a random chunk
    class SwitchMap : AnimationSprite
    {
        public SwitchMap(TiledObject obj = null) : base("colors.png", 1, 1)
        {
            collider.isTrigger = true;
        }

        public SwitchMap(string imageFile, int cols, int rows, TiledObject obj) : base(imageFile, cols, rows)
        {
            collider.isTrigger = true;
        }

        void Initialize(TiledObject obj = null)
        {
         collider.isTrigger = true;
        }

        void Update()
        {
            Move(0, ((MyGame)game).gravity);
        }    
    }

