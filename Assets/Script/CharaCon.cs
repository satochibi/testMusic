using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCon : MonoBehaviour
{
    //�L�����N�^�[�p
    public GameObject idolchara;
    public GameObject movechara;

    GameSystem manager;

    GameSystem.ResultPalam testPalam;
    private void Awake()
    {
        idolchara.SetActive(true);
        movechara.SetActive(false);

    }

   

    private void Start()
    {
        //manager = GameObject.Find("GameManager").GetComponent<GameSystem>();
    }

  

    public void Tap()
    {
        //�L�����N�^�[�p�i���[�u���[�h�ɕύX�j
        idolchara.SetActive(false);
        movechara.SetActive(true);

        //JudgementType type = JudgementType.Perfect;
        //manager.AddResultPalam(type);
        //manager.DebugTap();

    }
    public void Exit()
    {
        //�L�����N�^�[�p�i�A�C�h�����[�h�ɖ߂��j
        idolchara.SetActive(true);
        movechara.SetActive(false);
    }
}

