using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    Track track;
    public List<GameObject> lightOBJ;
    public bool swipeFlag =false;
    public bool touchFlag =  false ;
    public int[] touchTime; 
    public  void IsSwipe(bool flag)
    {
        swipeFlag = flag;
    }
    public void TouchON()
    {
        touchFlag = true;
    }
    public void TouchOFF()
    {
        touchFlag = false;
    }
    public void Swipe(int lanenum)
    {
        if(touchFlag)
        {
            lightOBJ[lanenum].GetComponent<TapL>().Tap();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
