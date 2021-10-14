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

        public string musicTitle;   //�~���[�W�b�N�^�C�g��
        public Difficulty difficulty; //��Փx

        public int score;           //�X�R�A       
        public ScoreRankType rank;  //�X�R�A�����N
        
        public int perfect;         //Perfect�̐�        
        public int great;           //Great�̐�
        public int good;            //Good�̐�
        public int bad;             //Bad�̐�
        public int miss;            //Miss�̐�
        
        public int maxCombo;        //�ő�R���{��
        public bool isFullCombo;    //�t���R���{�������ǂ���
        public bool isHighScore;    //�n�C�X�R�A���ǂ����B
        public int ScoreDiff;       //�O�̃X�R�A�̍�        
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
                //�ő�R���{�X�V�I
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
        result.musicTitle = name;
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
        string savepath = SaveDataManager.Path + result.musicTitle + "/" + result.difficulty.ToString() + ".json";
        if (!System.IO.File.Exists(savepath))
        {
            Debug.Log("�Z�[�u�f�[�^��������܂���I");
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

        Debug.Log(result.musicTitle + "��" + result.difficulty.ToString() + "�̃Z�[�u�f�[�^���쐬���܂����B");
        writer.Write(Json);
        writer.Flush();
        writer.Close();
    }

    //�i�t��
    public void SetRank(int score)
    {
       
        //�����N�{�[�_�[/50000�Ő����͈�switch
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

            //JUI�I�u�W�F�N�g�擾�B
            jUI = GameObject.Find("JudgeUI");

            normalScore = 1000000.00 / notesnum;

            
        }
        //�f�o�b�O�p
        if (SceneManager.GetActiveScene().name == "MultiTapTest" && jUI == null)
        {

            //JUI�I�u�W�F�N�g�擾�B
            jUI = GameObject.Find("JudgeUI");

            normalScore = 1000000.00 / notesnum;

            music = GameObject.Find("Audio").GetComponent<AudioSource>();
            if (!string.IsNullOrEmpty(result.musicTitle))
            {
                music.clip = Resources.Load<AudioClip>("MusicF/" + result.musicTitle);
            }
        }
        //���U���g�V�[����GO
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
        //���U���g�p�����[�^���f�o�b�O�\��
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
    //�V�[�����؂�ւ���Ă��c�葱����
    //�V���O���g���p�^�[������
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
