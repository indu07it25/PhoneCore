using System.Reflection;
using PhoneCore.Framework.IoC.Interception.Proxies;
namespace PhoneCore.Framework.UnitTests.Stubs.Container{
public class ClassCProxy:ProxyBase, PhoneCore.Framework.UnitTests.Stubs.Container.IClassC
{
public void Run(System.String param1,System.String param2){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,param1,param2);RunBehaviors(methodInvocation)
;}

public System.String GenerateResult(System.String fileName){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,fileName);return 
RunBehaviors(methodInvocation)
.GetReturnValue<System.String>();}

public System.Boolean CanRun{
get{ return RunBehaviors(BuildMethodInvocation(MethodBase.GetCurrentMethod()))
.GetReturnValue<System.Boolean>();}set{ RunBehaviors(BuildMethodInvocation(MethodBase.GetCurrentMethod(), value));}
}

}}
