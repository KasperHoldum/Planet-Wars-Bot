namespace AthenaBot
{
    public class Tuple<T1, T2>
    {
        private readonly T1 value1;
        private readonly T2 value2;

        public Tuple(T1 value1, T2 value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public T1 Value1
        {
            get { return value1; }
        }

        public T2 Value2
        {
            get { return value2; }
        }
    }

}
