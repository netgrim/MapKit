using System;

namespace Cyrez.Graphics.Control
{
    public class WindowChangedEventArgs :EventArgs
    {
        public WindowChangedEventArgs(WindowChangedEventType type)
        {
            Type = type;
        }

        public WindowChangedEventType Type { get; set; }
    }
}
