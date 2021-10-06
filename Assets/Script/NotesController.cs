using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum NotesType
{
    Normal,
    Long,
    LongEnd,
    NotesTypeNum
}

public class NotesController : MonoBehaviour
{
    
    float notesTime;

    Track notesTrack = Track.track3;
    NotesType notesType = default;
    public float NotesTime {
        get { return this.notesTime; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(); 
            }
            this.notesTime = value;
        }
    }
    public NotesType Type { get { return this.notesType; } set { this.notesType = value; } }
    public bool IsTapped { get; set; } = false;

    public Track NotesTrack {
        get { return this.notesTrack; }
        set { this.notesTrack = value; }
    }


    



}
