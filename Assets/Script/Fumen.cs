using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fumen : MonoBehaviour
{
    //�m�[�c���ړ�������X�N���v�g
    public float speed = 10f;
    private Rigidbody rb;
    public int notesNum =0;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
        //this.transform.GetChildCount
    }

}
