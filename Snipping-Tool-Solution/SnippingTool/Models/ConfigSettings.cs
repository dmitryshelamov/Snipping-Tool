using System;
using System.IO;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.Models
{
    public class ConfigSettings : IConfigSettings
    {
        public string XmlName => "UserSettings.xml";
        public string ConfigPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XmlName);
    }
}
