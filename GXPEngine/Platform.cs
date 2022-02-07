namespace GXPEngine
{
    public class Platform : Sprite
    {
        public bool beenUsed = false;
        public Platform(string filename) : base(filename)
        {
            collider.isTrigger = true;
        }
    }
}