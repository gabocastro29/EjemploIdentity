using System;

namespace EjemploIdentity
{
    public class Util
    {
        public static long GetTimeInMillis() {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static string MensajeValidacionFechaNacimientoMinimo()
        {
            var min = DateTime.Now.AddYears(-110);
            var msg = string.Format("{0:MM/dd/yyyy}", min);
            return msg;
        }
        public static string MensajeValidacionFechaNacimientoMaximo()
        {
            var max = DateTime.Now.AddYears(-18);
            var msg = string.Format("{0:MM/dd/yyyy}", max);
            return msg;
        }
        public static string MensajeValidacionFechaNacimiento() {
            var min = DateTime.Now.AddYears(-18);
            var max = DateTime.Now.AddYears(-110);
            var msg = string.Format("Ingrese una fecha entre {0:MM/dd/yyyy} y {1:MM/dd/yyyy}", max, min);
            return msg;
        }
    }
}