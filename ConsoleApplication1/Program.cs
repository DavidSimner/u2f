﻿using u2f;

namespace ConsoleApplication1
{
    internal static class Program
    {
        private static void Main()
        {
            new Tests().ValidRegisterResponseIsParsedAsValid();
            new Tests().TheFirstValidSignResponseIsParsedAsValid();
            new Tests().TheSecondValidSignResponseIsParsedAsValid();
        }
    }
}
