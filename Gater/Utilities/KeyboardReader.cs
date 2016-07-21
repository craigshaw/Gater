using System.Runtime.InteropServices;

namespace Gater.Utilities
{
    public static class KeyboardReader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); // Keys enumeration

        public static bool IsKeyDown(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState(vKey) & 0x8000);
        }
    }
}
