using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//スコアランク
public enum ScoreRankType
{
    SSS = 0,
    SS,
    S,
    A,
    B,
    C,
    D,
    TypeNum
}
[System.Serializable]

public enum Difficulty
{
    easy,
    normal,
    hard,
    DifficaltyType
}

public class SaveDataPalam
{
    [SerializeField]

    public int highscore = 0;
    public bool isFullCombo = false;
    public ScoreRankType rankType = ScoreRankType.D;

}


public class GameSystem : MonoBehaviour
{


    public static GameSystem instance;
    [SerializeField]
    public GameObject JUI = null;
    public static string GameSceneName = "SampleScene";
    //各ランクのスコアボーダー
    public int[] m_rankBorder =
    {
        950000,
        900000,
        850000,
        800000,
        750000,
        700000
    };

    [SerializeField]
    //リザルトで使用するパラメータ
    public struct ResultPalam
    {

        public string MusicTitle;   //ミュージックタイトル
        public Difficulty difficulty; //難易度

        public int score;           //スコア       
        public ScoreRankType Rank;  //スコアランク
        
        public int Perfect;         //Perfectの数        
        public int Great;           //Greatの数
        public int Good;            //Goodの数
        public int Bad;             //Badの数
        public int Miss;            //Missの数
        
        public int MaxCombo;        //最大コンボ数
        public bool isFullCombo;    //フルコンボしたかどうか
        //public bool isAllPerfect;


    }
    [SerializeField]
    public struct PlayerPalam
    {
        public CharacterType character;
    }

    [SerializeField]
    //リザルト変数
    public ResultPalam m_result;

    public PlayerPalam p_palam;
    //コンボ数
    public int Combo = 0;
    //総ノーツ数
    public int notesnum = 0;
    //判定指数１の場合のスコア
    public double normalscore = 0.00;
    //計算用スコア
    public double sumscore = 0.0;
    //曲終了フラグ
    public bool IsEnd = false;
    AudioSource music;

