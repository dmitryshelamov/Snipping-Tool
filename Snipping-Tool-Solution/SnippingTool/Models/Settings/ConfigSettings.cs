using System;
using System.IO;
using SnippingTool.Models.Settings.Interfaces;

namespace SnippingTool.Models.Settings
{
    public class ConfigSettings : IConfigSettings
    {
        public string XmlName => "UserSettings.xml";
        public string ConfigPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XmlName);
    }
}
