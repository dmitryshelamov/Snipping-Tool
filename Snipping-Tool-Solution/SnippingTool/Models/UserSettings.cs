namespace SnippingTool.Models
{
    /// <summary>
    /// Represent a user settings
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Directory where screenshot will be saved
        /// </summary>
        public string SaveDirectory { get; set; }

        /// <summary>
        /// Extension of saving screenshot
        /// </summary>
        public string ImageExtentions { get; set; }
    }
}
