﻿using Net.Chdk.Detectors.Software.Product;
using Net.Chdk.Providers.Boot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Net.Chdk.Detectors.Software.Sdm
{
    sealed class SdmProductDetector : ProductDetector
    {
        private static readonly Dictionary<string, string> TextsVersions = new Dictionary<string, string>
        {
            ["Spanish.txt"] = "2.2",
            ["Indonesian.txt"] = "2.2",
            ["Romanian.txt"] = "2.2",
        };

        private static readonly Dictionary<int, string> PropsVersions = new Dictionary<int, string>
        {
            [350] = "2.2",
            [290] = "2.1",
        };

        public SdmProductDetector(IBootProvider bootProvider)
            : base(bootProvider)
        {
        }

        protected override string ProductName => "SDM";

        protected override Version GetVersion(string rootPath)
        {
            var version = GetVersionFromProperties(rootPath);
            if (version != null)
                return version;

            var txtPath = Path.Combine(rootPath, ProductName, "TEXTS");
            return GetValue(txtPath, TextsVersions, Version.Parse);
        }

        protected override CultureInfo GetLanguage(string rootPath)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        private static Version GetVersionFromProperties(string rootPath)
        {
            var propsPath = Path.Combine(rootPath, "sdmtable.properties");
            if (!File.Exists(propsPath))
                return null;

            var lines = File.ReadAllLines(propsPath);
            if (lines == null)
                return null;

            var nLine = lines.FirstOrDefault(l => l.StartsWith("n=", StringComparison.InvariantCulture));
            if (nLine == null)
                return null;

            var nStr = nLine.Substring("n=".Length);
            if (nStr == null)
                return null;

            int n;
            if (!int.TryParse(nStr, out n))
                return null;

            string versionStr;
            if (!PropsVersions.TryGetValue(n, out versionStr))
                return null;

            return Version.Parse(versionStr);
        }
    }
}