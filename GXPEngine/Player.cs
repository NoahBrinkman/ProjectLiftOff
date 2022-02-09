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
        private Pivot parentObject;
        
        public Player(string fileName, float velocityBuildUpIncrements, float velocityDropOffIncrements, Pivot pivot) : base(fileName)
        {
            this.velocityBuildUpIncrements = velocityBuildUpIncrements;
            this.velocityDropOffIncrements = velocityDropOffIncrements;
            parentObject = pivot;
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
            if (Input.GetKey(Key.SPACE) && !isAirborne)
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
               Console.WriteLine("you lose");
            }

            if (x > game.width)
            {
               x = -game.width;
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
                if (other is BoosterPlatform)
                {
                    rotation = other.rotation;
                   
                    BoosterPlatform b = (BoosterPlatform)other; 
                    velocity *= b.speedMultiplier;
                    b.beenUsed = true;
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
}