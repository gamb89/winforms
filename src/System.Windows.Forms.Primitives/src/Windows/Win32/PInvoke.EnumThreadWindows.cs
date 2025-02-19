﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32;

internal static partial class PInvoke
{
    public delegate BOOL EnumThreadWindowsCallback(HWND hWnd);

    public static unsafe BOOL EnumThreadWindows(uint dwThreadId, EnumThreadWindowsCallback callback)
    {
        // We pass a function pointer to the native function and supply the callback as
        // reference data, so that the CLR doesn't need to generate a native code block for
        // each callback delegate instance (for storing the closure pointer).
        GCHandle gcHandle = GCHandle.Alloc(callback);
        try
        {
            return EnumThreadWindows(dwThreadId, &HandleEnumThreadWindowsNativeCallback, (LPARAM)(nint)gcHandle);
        }
        finally
        {
            gcHandle.Free();
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static BOOL HandleEnumThreadWindowsNativeCallback(HWND hWnd, LPARAM lParam)
    {
        return ((EnumThreadWindowsCallback)((GCHandle)(nint)lParam).Target!)(hWnd);
    }
}
