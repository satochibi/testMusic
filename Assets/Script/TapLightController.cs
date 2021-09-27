using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapLightController : MonoBehaviour
{
    //判定ライン上のライトの点灯及び消灯スクリプト
    public float speed = 1.0f;
    private new Renderer renderer;
    private bool lightFlag = false;

    GameSystem manager;

    GameSystem.ResultPalam testPalam;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        
    }

    private void OnEnable()
    {
        Default();
    }

    private void Default()
    {
        //初期光量
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
    }

    private void Start()
    {
        if (speed == 0)
        {
            //消灯までの早さ
            speed = 1;
        }
        manager = GameObject.Find("GameManager").GetComponent<GameSystem>();
    }

    private void Update()
    {
        //アルファ値減少
        if (renderer.material.color.a <= 0)
        {
            this.enabled = false;
        }
        else
        {
            if (lightFlag == false)
            {
                var alfa = renderer.material.color.a - speed * Time.deltaTime;
                renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alfa);
            }
        }
    }

    public void LightUp()
    {
        //押された際に光らせる動作
        Default();
        this.enabled = true;
        Debug.Log("Tap");
        lightFlag = true;

       
    }
    public void LightDown()
    {
        //判定外に出たときの消灯用フラグ
        lightFlag = false;
    }
}
