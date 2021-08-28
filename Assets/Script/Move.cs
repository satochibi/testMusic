using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    //ノーツを移動させるスクリプト
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    GameObject AudioObject;

    public float Speed { get { return this.speed; } }


    AudioClip clip;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        clip = AudioObject.GetComponent<AudioSource>().clip;
    }

    public void GameStart(GameObject sender)
    {
        sender.SetActive(false);
        rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
        AudioObject.SetActive(true);
    }

}
