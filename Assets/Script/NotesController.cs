using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotesController : MonoBehaviour
{
    
    public Track track = Track.track3;
   
    public void SetTrack(Track num)
    {
        //ノーツ生成時に呼び出される
        track = num;
    }

}
