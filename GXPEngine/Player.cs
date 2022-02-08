using System;

namespace GXPEngine
{
    
    public class Player : Sprite
    {
        private float builtUpVelocity;
        private float velocity;
        private float velocityBuildUpIncrements;
        private float velocityDropOffIncrements;
        private bool onPlatform = true;
        private bool isAirborne
        {
            get { return (velocity >= 0.1f); }
        }

        private Platform platformCurrentlyOn;
        
        public Player(string fileName, float velocityBuildUpIncrements, float velocityDropOffIncrements) : base(fileName)
        {
            this.velocityBuildUpIncrements = velocityBuildUpIncrements;
            this.velocityDropOffIncrements = velocityDropOffIncrements;
        }
        
        void Update()
        {
            if (GetCollisions().Length == 0)
            {
                onPlatform = false;
            }
            if (Input.GetKey(Key.A))
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

            if (Input.GetKey(Key.D))
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
            if (Input.GetKey(Key.SPACE))
            {
                builtUpVelocity += velocityBuildUpIncrements * (float)Time.deltaTime / 1000;
                builtUpVelocity = Mathf.Clamp(builtUpVelocity, 0, velocityBuildUpIncrements * 5);
            }

            if (Input.GetKeyUp(Key.SPACE))
            {
                velocity = builtUpVelocity;
                builtUpVelocity = 0;
            }

            if (velocity >= 0.1f)
            {
                
                Move(0,-velocity );
                velocity -= onPlatform ? velocityDropOffIncrements *  (float)Time.deltaTime / 1000 * 3 : velocityDropOffIncrements *  (float)Time.deltaTime / 1000;
            }else if (!onPlatform)
            {
                Console.WriteLine("you lose");
            }

            if (x > game.width)
            {
               x = -width;
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
                Platform p = (Platform)other;
                if (!p.beenUsed)
                {
                    p.beenUsed = true;
                    platformCurrentlyOn = p;
                   
                }
                onPlatform = true;
            }
        }
    }
}