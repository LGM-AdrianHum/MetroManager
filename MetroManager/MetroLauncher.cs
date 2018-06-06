// File: MetroManager/MetroManager/MetroLauncher.cs
// User: Adrian Hum/
//
// Created:  2018-06-06 8:34 PM
// Modified: 2018-06-06 10:02 PM

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MetroManager
{
    [ComImport]
    [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
    internal class ApplicationActivationManager { }

    internal static class MetroLauncher
    {
        public static uint LaunchApp(string packageFullName, string arguments = null)
        {
            var pir = IntPtr.Zero;
            var activation = (IApplicationActivationManager)new ApplicationActivationManager();
            try
            {
                var error = OpenPackageInfoByFullName(packageFullName, 0, out pir);
                Debug.Assert(error == 0);
                if (error != 0) throw new Win32Exception(error);

                int length = 0;
                GetPackageApplicationIds(pir, ref length, null, out var count);

                var buffer = new byte[length];
                error = GetPackageApplicationIds(pir, ref length, buffer, out count);

                Debug.Assert(error == 0);
                if (error != 0) throw new Win32Exception(error);

                var appUserModelId = Encoding.Unicode.GetString(buffer, IntPtr.Size * count, length - IntPtr.Size * count);

                var hr = activation.ActivateApplication(appUserModelId, arguments ?? string.Empty, ActivateOptions.NoErrorUI, out var pid);
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                return pid;
            }
            finally
            {
                if (pir != IntPtr.Zero) ClosePackageInfo(pir);
            }
        }

        [DllImport("kernel32")]
        private static extern int OpenPackageInfoByFullName([MarshalAs(UnmanagedType.LPWStr)] string fullName,
            uint reserved, out IntPtr packageInfo);

        [DllImport("kernel32")]
        private static extern int GetPackageApplicationIds(IntPtr pir, ref int bufferLength, byte[] buffer,
            out int count);

        [DllImport("kernel32")]
        private static extern int ClosePackageInfo(IntPtr pir);
    }
}