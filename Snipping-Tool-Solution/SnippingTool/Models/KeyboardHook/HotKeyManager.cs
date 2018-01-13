using System;
using System.Windows.Input;
using SnippingTool.Models.Settings;

namespace SnippingTool.Models.KeyboardHook
{
    /// <summary>
    /// Represent a class that will listen for keyboard event 
    /// and fire event when corresponding hotkey will be pressed. 
    /// </summary>
    public class HotKeyManager
    {
        /// <summary>
        /// EventHandler for taking entire screen
        /// </summary>
        public event EventHandler TakeWholeScreenEvent;
        /// <summary>
        /// EventHandler for taking screenshot and then cropping on the fly
        /// </summary>
        public event EventHandler TakeAndCropScreenEvent;

        private readonly UserSettings _userSettings;
        private readonly GlobalKeyboardHook _keyboardHook;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotKeyManager"/> class.
        /// </summary>
        /// <param name="userSettings"></param>
        /// <param name="keyboardHook"></param>
        public HotKeyManager(UserSettings userSettings, GlobalKeyboardHook keyboardHook)
        {
            _keyboardHook = keyboardHook;
            _keyboardHook.SetHook();
            _keyboardHook.KeyDownEvent += KeyboardHookOnKeyDown;
            _userSettings = userSettings;
        }

        private void KeyboardHookOnKeyDown(object sender, EventArgs e)
        {

            if (Keyboard.IsKeyDown(_userSettings.TakeWholeScreenHotKey.Key)
                && Keyboard.Modifiers ==
                (_userSettings.TakeWholeScreenHotKey.Mod1 | _userSettings.TakeWholeScreenHotKey.Mod2))
            {
                if (TakeWholeScreenEvent != null)
                    TakeWholeScreenEvent(this, EventArgs.Empty);
                return;
            }

            if (Keyboard.IsKeyDown(_userSettings.TakeAndCropScreenHotKey.Key)
                && Keyboard.Modifiers ==
                (_userSettings.TakeAndCropScreenHotKey.Mod1 | _userSettings.TakeAndCropScreenHotKey.Mod2))
            {
                if (TakeAndCropScreenEvent != null)
                    TakeAndCropScreenEvent(this, EventArgs.Empty);
                return;
            }
        }
    }
}
