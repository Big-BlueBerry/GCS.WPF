using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCS.WPF
{
    [Flags]
    public enum State
    {
        NOTDRAWING = 0,
        DRAWING = 1,

        LEFTMOUSE_DOWN = 1 << 1,

        CIRCLE = 1 << 2,
        LINE = 1 << 3,
        DOT = 1 << 4,

        DRAWING_CIRCLE = 1 | 1 << 2,
        DRAWING_LINE = 1 | 1 << 3,
        DRAWING_DOT = 1 | 1 << 4
    }
}
