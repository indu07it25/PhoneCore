using System.Reflection;
using PhoneCore.Framework.IoC.Interception.Proxies;
namespace PhoneCore.Framework.Storage{
public class FileSystemServiceProxy:ProxyBase, PhoneCore.Framework.Storage.IFileSystemService
{
public System.IO.Stream OpenFile(System.String path,System.IO.FileMode mode){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,path,mode);return 
RunBehaviors(methodInvocation)
.GetReturnValue<System.IO.Stream>();}

public void CreateDirectory(System.String path){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,path);RunBehaviors(methodInvocation)
;}

public System.Boolean DirectoryExists(System.String path){var methodInvocation = BuildMethodInvocation(MethodBase.GetCurrentMethod()
,path);return 
RunBehaviors(methodInvocation)
.GetReturnValue<System.Boolean>();}

}}
