using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeUI : MonoBehaviour
{
    public bool isEnd = false;

    private Animator animator;

    public void JudgeUIAnimationPlay()
    { 
        animator.Play("Anim",0,0.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(isEnd)
        //{
        //    animator.SetInteger("PlayFlag", 0);
        //    isEnd = false;
        //}
    }
}
