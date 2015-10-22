using u2f;

namespace ConsoleApplication1
{
    internal static class Program
    {
        private static void Main()
        {
            new Tests().ValidRegisterResponseIsParsedAsValid();
        }
    }
}
