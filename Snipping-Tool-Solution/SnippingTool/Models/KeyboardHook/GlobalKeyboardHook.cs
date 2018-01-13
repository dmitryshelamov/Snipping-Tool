using System;
using System.Runtime.InteropServices;

namespace SnippingTool.Models.KeyboardHook
{
    public class GlobalKeyboardHook : IDisposable
    {
        /// <summary>
        /// Represent a event when any button pressed on keyboard
        /// </summary>
        public event EventHandler KeyDownEvent;

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain.
        ///  You would install a hook procedure to monitor the 
        /// system for certain types of events. These events are associated
        ///  either with a specific thread or with all threads
        /// in the same desktop as the calling thread.
        /// </summary>
        /// <param name="idHook">The type of hook procedure to be installed.</param>
        /// <param name="func">A pointer to the hook procedure.</param>
        /// <param name="hInstance">A handle to the DLL containing the 
        /// hook procedure pointed to by the lpfn parameter.</param>
        /// <param name="threadID">The identifier of the thread with which the hook
        ///     procedure is to be associated. For desktop apps, if this parameter is zero,
        ///  the hook procedure is associated with all existing threads running 
        /// in the same desktop as the calling thread.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(HookType idHook, hookProc func, IntPtr hInstance, int threadID);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hInstance">A handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain.
        /// A hook procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="idHook">This parameter is ignored.</param>
        /// <param name="nCode">The hook code passed to the current hook procedure. The next hook procedure uses this code to determine how to process the hook information.</param>
        /// <param name="wParam">The wParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        /// <param name="lParam">The lParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, ref KeyInfoStruct lParam);

        /// <summary>
        /// Loads the specified module into the address space of the calling process.
        /// </summary>
        /// <param name="lpFileName">The name of the module.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);


        /// <summary>
        /// Determines whether a key is up or down at the time the function is called
        /// </summary>
        /// <param name="nVirtKey">Virtual-key code</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int nVirtKey);

        /// <summary>
        /// Occurs when a any key is pressed.
        /// </summary>
        private delegate IntPtr hookProc(int code, IntPtr wParam, ref KeyInfoStruct lParam);
        private hookProc _hookProc;
        bool keyReleased;

        /// <summary>
        /// Store hookID for our hook.
        /// </summary>
        private IntPtr hookId = IntPtr.Zero;


        /// <summary>
        /// Contains information about a low-level keyboard input event.
        /// </summary>
        public struct KeyInfoStruct
        {
            /// <summary>
            /// A virtual-key code.
            /// </summary>
            public int vkCode;
            /// <summary>
            /// A hardware scan code for the key.
            /// </summary>
            public int scanCode;
            /// <summary>
            /// The extended-key flag, event-injected flags, context code, and transition-state flag.
            /// </summary>
            public int flags;
            /// <summary>
            /// The time stamp for this message
            /// </summary>
            public int time;
            /// <summary>
            /// Additional information associated with the message.
            /// </summary>
            public int dwExtraInfo;
        }

        /// <summary>
        /// Represent Key state
        /// </summary>
        public enum KeyboardState
        {
            KeyPressed = 0x8000,
            KeyDown = 0x0100,
            KeyUp = 0x0101,
            SysKeyDown = 0x0104,
            SysKeyUp = 0x0105
        }

        /// <summary>
        /// Type of global hook
        /// </summary>
        public enum HookType
        {
            /// <summary>
            /// Keyboard hook
            /// </summary>
            WH_KEYBOARD_LL = 13
        }

        /// <summary>
        /// Indicating whether hook is installed.
        /// </summary>
        public bool IsHookSet { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalKeyboardHook"/> class.
        /// </summary>
        public GlobalKeyboardHook()
        {
            _hookProc = HookProc;
        }

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain.
        /// </summary>
        public void SetHook()
        {
            if (IsHookSet == false)
            {
                IntPtr hInstance = LoadLibrary("User32");
                hookId = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, _hookProc, hInstance, 0);
                IsHookSet = true;
            }
        }

        /// <summary>
        /// Removes a hook procedure installed in a hook chain.
        /// </summary>
        public void RemoveHook()
        {
            if (IsHookSet)
            {
                UnhookWindowsHookEx(hookId);
                IsHookSet = false;
            }
        }

        /// <summary>
        /// Hook procedure, triggered by hook
        /// </summary>
        /// <param name="code">Hook code</param>
        /// <param name="wparam">Event type</param>
        /// <param name="lparam">Hook event information</param>
        /// <returns></returns>
        private IntPtr HookProc(int code, IntPtr wparam, ref KeyInfoStruct lparam)
        {
            //if it isn't >= 0, the function shouldn't do anyting
            if (code >= 0)
            {
                if (wparam == (IntPtr)KeyboardState.KeyUp)
                    keyReleased = true;
                if (keyReleased)
                {
                    keyReleased = false;
                    if (KeyDownEvent != null)
                        KeyDownEvent(this, null);
                }
            }
            return CallNextHookEx(hookId, code, wparam, ref lparam);
        }

        public void Dispose()
        {
            RemoveHook();
        }
    }
}
