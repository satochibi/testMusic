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

    [SerializeField]
    //���U���g�Ŏg�p����p�����[�^
    public struct ResultPalam
    {

        public string MusicTitle;   //�~���[�W�b�N�^�C�g��
        public Difficulty difficulty; //��Փx

        public int score;           //�X�R�A       
        public ScoreRankType Rank;  //�X�R�A�����N
        
        public int Perfect;         //Perfect�̐�        
        public int Great;           //Great�̐�
        public int Good;            //Good�̐�
        public int Bad;             //Bad�̐�
        public int Miss;            //Miss�̐�
        
        public int MaxCombo;        //�ő�R���{��
        public bool isFullCombo;    //�t���R���{�������ǂ���
        //public bool isAllPerfect;


    }
    [SerializeField]
    public struct PlayerPalam
    {
        public CharacterType character;
    }

    [SerializeField]
    //���U���g�ϐ�
    public ResultPalam m_result;

    public PlayerPalam p_palam;
    //�R���{��
    public int Combo = 0;
    //���m�[�c��
    public int notesnum = 0;
    //����w���P�̏ꍇ�̃X�R�A
    public double normalscore = 0.00;
    //�v�Z�p�X�R�A
    public double sumscore = 0.0;
    //�ȏI���t���O
    public bool IsEnd = false;
    AudioSource music;

    //����̋L�^�f�[�^
    SaveDataPalam saveData;
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
                m_result.Great++;
                m_result.score += (int)(normalscore * 0.9);
                Combo++;
                Debug.Log("Great!");
                //�ő�R���{�X�V�I
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
    //�^�b�v���̏���
    public void AddResultPalam(JudgementType judge)
    {



        //���ۂ̃X�R�A�v�Z(��)�F�P�m�[�c������@(1,000,000/�S�m�[�c��)������w��
        //����w��:Perfect 1.0,Great 0.9,Good 0.7,Bad 0.5, Miss 0.0
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
                //�ő�R���{�X�V�I
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
        //�\���y�ыL�^�p�p�����[�^�Ɍv�Z�p�X�R�A���l�̌ܓ��{int�^�ɃL���X�g���đ���B
        // m_result.score = (int)Mathf.Round(sumscore, System.MidpointRounding.AwayFromZero);
        m_result.score = (int)System.Math.Round(sumscore, System.MidpointRounding.AwayFromZero);
        //m_result.score = (int)sumscore;

    }
    //���U���g�p�p�����[�^���擾
    public ResultPalam GetResultPalam()
    {
        return m_result;
    }
    public void SetMusicName(string name)
    {
        m_result.MusicTitle = name;
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

        Debug.Log(m_result.MusicTitle + "��" + m_result.difficulty.ToString() + "�̃Z�[�u�f�[�^���쐬���܂����B");
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

            //JUI�I�u�W�F�N�g�擾�B
            JUI = GameObject.Find("JudgeUI");

            normalscore = 1000000.00 / notesnum;

            music = GameObject.Find("Audio").GetComponent<AudioSource>();
            if (!string.IsNullOrEmpty(m_result.MusicTitle))
            {
                music.clip = Resources.Load<AudioClip>("MusicF/" + m_result.MusicTitle);
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
            SetRank(m_result.score);
            saveData.highscore = m_result.score;
            saveData.rankType = m_result.Rank;
            saveData.isFullCombo = m_result.isFullCombo;
            ComparePastData();
            WritePlayData();
            IsEnd = false;
            SceneManager.LoadScene("Result");
        }
        //���U���g�p�����[�^���f�o�b�O�\��
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
