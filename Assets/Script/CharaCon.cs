using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCon : MonoBehaviour
{
    //キャラクター用
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
        //キャラクター用（ムーブモードに変更）
        idolchara.SetActive(false);
        movechara.SetActive(true);

        //JudgementType type = JudgementType.Perfect;
        //manager.AddResultPalam(type);
        //manager.DebugTap();

    }
    public void Exit()
    {
        //キャラクター用（アイドルモードに戻す）
        idolchara.SetActive(true);
        movechara.SetActive(false);
    }
}

