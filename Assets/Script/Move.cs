using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //�m�[�c���ړ�������X�N���v�g
    public float speed = 10f;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
    }

}
