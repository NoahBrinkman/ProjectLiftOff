namespace GXPEngine
{
    public class WrappingBackground : GameObject
    {
        private Sprite[] sprites = new Sprite[3];
        public float speed;
        public WrappingBackground(string fileName)
        {
            speed = 0;
            sprites[0] = new Sprite(fileName,false,false);
            sprites[0].y = -sprites[0].height * 2;
            AddChild(sprites[0]);
            sprites[1] = new Sprite(fileName,false,false);
            sprites[1].y = -sprites[1].height;
            AddChild(sprites[1]);
            sprites[2] = new Sprite(fileName,false,false);
            sprites[2].y = 0;
            AddChild(sprites[2]);
        }

        void Update()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].Move(0,speed);
                if (sprites[i].y > game.height)
                {
                    sprites[i].y = -sprites[i].height;
                }
            }
        }
        
    }
}