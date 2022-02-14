using System;
using System.Drawing;
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
        private float maxBuiltUpVelocity => velocityBuildUpIncrements * 1.5f;
        private bool onPlatform = true;
        private bool canMove = true;
        public Action<string> OnJump;
        public bool isAirborne
        {
            get { return (velocity >= 0.1f); }
        }
        private Platform platformCurrentlyOn;
        private Pivot parentObject;
        
        private Sprite speedIndicator;
        private ParticleColor indicatorColor;
        private Color minColor;
        private Color maxColor;
        public Player(string fileName, float velocityBuildUpIncrements, float velocityDropOffIncrements, Pivot pivot,int cols, int rows, TiledObject obj) : base(fileName,cols,rows)
        {
            this.velocityBuildUpIncrements = velocityBuildUpIncrements;
            this.velocityDropOffIncrements = velocityDropOffIncrements;
            parentObject = pivot;
            speedIndicator = new Sprite("Indicator.png",false,false);
            speedIndicator.SetOrigin(speedIndicator.width / 2, speedIndicator.height / 2);
            speedIndicator.SetScaleXY(.4f);
            speedIndicator.SetXY(0, height / 2 + 20);
            indicatorColor = new ParticleColor(255, 255, 255, 255);
            game.AddChild(speedIndicator);
            minColor = Color.White;
            maxColor = Color.DarkRed;
            speedIndicator.visible = false;
            Console.WriteLine(maxBuiltUpVelocity);
            
        }
        
        
        
        void Update()
        {
            
            if (!isAirborne)
            {
                SetCycle(0,1);
            }
            Animate(.1f);
            speedIndicator.SetXY(x, y +parentObject.y + height / 2 + 20);
            if (!canMove)
            {
                return;
            }

        if (GetCollisions().Length == 0)
            {
                onPlatform = false;
            }
            if (Input.GetKey(Key.D) && canMove)
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

            if (Input.GetKey(Key.A) && canMove)
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
                SetCycle(0,4);
                builtUpVelocity += velocityBuildUpIncrements * (float)Time.deltaTime / 1000;
                builtUpVelocity = Mathf.Clamp(builtUpVelocity, 0, maxBuiltUpVelocity);
                ParticleColor indicatorColor = new ParticleColor(255,255,2555,255);
                indicatorColor = indicatorColor.LerpColor(new ParticleColor(minColor.R, minColor.G,minColor.B,minColor.A)
                    , new ParticleColor(maxColor.R, maxColor.G,maxColor.B,maxColor.A), builtUpVelocity / maxBuiltUpVelocity);
                speedIndicator.SetColor(indicatorColor.r,indicatorColor.g,indicatorColor.b);
                
                speedIndicator.visible = true;
            }

            if (Input.GetKeyUp(Key.SPACE) && !isAirborne)
            {
                SetCycle(4,10);
                OnJump?.Invoke("Jump");
                velocity = builtUpVelocity;
                builtUpVelocity = 0;
                indicatorColor = new ParticleColor(minColor.R, minColor.G, minColor.B, minColor.A);
                speedIndicator.visible = false;
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
                speedIndicator.Destroy();
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
                }else if (other is BouncyPlatform)
                {
                    rotation = -rotation;
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
