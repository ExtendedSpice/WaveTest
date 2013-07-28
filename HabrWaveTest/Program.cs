namespace HabrWaveTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(40, 20);
            b.Start(100);
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }
    }
}
