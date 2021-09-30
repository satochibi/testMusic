using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class LaneTap : MonoBehaviour
{
    
     TapEvent[] tapEvents = new TapEvent[] {
        new TapEvent { Track = Track.track1 },
        new TapEvent { Track = Track.track2 },
        new TapEvent { Track = Track.track3 },
        new TapEvent { Track = Track.track4 },
        new TapEvent { Track = Track.track5 }
    };

    float timeOut =1.0f;
    [SerializeField]
    GameObject fumenGameObj;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FuncCoroutine());
    }
    IEnumerator FuncCoroutine()
    {
        while (true)
        {

            //for (int i = 0; i < tapEvents.Length; i++)
            //{
                //Debug.Log("\nTrack" + ( 1).ToString() +" "+ tapEvents[0].IsTapDown);
            //}

            yield return new WaitForSeconds(timeOut);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void TapPress(int track)
    {
        tapEvents[track - 1].IsTapDown = true;

    }

    public void TapRelease(int track)
    {
        tapEvents[track - 1].IsTapDown = false;

    }
}
