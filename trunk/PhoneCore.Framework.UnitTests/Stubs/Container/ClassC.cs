using System;

namespace PhoneCore.Framework.UnitTests.Stubs.Container
{
    public class ClassC: IClassC
    {

        public void Run(string param1, string param2)
        {
            
        }

        public bool CanRun { get; set; }

        public string GenerateResult(string fileName)
        {
            return "result" + fileName;
        }
    }
}
