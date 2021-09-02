using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scoredisp : MonoBehaviour
{

    GameSystem manager;
   
    int combonum =0;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = manager.GetResultPalam().score.ToString();
        
       
    }
}
