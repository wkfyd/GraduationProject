using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static int isSocra;
    public static int isPlato;
    public static int isAristo;
    public static int isPytha;
    public static int isArchi;
    public static int isThales;
    public static int isEpicuru;
    public static int isZeno;
    public static int isDiog;
    public static int isProta;
    public static int isThrasy;
    public static int isGorgi;
    public static int isHippa;
    public static int isEucli;
    public static int isStoicism;
    public static int isEpicuri;
    public static int isSophist;

    public static int isTutorial;
    public static int isEnding;

    public static int currentStage;
    public static int currentEnemy_Id = 1008;
    public static int isGameOver;  //게임오버 후 타이틀씬 돌아올 때 판단용

    public static int isLanguage;  //0이 영어, 1이 그리스

    public int isSocra1;
    public int isPlato1;
    public int isAristo1;
    public int isPytha1;
    public int isArchi1;
    public int isThales1;
    public int isEpicuru1;
    public int isZeno1;
    public int isDiog1;
    public int isProta1;
    public int isThrasy1;
    public int isGorgi1;
    public int isHippa1;
    public int isEucli1;
    public int isStoicism1;
    public int isEpicuri1;
    public int isSophist1;

    public int isTutorial1;
    public int isEnding1;

    public int currentStage1;
    public int currentEnemy_Id1;
    public int isGameOver1;



    void Awake()
    {
        SaveData save = FindObjectOfType<SaveData>();

        if (save != null)
            DontDestroyOnLoad(gameObject);
        else
            return;
    }

    void Update()
    {
        isSocra1 = isSocra;
        isPlato1 = isPlato;
        isAristo1 = isAristo;
        isPytha1 = isPytha;
        isArchi1 = isArchi;
        isThales1 = isThales;
        isEpicuru1 = isEpicuru;
        isZeno1 = isZeno;
        isDiog1 = isDiog;
        isProta1 = isProta;
        isThrasy1 = isThrasy;
        isGorgi1 = isGorgi;
        isHippa1 = isHippa;
        isEucli1 = isEucli;
        isStoicism1 = isStoicism;
        isEpicuri1 = isEpicuri;
        isSophist1 = isSophist;

        isTutorial1 = isTutorial;
        isEnding1 = isEnding;

        currentStage1 = currentStage;
        currentEnemy_Id1 = currentEnemy_Id;
        isGameOver1 = isGameOver;

        Debug.Log(isTutorial);
        Debug.Log(currentEnemy_Id);
    }

    public static void GameSave()
    {
        PlayerPrefs.SetInt("Tutorial", isTutorial);
        PlayerPrefs.SetInt("ending", isEnding);

        PlayerPrefs.SetInt("currentStage", currentStage);
        PlayerPrefs.SetInt("currentEnemy_Id", currentEnemy_Id);
        PlayerPrefs.SetInt("isLanguage", isLanguage);

        PlayerPrefs.SetInt("isSocra", isSocra);
        PlayerPrefs.SetInt("isPlato", isPlato);
        PlayerPrefs.SetInt("isAristo", isAristo);
        PlayerPrefs.SetInt("isPytha", isPytha);
        PlayerPrefs.SetInt("isArchi", isArchi);
        PlayerPrefs.SetInt("isThales", isThales);
        PlayerPrefs.SetInt("isEpicuru", isEpicuru);
        PlayerPrefs.SetInt("isZeno", isZeno);
        PlayerPrefs.SetInt("isDiog", isDiog);
        PlayerPrefs.SetInt("isProta", isProta);
        PlayerPrefs.SetInt("isThrasy", isThrasy);
        PlayerPrefs.SetInt("isGorgi", isGorgi);
        PlayerPrefs.SetInt("isHippa", isHippa);
        PlayerPrefs.SetInt("isEucli", isEucli);
        PlayerPrefs.SetInt("isStoicism", isStoicism);
        PlayerPrefs.SetInt("isEpicuri", isEpicuri);
        PlayerPrefs.SetInt("isSophist", isSophist);
    }

    public static void GameLoad()
    {
        isTutorial = PlayerPrefs.GetInt("Tutorial");
        isEnding = PlayerPrefs.GetInt("ending");

        currentStage = PlayerPrefs.GetInt("currentStage");
        currentEnemy_Id = PlayerPrefs.GetInt("currentEnemy_Id");

        isSocra = PlayerPrefs.GetInt("isSocra");
        isPlato = PlayerPrefs.GetInt("isPlato");
        isAristo = PlayerPrefs.GetInt("isAristo");
        isPytha = PlayerPrefs.GetInt("isPytha");
        isArchi = PlayerPrefs.GetInt("isArchi");
        isThales = PlayerPrefs.GetInt("isThales");
        isEpicuru = PlayerPrefs.GetInt("isEpicuru");
        isZeno = PlayerPrefs.GetInt("isZeno");
        isDiog = PlayerPrefs.GetInt("isDiog");
        isProta = PlayerPrefs.GetInt("isProta");
        isThrasy = PlayerPrefs.GetInt("isThrasy");
        isGorgi = PlayerPrefs.GetInt("isGorgi");
        isHippa = PlayerPrefs.GetInt("isHippa");
        isEucli = PlayerPrefs.GetInt("isEucli");
        isStoicism = PlayerPrefs.GetInt("isStoicism");
        isEpicuri = PlayerPrefs.GetInt("isEpicuri");
        isSophist = PlayerPrefs.GetInt("isSophist");
    }

    public static void NewPlay()
    {
        isTutorial = 0;
        isEnding = 0;

        currentStage = 0;
        currentEnemy_Id = 0;

        isSocra = 0;
        isPlato = 0;
        isAristo = 0;
        isPytha = 0;
        isArchi = 0;
        isThales = 0;
        isEpicuru = 0;
        isZeno = 0;
        isDiog = 0;
        isProta = 0;
        isThrasy = 0;
        isGorgi = 0;
        isHippa = 0;
        isEucli = 0;
        isStoicism = 0;
        isEpicuri = 0;
        isSophist = 0;
    }
}
