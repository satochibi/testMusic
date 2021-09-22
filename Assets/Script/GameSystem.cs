using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//�X�R�A�����N
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
    //�e�����N�̃X�R�A�{�[�_�[
    public int[] m_rankBorder =
    {
        950000,
        900000,
        850000,
        800000,
        750000,
        700000
    };
    //���U���g�Ŏg�p����p�����[�^
    public struct ResultPalam
    {

        public string MusicTitle;   //�~���[�W�b�N�^�C�g��
        public Difficulty difficulty; //��Փx

        public int score;           //�X�R�A       
        public ScoreRankType Rank;  //�X�R�A�����N
        
        public int perfect;         //Perfect�̐�        
        public int great;           //Great�̐�
        public int good;            //Good�̐�
        public int bad;             //Bad�̐�
        public int miss;            //Miss�̐�
        
        public int MaxCombo;        //�ő�R���{��
        public bool isFullCombo;    //�t���R���{�������ǂ���
        //public bool isAllPerfect;


    }
   
    public struct PlayerPalam
    {
        public CharacterType character;
    }

   
    //���U���g�ϐ�
    public ResultPalam result;

    public PlayerPalam playerPalam;
    //�R���{��
    int combo = 0;
    public int Combo { set { this.combo = value; } get { return this.combo; } }
    //���m�[�c��
    int notesnum = 0;
    //����w���P�̏ꍇ�̃X�R�A
    double normalScore = 0.00;
    //�v�Z�p�X�R�A
    double sumscore = 0.0;
    //�ȏI���t���O
    public bool IsEnd { get; set; } = false;
    AudioSource music;

    //����̋L�^�f�[�^
    SaveDataPalam saveData = new SaveDataPalam();
    //�f�o�b�O�p�^�b�v����
    public void DebugTap()
    {
        //�����_������
        int a = Random.Range(0, 20);


        //���g�K���������菈���������ł���Ώ��������Ďg������B
        //���ۂ̃X�R�A�v�Z(��)�F�P�m�[�c������@(1,000,000/�m�[�c��)������w��
        //����w��:Perfect 1.0,Great 0.9,Good 0.7,Bad 0.5, Miss 0.0
        switch (a)
        {
            case 0:
                result.great++;
                result.score += (int)(normalScore * 0.9);
                combo++;
                Debug.Log("Great!");
                //�ő�R���{�X�V�I
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
    //�^�b�v���̏���
    public void AddResultPalam(JudgementType judge)
    {



        //���ۂ̃X�R�A�v�Z(��)�F�P�m�[�c������@(1,000,000/�S�m�[�c��)������w��
        //����w��:Perfect 1.0,Great 0.9,Good 0.7,Bad 0.5, Miss 0.0
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
                //�ő�R���{�X�V�I
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
        //�\���y�ыL�^�p�p�����[�^�Ɍv�Z�p�X�R�A���l�̌ܓ��{int�^�ɃL���X�g���đ���B
        // m_result.score = (int)Mathf.Round(sumscore, System.MidpointRounding.AwayFromZero);
        result.score = (int)System.Math.Round(sumscore, System.MidpointRounding.AwayFromZero);
        //m_result.score = (int)sumscore;

    }
    //���U���g�p�p�����[�^���擾
    public ResultPalam GetResultPalam()
    {
        return result;
    }
    public void SetMusicName(string name)
    {
        result.MusicTitle = name;
    }
    //�m�[�c�̑�����ݒ�
    public void SetNotesNum(int num)
    {
        notesnum = num;
    }
    public void MusicTitleDisp(string titlename)
    {
        GameObject.Find("Canvas/MusicTitle").GetComponent<MusicTitle>().Disp(titlename);
    }
    //�V�[��
    public void ChangeScene(string scenename)
    {
        if (scenename == "Musicselect")
        {
            InitializedPalam();

            Debug.Log("Result�p�����[�^�����������܂����B");
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
            Debug.Log("�Z�[�u�f�[�^��������܂���I");
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

        Debug.Log(result.MusicTitle + "��" + result.difficulty.ToString() + "�̃Z�[�u�f�[�^���쐬���܂����B");
        writer.Write(Json);
        writer.Flush();
        writer.Close();
    }

    //�i�t��
    public void SetRank(int score)
    {
        int num = m_rankBorder[1];
        //�����N�{�[�_�[/50000�Ő����͈�switch
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

            //JUI�I�u�W�F�N�g�擾�B
            JUI = GameObject.Find("JudgeUI");

            normalScore = 1000000.00 / notesnum;

            music = GameObject.Find("Audio").GetComponent<AudioSource>();
            if (!string.IsNullOrEmpty(result.MusicTitle))
            {
                music.clip = Resources.Load<AudioClip>("MusicF/" + result.MusicTitle);
            }
            //music.Play(); //�{�^���ŉ��y�𗬂��悤�ɂ������̂ŁAplay�͕ۗ�
        }


        //���U���g�V�[����GO
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
        //���U���g�p�����[�^���f�o�b�O�\��
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
    //�V�[�����؂�ւ���Ă��c�葱����
    //�V���O���g���p�^�[�������v
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
