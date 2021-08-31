using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JudgeUI : MonoBehaviour
{
    [SerializeField]
    //UIオブジェクト
    public GameObject[] UI={};
    //アタッチされている各アニメーター
    private Animator[] animator ={ };

    //判定によって2Dアニメーションを再生　JudgementType(引数)に対応。
    public void JudgeUIAnimationPlay(JudgementType type)
    { 
        //intにキャストして再生。
        //State:Anim |layer 0 |normlizedTime 0.0f|
        animator[(int)type].Play("Anim",0,0.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        //アニメーターの配列のサイズをUIオブジェクトと同じにする。
        Array.Resize(ref animator, UI.Length);

        for (int i = 0; i < UI.Length; i++) {
            animator[i] = UI[i].GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
