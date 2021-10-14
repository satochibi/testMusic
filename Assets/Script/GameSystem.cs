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

    //public int highscore { get; set; }
    //public bool isFullCombo { set; get; } = false;
    //public ScoreRankType rankType { set; get; }
    public int highscore = 0;
    public bool isFullCombo  = false;
    public ScoreRankType rankType = ScoreRankType.D;
}


public class GameSystem : MonoBehaviour
{
    static GameSystem instance;
    [SerializeField]
    GameObject jUI = null;
    static string gameSceneName = "SampleScene";
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

        public string musicTitle;   //ミュージックタイトル
        public Difficulty difficulty; //難易度

        public int score;           //スコア       
        public ScoreRankType rank;  //スコアランク
        
        public int perfect;         //Perfectの数        
        public int great;           //Greatの数
        public int good;            //Goodの数
        public int bad;             //Badの数
        public int miss;            //Missの数
        
        public int maxCombo;        //最大コンボ数
        public bool isFullCombo;    //フルコンボしたかどうか
        public bool isHighScore;    //ハイスコアかどうか。
        public int ScoreDiff;       //前のスコアの差        
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
                if (result.maxCombo < combo)
                {
                    result.maxCombo = combo;
                }
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                break;
            case 1:
                result.great++;
                result.score += (int)(normalScore * 0.9);
                combo++;
                Debug.Log("Great!");
                if (result.maxCombo < combo)
                {
                    result.maxCombo = combo;
                }
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                break;
            case 2:
                result.good++;
                result.score += (int)(normalScore * 0.7);
                Debug.Log("Good");
                combo = 0;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Good);
                break;
            case 3:
                result.bad++;
                Debug.Log("Bad");
                result.score += (int)(normalScore * 0.5);
                combo = 0;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Bad);
                break;
            case 4:
                result.miss++;
                Debug.Log("Miss");
                combo = 0;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Miss);
                break;

            default:
                result.perfect++;
                sumscore += normalScore;
                Debug.Log("Perfect!");
                combo++;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Perfect);
                if (result.maxCombo < combo)
                {
                    result.maxCombo = combo;
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
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Perfect);
                if (result.maxCombo < combo)
                {
                    result.maxCombo = combo;
                }
                break;
            case JudgementType.Great:
                result.great++;
                sumscore += normalScore * 0.9f;
                combo++;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Great);
                Debug.Log("Great!");
                //最大コンボ更新！
                if (result.maxCombo < combo)
                {
                    result.maxCombo = combo;
                }
                break;

            case JudgementType.Good:
                result.good++;
                sumscore += normalScore * 0.7f;
                Debug.Log("Good");
                combo = 0;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Good);
                break;
            case JudgementType.Bad:
                result.bad++;
                sumscore += normalScore * 0.5f;
                Debug.Log("Bad");
                combo = 0;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Bad);
                break;
            case JudgementType.Miss:
                result.miss++;
                Debug.Log("Miss");
                combo = 0;
                jUI.GetComponent<JudgeUI>().JudgeUIAnimationPlay(JudgementType.Miss);
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
        result.musicTitle = name;
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
        string savepath = SaveDataManager.Path + result.musicTitle + "/" + result.difficulty.ToString() + ".json";
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
        else
        {
            result.isHighScore = true;
            result.ScoreDiff = saveData.highscore - pastsaveData.highscore;
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
        writer = new System.IO.StreamWriter(SaveDataManager.Path + result.musicTitle + "/" + result.difficulty.ToString() + ".json", false);

        Debug.Log(result.musicTitle + "の" + result.difficulty.ToString() + "のセーブデータを作成しました。");
        writer.Write(Json);
        writer.Flush();
        writer.Close();
    }

    //格付け
    public void SetRank(int score)
    {
       
        //ランクボーダー/50000で整数範囲switch
        switch (score / 50000)
        {

            case 14:
                result.rank = ScoreRankType.C;
                break;
            case 15:
                result.rank = ScoreRankType.B;
                break;
            case 16:
                result.rank = ScoreRankType.A;
                break;
            case 17:
                result.rank = ScoreRankType.S;
                break;
            case 18:
                result.rank = ScoreRankType.SS;
                break;
            case 19:
                result.rank = ScoreRankType.SSS;
                break;
            case 20:
                result.rank = ScoreRankType.SSS;
                break;
            default:
                result.rank = ScoreRankType.D;
                return;

        }


    }
    // Start is called before the first frame update
    void Start()
    {
        //var a = 0;
        //var b = 0;
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:

                AudioSettings.SetDSPBufferSize(256, 4);
                break;

            case RuntimePlatform.Android:

                AudioSettings.SetDSPBufferSize(256, 4);
                break;

            default:

                AudioSettings.SetDSPBufferSize(1024, 4);
                //AudioSettings.GetDSPBufferSize(out a, out b);
                //Debug.Log(a + " " + b);
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
     

        if (SceneManager.GetActiveScene().name == gameSceneName && jUI == null)
        {

            //JUIオブジェクト取得。
            jUI = GameObject.Find("JudgeUI");

            normalScore = 1000000.00 / notesnum;

            
        }
        //デバッグ用
        if (SceneManager.GetActiveScene().name == "MultiTapTest" && jUI == null)
        {

            //JUIオブジェクト取得。
            jUI = GameObject.Find("JudgeUI");

            normalScore = 1000000.00 / notesnum;

            music = GameObject.Find("Audio").GetComponent<AudioSource>();
            if (!string.IsNullOrEmpty(result.musicTitle))
            {
                music.clip = Resources.Load<AudioClip>("MusicF/" + result.musicTitle);
            }
        }
        //リザルトシーンへGO
        if (Input.GetKeyDown(KeyCode.A))
        {
            IsEnd = true;

        }
        if (IsEnd)
        {
            if(result.good+result.bad+result.miss ==0)
            {
                result.isFullCombo = true;
            }
            SetRank(result.score);
            saveData.highscore = result.score;
            saveData.rankType = result.rank;
            saveData.isFullCombo = result.isFullCombo;
            ComparePastData();
            WritePlayData();
            IsEnd = false;
            SceneManager.LoadScene("Result");

        }
        //リザルトパラメータをデバッグ表示
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("score " + result.score +
                "\nRank " + result.rank +
                "\nMaxCombo " + result.maxCombo +
                "\nPerfect " + result.perfect +
                "\nGreat " + result.great +
                "\nGood " + result.good +
                "\nBad " + result.bad +
                "\nMiss " + result.miss);
        }

    }
    //シーンが切り替わっても残り続ける
    //シングルトンパターン導入
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
