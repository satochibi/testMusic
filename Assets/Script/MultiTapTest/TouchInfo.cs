using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Touch構造体のToStringを生成するために、クラスを新しく作成。(構造体は継承できないから)
/// </summary>
public class TouchInfo
{
    public Vector2 Position { get; set; }
    public int FingerID { get; set; }

    public TouchPhase Phase { get; set; }

    public TouchInfo(Touch touch)
    {
        this.Position = touch.position;
        this.FingerID = touch.fingerId;
        this.Phase = touch.phase;
    }

    public override string ToString()
    {
        return string.Format("id: {0}, {1}, pos: {2}", this.FingerID, this.Phase, this.Position);
    }
}
