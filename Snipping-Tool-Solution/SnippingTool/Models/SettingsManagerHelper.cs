﻿using System;
using SnippingTool.Models.Interfaces;

namespace SnippingTool.Models
{
    class SettingsManagerHelper : ISettingsManagerHelper
    {
        /// <summary>
        /// Get path to "MyPictures" folder
        /// </summary>
        /// <returns>Path to ""MMyPictures" folder</returns>
        public string GetDefaultSaveDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }
}