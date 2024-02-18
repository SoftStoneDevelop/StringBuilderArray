#if NET6_0_OR_GREATER
#else
using System;
using System.Reflection;

namespace StringBuilderArray
{
    public static class StringHelper
    {
        public static readonly Func<int, string> FastAllocateString =
            (Func<int, string>)typeof(string)
            .GetMethod("FastAllocateString", BindingFlags.NonPublic | BindingFlags.Static)
            .CreateDelegate(typeof(Func<int, string>))
            ;
    }
}
#endif