    //今回の記録データ
    SaveDataPalam saveData;
    //デバッグ用タップ処理
    public void DebugTap()
    {
        //ランダム生成
        int a = Random.Range(0, 20);


        //中身適当だが判定処理が実装できれば書き換えて使うつもり。
        //実際のスコア計算(仮)：１ノーツあたり　(1,000,000/ノーツ数)＊判定指数
        //判定指数:Perfect 1.0,Great 0.9,Good 0.7,Bad 0.5, Miss 0.0
        switch (a)
        {
            case 0:
                m_result.Great++;
                m_result.score += (int)(normalscore * 0.9);
                Combo++;
                Debug.Log("Great!");
                //最大コンボ更新！
                if (m_result.MaxCombo < Combo)
                {
                    m_result.MaxCombo = Combo;
                }
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                break;
            case 1:
                m_result.Great++;
                m_result.score += (int)(normalscore * 0.9);
                Combo++;
                Debug.Log("Great!");
                if (m_result.MaxCombo < Combo)
                {
                    m_result.MaxCombo = Combo;
                }
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                break;
            case 2:
                m_result.Good++;
                m_result.score += (int)(normalscore * 0.7);
                Debug.Log("Good");
                Combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Good);
                break;
            case 3:
                m_result.Bad++;
                Debug.Log("Bad");
                m_result.score += (int)(normalscore * 0.5);
                Combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Bad);
                break;
            case 4:
                m_result.Miss++;
                Debug.Log("Miss");
                Combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Miss);
                break;

            default:
                m_result.Perfect++;
                sumscore += normalscore;
                Debug.Log("Perfect!");
                Combo++;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Perfect);
                if (m_result.MaxCombo < Combo)
                {
                    m_result.MaxCombo = Combo;
                }
                break;
        }

    }
    //タップ時の処理
    public void AddResultPalam(JudgementType judge)
    {



        //実際のスコア計算(仮)：１ノーツあたり　(1,000,000/全ノーツ数)＊判定指数
        //判定指数:Perfect 1.0,Great 0.9,Good 0.7,Bad 0.5, Miss 0.0
        switch (judge)
        {

            case JudgementType.Perfect:

                m_result.Perfect++;
                sumscore += normalscore;
                Debug.Log("Perfect!");
                Combo++;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Perfect);
                if (m_result.MaxCombo < Combo)
                {
                    m_result.MaxCombo = Combo;
                }
                break;
            case JudgementType.Great:
                m_result.Great++;
                sumscore += normalscore * 0.9f;
                Combo++;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                Debug.Log("Great!");
                //最大コンボ更新！
                if (m_result.MaxCombo < Combo)
                {
                    m_result.MaxCombo = Combo;
                }
                break;

            case JudgementType.Good:
                m_result.Good++;
                sumscore += normalscore * 0.7f;
                Debug.Log("Good");
                Combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Good);
                break;
            case JudgementType.Bad:
                m_result.Bad++;
                sumscore += normalscore * 0.5f;
                Debug.Log("Bad");
                Combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Bad);
                break;
            case JudgementType.Miss:
                m_result.Miss++;
                Debug.Log("Miss");
                Combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Miss);
                break;

            default:


                break;
        }
        //表示及び記録用パラメータに計算用スコアを四捨五入＋int型にキャストして代入。
        // m_result.score = (int)Mathf.Round(sumscore, System.MidpointRounding.AwayFromZero);
        m_result.score = (int)System.Math.Round(sumscore, System.MidpointRounding.AwayFromZero);
        //m_result.score = (int)sumscore;

    }
    //リザルト用パラメータを取得
    public ResultPalam GetResultPalam()
    {
        return m_result;
    }
    public void SetMusicName(string name)
    {
        m_result.MusicTitle = name;
    }
    //ノーツの総数を設定
    public void SetNotesNum(int num)
    {
        notesnum = num;
    }
    public void MusicTitleDisp(string titlename)
    {
        GameObject.Find("Canvas/MusicTitle").GetComponent<MusicTitle>().Disp(titlename);
    }
    //シーン
    public void ChangeScene(string scenename)
    {
        if (scenename == "Musicselect")
        {
            InitializedPalam();

            Debug.Log("Resultパラメータを初期化しました。");
        }
        SetRank(m_result.score);
        IsEnd = false;
        SceneManager.LoadScene(scenename);

    }

    public void InitializedPalam()
    {
        m_result = new ResultPalam();
        saveData = new SaveDataPalam();
        Combo = 0;
        sumscore = 0.00f;

    }
    

    public void ComparePastData()
    {
        string savepath = SaveDataManager.Path + m_result.MusicTitle + "/" + m_result.difficulty.ToString() + ".json";
        if (!System.IO.File.Exists(savepath))
        {
            Debug.Log("セーブデータが見つかりません！");
            return;
        }
        SaveDataPalam pastsaveData;
        pastsaveData = new SaveDataPalam();
        pastsaveData = JsonUtility.FromJson<SaveDataPalam>(System.IO.File.ReadAllText(savepath));
        if (pastsaveData.highscore > saveData.highscore)
        {
            saveData.highscore = pastsaveData.highscore;
        }
        if ((int)pastsaveData.rankType < (int)saveData.rankType)
        {
            saveData.rankType = pastsaveData.rankType;
        }
        if (pastsaveData.isFullCombo == true)
        {
            saveData.isFullCombo = true;
        }
    }
    public void WritePlayData()
    {
        var Json = JsonUtility.ToJson(saveData);
        System.IO.StreamWriter writer;
        writer = new System.IO.StreamWriter(SaveDataManager.Path + m_result.MusicTitle + "/" + m_result.difficulty.ToString() + ".json", false);

        Debug.Log(m_result.MusicTitle + "の" + m_result.difficulty.ToString() + "のセーブデータを作成しました。");
        writer.Write(Json);
        writer.Flush();
        writer.Close();
    }

    //格付け
    public void SetRank(int score)
    {
        int num = m_rankBorder[1];
        //ランクボーダー/50000で整数範囲switch
        switch (score / 50000)
        {

            case 14:
                m_result.Rank = ScoreRankType.C;
                break;
            case 15:
                m_result.Rank = ScoreRankType.B;
                break;
            case 16:
                m_result.Rank = ScoreRankType.A;
                break;
            case 17:
                m_result.Rank = ScoreRankType.S;
                break;
            case 18:
                m_result.Rank = ScoreRankType.SS;
                break;
            case 19:
                m_result.Rank = ScoreRankType.SSS;
                break;
            case 20:
                m_result.Rank = ScoreRankType.SSS;
                break;
            default:
                m_result.Rank = ScoreRankType.D;
                return;

        }


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DebugTap();
        }


        if (SceneManager.GetActiveScene().name == GameSceneName && JUI == null)
        {

            //JUIオブジェクト取得。
            JUI = GameObject.Find("JudgeUI");

            normalscore = 1000000.00 / notesnum;

            music = GameObject.Find("Audio").GetComponent<AudioSource>();
            if (!string.IsNullOrEmpty(m_result.MusicTitle))
            {
                music.clip = Resources.Load<AudioClip>("MusicF/" + m_result.MusicTitle);
            }
            //music.Play(); //ボタンで音楽を流すようにしたいので、playは保留
        }


        //リザルトシーンへGO
        if (Input.GetKeyDown(KeyCode.A))
        {
            IsEnd = true;

        }
        if (IsEnd)
        {
            SetRank(m_result.score);
            saveData.highscore = m_result.score;
            saveData.rankType = m_result.Rank;
            saveData.isFullCombo = m_result.isFullCombo;
            ComparePastData();
            WritePlayData();
            IsEnd = false;
            SceneManager.LoadScene("Result");
        }
        //リザルトパラメータをデバッグ表示
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("score " + m_result.score +
                "\nRank " + m_result.Rank +
                "\nMaxCombo " + m_result.MaxCombo +
                "\nPerfect " + m_result.Perfect +
                "\nGreat " + m_result.Great +
                "\nGood " + m_result.Good +
                "\nBad " + m_result.Bad +
                "\nMiss " + m_result.Miss);
        }

    }
    //シーンが切り替わっても残り続ける
    //シングルトンパターン導入」
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
