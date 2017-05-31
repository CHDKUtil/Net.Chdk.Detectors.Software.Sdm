using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Model.Software;
using Net.Chdk.Providers.Software;

namespace Net.Chdk.Detectors.Software.Sdm
{
    abstract class SdmSoftwareDetectorBase : ProductBinarySoftwareDetector
    {
        protected SdmSoftwareDetectorBase(ISourceProvider sourceProvider)
            : base(sourceProvider)
        {
        }

        public sealed override string CategoryName => "PS";
        public sealed override string ProductName => "SDM";

        protected override SoftwareBuildInfo GetBuild(string[] strings)
        {
            return new SoftwareBuildInfo();
        }
    }
}
