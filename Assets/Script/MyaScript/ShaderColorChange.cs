using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShaderColorChange : MonoBehaviour
{

    Material material;
    Color changeColor =Color.clear;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        
    }
    public void ChangeColor(Color color)
    {
        material.SetColor("_Color",color);
        changeColor = color;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (material.HasProperty("_Color"))
        {
            //float val = Mathf.PingPong(Time.time, 2.0F);
            //Color color = new Color(1.0f - Mathf.Pow(val, 2), 1.0f, 1.0f);
            Color color = Color.gray;
            if(changeColor != Color.clear)
            {
                color = changeColor;
                changeColor = Color.clear;
            }
            material.SetColor("_Color", color);
        }
        //ChangeColor(Color.black);
    }
}
