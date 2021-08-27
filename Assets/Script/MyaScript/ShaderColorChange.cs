using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShaderColorChange : MonoBehaviour
{

    Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (material.HasProperty("_Color"))
        {
            float val = Mathf.PingPong(Time.time, 2.0F);
            Color color = new Color(1.0f-Mathf.Pow(val,2),1.0f,1.0f);
            material.SetColor("_Color", color);
        }
    }
}
