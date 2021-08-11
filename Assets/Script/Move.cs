using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //ノーツを移動させるスクリプト
    public float speed = 10f;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
    }

}
