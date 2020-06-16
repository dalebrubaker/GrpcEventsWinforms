﻿using System.Diagnostics.CodeAnalysis;

namespace Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class NativeConstants
    {
        public const uint WM_MOUSEACTIVATE = 0x21;
        public const uint MA_ACTIVATE = 1;
        public const uint MA_ACTIVATEANDEAT = 2;
        public const uint MA_NOACTIVATE = 3;
        public const uint MA_NOACTIVATEANDEAT = 4;
    }
}