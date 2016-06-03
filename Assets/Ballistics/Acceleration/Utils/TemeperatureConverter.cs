namespace Assets.Ballistics.Acceleration.Utils
{
    public class TemeperatureConverter
    {

        public const float CelsiusInKelvinZero = 273.15f;

        public static float CelsiusToKelvin(float temperature)
        {
            return temperature + CelsiusInKelvinZero;
        }

        public static float KelvinToCelsius(float temperature)
        {
            return temperature - CelsiusInKelvinZero;
        }
    }
}
