namespace DoExtension
{
    public static class IntExtension
    {
        public static int Hour(this in int num)
        {
            return num * 60000 * 60;
        }

        public static bool IsEven(this in int i)
        {
            return (i % 2) == 0;
        }

        public static bool IsOdd(this in int i)
        {
            return (i % 2) != 0;
        }

        public static int Minutes(this in int num)
        {
            return num * 60000;
        }

        public static int Seconds(this in int num)
        {
            return num * 1000;
        }
    }
}
