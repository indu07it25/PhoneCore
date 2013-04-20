using System;

namespace PhoneCore.Framework.UnitTests.Stubs.Container
{
    public interface IClassC
    {
        void Run(string param1, string param2);
        bool CanRun { get; set; }
        string GenerateResult(string fileName);

    }
}
