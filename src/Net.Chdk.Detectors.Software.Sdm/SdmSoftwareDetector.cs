using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Model.Software;
using Net.Chdk.Providers.Software;
using System;
using System.Globalization;

namespace Net.Chdk.Detectors.Software.Sdm
{
    sealed class SdmSoftwareDetector : ProductBinarySoftwareDetector
    {
        public SdmSoftwareDetector(ISourceProvider sourceProvider)
            : base(sourceProvider)
        {
        }

        public override string ProductName => "SDM";

        protected override string[] Strings => new[]
        {
            "Writing info file...\0",
        };

        protected override int StringCount => 14;

        protected override Version GetProductVersion(string[] strings)
        {
            return GetVersion(strings[3]);
        }

        protected override CultureInfo GetLanguage(string[] strings)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        protected override DateTime? GetCreationDate(string[] strings)
        {
            return GetCreationDate($"{strings[4]} {strings[5]}");
        }

        protected override SoftwareCameraInfo GetCamera(string[] strings)
        {
            string revision = GetRevision(strings);
            return GetCamera(strings[9], revision);
        }

        protected override string GetSourceName(string[] strings)
        {
            return ProductName;
        }

        private string GetRevision(string[] strings)
        {
            for (var i = 10; i < StringCount; i++)
                if (strings[i].Length > 0)
                    return strings[i];
            return null;
        }
    }
}
