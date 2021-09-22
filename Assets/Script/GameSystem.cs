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
    Easy,
    Normal,
    Hard,
    DifficaltyType
}

public class SaveDataPalam
{
    
    int highscore = 0;
    

    public int Highscore
    {
        get{ return this.highscore; }
        set {
            if (value < 0)
            {
                this.highscore = value;
            }
        }
    }
    public bool IsFullCombo { set; get; } = false;
    public ScoreRankType RankType { set; get; }
}


public class GameSystem : MonoBehaviour
{
    static GameSystem instance;
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
    //リザルトで使用するパラメータ
    public struct ResultPalam
    {

        public string MusicTitle;   //ミュージックタイトル
        public Difficulty difficulty; //難易度

        public int score;           //スコア       
        public ScoreRankType Rank;  //スコアランク
        
        public int perfect;         //Perfectの数        
        public int great;           //Greatの数
        public int good;            //Goodの数
        public int bad;             //Badの数
        public int miss;            //Missの数
        
        public int MaxCombo;        //最大コンボ数
        public bool isFullCombo;    //フルコンボしたかどうか
        //public bool isAllPerfect;


    }
   
    public struct PlayerPalam
    {
        public CharacterType character;
    }

   
    //リザルト変数
    public ResultPalam result;

    public PlayerPalam playerPalam;
    //コンボ数
    int combo = 0;
    public int Combo { set { this.combo = value; } get { return this.combo; } }
    //総ノーツ数
    int notesnum = 0;
    //判定指数１の場合のスコア
    double normalScore = 0.00;
    //計算用スコア
    double sumscore = 0.0;
    //曲終了フラグ
    public bool IsEnd { get; set; } = false;
    AudioSource music;

    //今回の記録データ
    SaveDataPalam saveData = new SaveDataPalam();
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
                result.great++;
                result.score += (int)(normalScore * 0.9);
                combo++;
                Debug.Log("Great!");
                //最大コンボ更新！
                if (result.MaxCombo < combo)
                {
                    result.MaxCombo = combo;
                }
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                break;
            case 1:
                result.great++;
                result.score += (int)(normalScore * 0.9);
                combo++;
                Debug.Log("Great!");
                if (result.MaxCombo < combo)
                {
                    result.MaxCombo = combo;
                }
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                break;
            case 2:
                result.good++;
                result.score += (int)(normalScore * 0.7);
                Debug.Log("Good");
                combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Good);
                break;
            case 3:
                result.bad++;
                Debug.Log("Bad");
                result.score += (int)(normalScore * 0.5);
                combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Bad);
                break;
            case 4:
                result.miss++;
                Debug.Log("Miss");
                combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Miss);
                break;

            default:
                result.perfect++;
                sumscore += normalScore;
                Debug.Log("Perfect!");
                combo++;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Perfect);
                if (result.MaxCombo < combo)
                {
                    result.MaxCombo = combo;
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

                result.perfect++;
                sumscore += normalScore;
                Debug.Log("Perfect!");
                combo++;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Perfect);
                if (result.MaxCombo < combo)
                {
                    result.MaxCombo = combo;
                }
                break;
            case JudgementType.Great:
                result.great++;
                sumscore += normalScore * 0.9f;
                combo++;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                Debug.Log("Great!");
                //最大コンボ更新！
                if (result.MaxCombo < combo)
                {
                    result.MaxCombo = combo;
                }
                break;

            case JudgementType.Good:
                result.good++;
                sumscore += normalScore * 0.7f;
                Debug.Log("Good");
                combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Good);
                break;
            case JudgementType.Bad:
                result.bad++;
                sumscore += normalScore * 0.5f;
                Debug.Log("Bad");
                combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Bad);
                break;
            case JudgementType.Miss:
                result.miss++;
                Debug.Log("Miss");
                combo = 0;
                JUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Miss);
                break;

            default:


                break;
        }
        //表示及び記録用パラメータに計算用スコアを四捨五入＋int型にキャストして代入。
        // m_result.score = (int)Mathf.Round(sumscore, System.MidpointRounding.AwayFromZero);
        result.score = (int)System.Math.Round(sumscore, System.MidpointRounding.AwayFromZero);
        //m_result.score = (int)sumscore;

    }
    //リザルト用パラメータを取得
    public ResultPalam GetResultPalam()
    {
        return result;
    }
    public void SetMusicName(string name)
    {
        result.MusicTitle = name;
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
        SetRank(result.score);
        IsEnd = false;
        SceneManager.LoadScene(scenename);

    }

    public void InitializedPalam()
    {
        result = new ResultPalam();
        saveData = new SaveDataPalam();
        combo = 0;
        sumscore = 0.00f;

    }
    

    public void ComparePastData()
    {
        string savepath = SaveDataManager.Path + result.MusicTitle + "/" + result.difficulty.ToString() + ".json";
        if (!System.IO.File.Exists(savepath))
        {
            Debug.Log("セーブデータが見つかりません！");
            return;
        }
        SaveDataPalam pastsaveData;
        pastsaveData = new SaveDataPalam();
        pastsaveData = JsonUtility.FromJson<SaveDataPalam>(System.IO.File.ReadAllText(savepath));
        if (pastsaveData.Highscore > saveData.Highscore)
        {
            saveData.Highscore = pastsaveData.Highscore;
        }
        if ((int)pastsaveData.Highscore < (int)saveData.Highscore)
        {
            saveData.Highscore = pastsaveData.Highscore;
        }
        if (pastsaveData.IsFullCombo == true)
        {
            saveData.IsFullCombo = true;
        }
    }
    public void WritePlayData()
    {
        var Json = JsonUtility.ToJson(saveData);
        System.IO.StreamWriter writer;
        writer = new System.IO.StreamWriter(SaveDataManager.Path + result.MusicTitle + "/" + result.difficulty.ToString() + ".json", false);

        Debug.Log(result.MusicTitle + "の" + result.difficulty.ToString() + "のセーブデータを作成しました。");
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
                result.Rank = ScoreRankType.C;
                break;
            case 15:
                result.Rank = ScoreRankType.B;
                break;
            case 16:
                result.Rank = ScoreRankType.A;
                break;
            case 17:
                result.Rank = ScoreRankType.S;
                break;
            case 18:
                result.Rank = ScoreRankType.SS;
                break;
            case 19:
                result.Rank = ScoreRankType.SSS;
                break;
            case 20:
                result.Rank = ScoreRankType.SSS;
                break;
            default:
                result.Rank = ScoreRankType.D;
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
     

        if (SceneManager.GetActiveScene().name == GameSceneName && JUI == null)
        {

            //JUIオブジェクト取得。
            JUI = GameObject.Find("JudgeUI");

            normalScore = 1000000.00 / notesnum;

            music = GameObject.Find("Audio").GetComponent<AudioSource>();
            if (!string.IsNullOrEmpty(result.MusicTitle))
            {
                music.clip = Resources.Load<AudioClip>("MusicF/" + result.MusicTitle);
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
            SetRank(result.score);
            saveData.Highscore = result.score;
            saveData.RankType = result.Rank;
            saveData.IsFullCombo = result.isFullCombo;
            ComparePastData();
            WritePlayData();
            IsEnd = false;
            SceneManager.LoadScene("Result");
        }
        //リザルトパラメータをデバッグ表示
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("score " + result.score +
                "\nRank " + result.Rank +
                "\nMaxCombo " + result.MaxCombo +
                "\nPerfect " + result.perfect +
                "\nGreat " + result.great +
                "\nGood " + result.good +
                "\nBad " + result.bad +
                "\nMiss " + result.miss);
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
