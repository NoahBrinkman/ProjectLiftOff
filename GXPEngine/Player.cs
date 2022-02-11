using System;
using System.Threading;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

    
    public class Player : AnimationSprite
    {
        
        private float builtUpVelocity;
        public float velocity { get; private set; }
        private float velocityBuildUpIncrements;
        private float velocityDropOffIncrements;
        private bool onPlatform = true;
        private bool canMove = true;
        public Action<string> OnJump;
        public bool isAirborne
        {
            get { return (velocity >= 0.1f); }
        }
        private Platform platformCurrentlyOn;
        private Pivot parentObject;
        
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
            if (!canMove)
            {
                return;
            }
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
                OnJump?.Invoke("Jump");
                velocity = builtUpVelocity;
                builtUpVelocity = 0;
                
                //Play jump effect
            }

            if (velocity >= 0.1f)
            {
                if (y < 0 - parentObject.y - height / 8) velocity = 0;
                Move(0,-velocity );
                velocity -= onPlatform ? velocityDropOffIncrements *  (float)Time.deltaTime / 1000 * 3 : velocityDropOffIncrements *  (float)Time.deltaTime / 1000;
            }else if (!onPlatform || y > game.height - parentObject.y + width / 2)
            {
                //Console.WriteLine("you lose");
                Level level = (Level)SceneManager.instance.GetActiveScene();
                level.LostLevel();
                canMove = false;
            }
            
            if (x > game.width)
            {
               x = 0;
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
                bool touchingObstacle = false;
                Platform p = (Platform)other;
                if (other is BoosterPlatform)
                {
                    BoosterPlatform b = (BoosterPlatform)other;
                    velocity += 1;
                    velocity *= b.speedMultiplier;
                    rotation = b.rotation;
                    other.LateDestroy();
                }else if (other is ObstaclePlatform)
                {
                    velocity = 0;
                    ObstaclePlatform o = (ObstaclePlatform)other;
                    o.beenUsed = true;
                    touchingObstacle = true;
                }
                if (!p.beenUsed)
                {
                    if (velocity <= .1f)
                    {
                        p.Use();
                        platformCurrentlyOn = p;   
                    }
                }
                if(!touchingObstacle) onPlatform = true;
            }
        }
    }
