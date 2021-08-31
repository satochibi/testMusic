using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JudgeUI : MonoBehaviour
{
    [SerializeField]
    //UI�I�u�W�F�N�g
    public GameObject[] UI={};
    //�A�^�b�`����Ă���e�A�j���[�^�[
    private Animator[] animator ={ };

    //����ɂ����2D�A�j���[�V�������Đ��@JudgementType(����)�ɑΉ��B
    public void JudgeUIAnimationPlay(JudgementType type)
    { 
        //int�ɃL���X�g���čĐ��B
        //State:Anim |layer 0 |normlizedTime 0.0f|
        animator[(int)type].Play("Anim",0,0.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[�̔z��̃T�C�Y��UI�I�u�W�F�N�g�Ɠ����ɂ���B
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
