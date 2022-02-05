namespace GXPEngine
{
    public static class MathFunctions
    {
        public static float  Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat + (secondFloat - firstFloat) * by;
        }
    }
}