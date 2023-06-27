using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogglePauseButton
{
    internal static class ButtonStateEntity
    {
        public static bool IsReleased { get; set; }
        public static float Timer { get; set; } = 0f;
        public static float PrevTime { get; set; } = float.MaxValue;
    }
}
