using System.Windows.Input;

namespace SnippingTool.Models.KeyboardHook
{
    public class HotKey
    {
        public Key Key { get; set; }

        public ModifierKeys Mod1 { get; set; }

        public ModifierKeys Mod2 { get; set; }
    }
}
