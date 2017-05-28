using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Providers.Software;
using System;

namespace Net.Chdk.Detectors.Software.Sdm
{
    sealed class SdmAdHocSoftwareDetector : ProductBinarySoftwareDetector
    {
        public SdmAdHocSoftwareDetector(ISourceProvider sourceProvider)
            : base(sourceProvider)
        {
        }

        public override string ProductName => "SDM";

        protected override string String => "SDM ver. ";

        protected override int StringCount => 2;

        protected override Version GetProductVersion(string[] strings)
        {
            return GetVersion(strings[0]);
        }
    }
}
