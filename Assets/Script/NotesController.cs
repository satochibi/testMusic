using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotesController : MonoBehaviour
{
    
    public Track track = Track.track3;
   
    public void SetTrack(Track num)
    {
        //ƒm[ƒc¶¬‚ÉŒÄ‚Ño‚³‚ê‚é
        track = num;
    }

}
