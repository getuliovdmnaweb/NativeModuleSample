using Microsoft.ReactNative;
using Microsoft.ReactNative.Managed;

namespace NativeModuleSample
{
    public sealed partial class ReactPackageProvider : IReactPackageProvider
    {
        public void CreatePackage(IReactPackageBuilder packageBuilder)
        {
            packageBuilder.AddAttributedModules();
        }

       partial void CreatePackageImplementation(IReactPackageBuilder p);
    }
}
