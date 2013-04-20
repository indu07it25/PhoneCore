using System.Reflection;
using PhoneCore.Framework.IoC.Interception.Proxies;
namespace PhoneCore.Framework.UnitTests.Stubs.Container{
public class ClassAProxy:ProxyBase, PhoneCore.Framework.UnitTests.Stubs.Container.IClassA
{
public System.Int32 Add(System.Int32 a,System.Int32 b){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,a,b);return 
RunBehaviors(methodInvocation)
.GetReturnValue<System.Int32>();}

public System.String SayHello(System.String name){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,name);return 
RunBehaviors(methodInvocation)
.GetReturnValue<System.String>();}

}}
