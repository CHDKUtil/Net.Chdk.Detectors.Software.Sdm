using Net.Chdk.Model.Software;
using System;
using System.Globalization;

namespace Net.Chdk.Detectors.Software.Sdm
{
    sealed class SdmSoftwareDetector : InnerBinarySoftwareDetector
    {
        protected override string Name => "SDM";

        protected override string[] Strings => new[]
        {
            "Writing info file...\0"
        };

        protected override int StringCount => 11;

        protected override Version GetVersion(string[] strings)
        {
            return Version.Parse(strings[3]);
        }

        protected override CultureInfo GetLanguage(string[] strings)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        protected override DateTime? GetCreationDate(string[] strings)
        {
            var dateTimeStr = $"{strings[4]} {strings[5]}";
            return GetCreationDate(dateTimeStr);
        }

        protected override SoftwareCameraInfo GetCamera(string[] strings)
        {
            return new SoftwareCameraInfo
            {
                Platform = strings[9],
                Revision = strings[10]
            };
        }
    }
}
