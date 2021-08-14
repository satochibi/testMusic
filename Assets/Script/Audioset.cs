using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audioset : MonoBehaviour
{
    //オーディオのオフセット用
    public GameObject AudioObject;
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        //オーディオにセットされた曲データの回収
        clip = AudioObject.GetComponent<AudioSource>().clip;
        this.audioAwake();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //オフセットの時間及びオフセット起動用
    
    //オーディオのオブジェクトをアクティブ化
    void audioAwake()
    {
        AudioObject.SetActive(true);
    }
}
