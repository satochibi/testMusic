using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicNode : MonoBehaviour
{

    public void SetFileName()
    {
        GameObject.Find("GameManager").GetComponent<GameSystem>().SetMusicName(GetComponent<Text>().text);
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
