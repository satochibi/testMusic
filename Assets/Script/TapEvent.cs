using System.Collections;
using System.Collections.Generic;

public class TapEvent
{
    public Track Track { get; set; }
    public bool IsTapDown { get; set; } = false;

    public float? OnTapTime { get; set; } = null;

}
