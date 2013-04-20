using System.Reflection;
using PhoneCore.Framework.IoC.Interception.Proxies;
namespace PhoneCore.Framework.Storage{
public class SettingServiceProxy:ProxyBase, PhoneCore.Framework.Storage.ISettingService
{
public void Save(System.String key,System.Object value){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,key,value);RunBehaviors(methodInvocation)
;}

public System.Boolean IsExist(System.String key){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,key);return 
RunBehaviors(methodInvocation)
.GetReturnValue<System.Boolean>();}

public T Load<T>(System.String key){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,key);methodInvocation.GenericTypes.Add(typeof(T));
return 
RunBehaviors(methodInvocation)
.GetReturnValue<T>();}

}}
