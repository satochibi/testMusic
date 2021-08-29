using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotesController : MonoBehaviour
{
    
    float notesTime;

    Track notesTrack = Track.track3;

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

    public bool IsTapped { get; set; } = false;

    public Track NotesTrack {
        get { return this.notesTrack; }
        set { this.notesTrack = value; }
    }

    



}
