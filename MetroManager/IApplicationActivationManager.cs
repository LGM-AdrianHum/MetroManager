// File: MetroManager/MetroManager/IApplicationActivationManager.cs
// User: Adrian Hum/
//
// Created:  2018-06-06 8:34 PM
// Modified: 2018-06-06 10:01 PM

using System;
using System.Runtime.InteropServices;

namespace MetroManager
{
    internal enum ActivateOptions
    {
        None = 0x00000000, // No flags set
        DesignMode = 0x00000001, // The application is being activated for design mode

        NoErrorUI =
            0x00000002, // Do not show an error dialog if the app fails to activate

        NoSplashScreen = 0x00000004 // Do not show the splash screen when activating the app
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
    internal interface IApplicationActivationManager
    {
        int ActivateApplication([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId,
            [MarshalAs(UnmanagedType.LPWStr)] string arguments,
            ActivateOptions options, out uint processId);

        int ActivateForFile([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, IntPtr pShelItemArray,
            [MarshalAs(UnmanagedType.LPWStr)] string verb, out uint processId);

        int ActivateForProtocol([MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, IntPtr pShelItemArray,
            [MarshalAs(UnmanagedType.LPWStr)] string verb, out uint processId);
    }
}