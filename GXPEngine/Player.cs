using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

    
    public class Player : AnimationSprite
    {
        
        private float builtUpVelocity;
        private float velocity;
        private float velocityBuildUpIncrements;
        private float velocityDropOffIncrements;
        private bool onPlatform = true;
        private bool canMove = true;
        private bool isAirborne
        {
            get { return (velocity >= 0.1f); }
        }
        private Platform platformCurrentlyOn;
        private Pivot parentObject;

        public Player(TiledObject obj) : base("square.png", 1, 1)
        {
            
        }

        public Player(string fileName, float velocityBuildUpIncrements, float velocityDropOffIncrements, Pivot pivot,int cols, int rows, TiledObject obj) : base(fileName,cols,rows)
        {
            this.velocityBuildUpIncrements = velocityBuildUpIncrements;
            this.velocityDropOffIncrements = velocityDropOffIncrements;
            parentObject = pivot;
        }
        
    
        public void CollisionCheck()
        {
            GameObject[] collisions = GetCollisions();
            for(int i =0; i < collisions.Length; i++)
            {
                if(collisions[i] is SwitchMap)
                {
                    int r = Utils.Random(1, 2);
                    SceneManager.instance.LoadScene(r);
                }
            }

        }
        
        void Update()
        {
           //Move(0, ((MyGame)game).gravity);
        if (GetCollisions().Length == 0)
            {
                onPlatform = false;
            }
            if (Input.GetKey(Key.A) && canMove)
            {
                if (rotation >= -110)
                {
                    if (isAirborne)
                    {
                        rotation -= .5f;
                    }
                    else
                    {
                        rotation--;
                    }
                }
            }

            if (Input.GetKey(Key.D) && canMove)
            {
                if (rotation <= 110)
                {
                    if (isAirborne)
                    {
                        rotation += .5f;
                    }
                    else
                    {
                        rotation++;
                    }
                }
            }
            if (Input.GetKey(Key.SPACE) && !isAirborne &&canMove)
            {
                builtUpVelocity += velocityBuildUpIncrements * (float)Time.deltaTime / 1000;
                builtUpVelocity = Mathf.Clamp(builtUpVelocity, 0, velocityBuildUpIncrements * 1.5f);
            }

            if (Input.GetKeyUp(Key.SPACE) && !isAirborne)
            {
                velocity = builtUpVelocity;
                builtUpVelocity = 0;
            }

            if (velocity >= 0.1f)
            {
                if (y < 0 - parentObject.y - height / 8) velocity = 0;
                Move(0,-velocity );
                velocity -= onPlatform ? velocityDropOffIncrements *  (float)Time.deltaTime / 1000 * 3 : velocityDropOffIncrements *  (float)Time.deltaTime / 1000;
            }else if (!onPlatform)
            {
                
               //Console.WriteLine("you lose");
            }

            if (x > game.width)
            {
               x = -game.width;
            }

            if (x < 0)
            {
                x = game.width;
            }

        CollisionCheck();
    }

        private void OnCollision(GameObject other)
        {
            if (other is Platform)
            {
                if (!onPlatform)
                {
                   
                }            
                Platform p = (Platform)other;
                if (!p.beenUsed)
                {
                    if (velocity <= .1f)
                    {
                     p.beenUsed = true;
                     platformCurrentlyOn = p;   
                    }
                }
                onPlatform = true;
            }
        }
    }
