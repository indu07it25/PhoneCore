using System;

namespace PhoneCore.Framework.UnitTests.Stubs.Container
{
    public class ClassA: IClassA
    {

        public int Add(int a, int b)
        {
            return a + b;
        }

        public string SayHello(string name)
        {
            return String.Format("Hello, {0}", name);
        }
    }
}
