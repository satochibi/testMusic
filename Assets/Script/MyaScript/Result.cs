using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Result : MonoBehaviour
{

    public enum ResultPalam
    { 
        score,
        Perfect,
        Great,
        Good,
        Bad,
        Miss,
        MaxCombo,
        Rank
    }
    public ScoreRankType m_rank;
    public ResultPalam m_resultPalam;
    private GameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.Find("GameManager").GetComponent<GameSystem>();

        switch (m_resultPalam)
        {
            case ResultPalam.score:
                GetComponent<Text>().text = gameSystem.GetResultPalam().score.ToString();

                break;
            case ResultPalam.Perfect:
                GetComponent<Text>().text = gameSystem.GetResultPalam().perfect.ToString();

                break;
            case ResultPalam.Great:
                GetComponent<Text>().text = gameSystem.GetResultPalam().great.ToString();

                break;
            case ResultPalam.Good:
                GetComponent<Text>().text = gameSystem.GetResultPalam().good.ToString();

                break;
            case ResultPalam.Bad:
                GetComponent<Text>().text = gameSystem.GetResultPalam().bad.ToString();

                break;
            case ResultPalam.Miss:
                GetComponent<Text>().text = gameSystem.GetResultPalam().miss.ToString();

                break;
            case ResultPalam.MaxCombo:
                GetComponent<Text>().text = gameSystem.GetResultPalam().maxCombo.ToString();

                break;
            case ResultPalam.Rank:
                if (gameSystem.GetResultPalam().rank == m_rank)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                break;
        }
       
    }
    // Update is called once per frame
    void Update()
    {

    }
}