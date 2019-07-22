using System;
using System.Linq;

namespace Generics
{
    class Program
    {
        public enum Gender : int
        {
            Male = 1,
            Female  = 2
        }

        public enum Weekday
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }


        public static object MapValueToEnum<T>(T value)
        {
            if(typeof(T) == typeof(int))
            {
                if (Enum.IsDefined(typeof(Gender), value))
                {
                    return Enum.Parse(typeof(Gender), value.ToString());
                }

            } else if (typeof(T) == typeof(string))
            {
                if (Enum.IsDefined(typeof(Gender), value))
                {
                    return Enum.Parse(typeof(Gender), value.ToString());
                }
                else if (Enum.IsDefined(typeof(Weekday), value))
                {
                    return Enum.Parse(typeof(Weekday), value.ToString());
                }
            }

            return default(T);
        }
        static void Main(string[] args)
        {
            var someValue = MapValueToEnum<int>(3);
            var someValue2 = MapValueToEnum<int>(2);

            var otherValue = MapValueToEnum("Male");
            var otherOtherValue = MapValueToEnum("Maleeee");

            var aaaaaa = MapValueToEnum("Monday");



            Console.WriteLine("Hello World!");
        }
    }
}
