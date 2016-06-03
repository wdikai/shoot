namespace Assets.Ballistics.Acceleration.Utils
{
    public class PressureConverter
    {
        public const float MmHgInPascal = 133.3224f;

        public static float MmHgToPascal(float pressure)
        {
            return pressure * MmHgInPascal;
        }

        public static float PascalToMmHg(float pressure)
        {
            return pressure / MmHgInPascal;
        }
    }
}
