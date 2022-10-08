using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Enums
{
    internal enum ThreadPriority
    {
        THREAD_PRIO_HIGHEST = 0,
        THREAD_PRIO_NORMAL = 1,
        THREAD_PRIO_LOWEST = 2,
        THREAD_PRIO_MANUAL_UPDATE = 100
    }
}
