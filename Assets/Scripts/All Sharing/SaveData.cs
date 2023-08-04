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

    public static int currentStage;
    public static int currentEnemy_Id;
    public static int freeHighScore;
    public static int isLanguage;  //0이 영어, 1이 그리스

    //타이틀 느낌표
    public static int newBook;

    public static int isGameOver;  //게임오버 후 타이틀씬 돌아올 때 판단용
    public static int isTutorial;
    public static int isEnding;

    //느낌표 아이콘 캐릭터별
    public static int newChar01;
    public static int newChar02;
    public static int newChar03;
    public static int newChar04;
    public static int newChar05;
    public static int newChar06;
    public static int newChar07;
    public static int newChar08;
    public static int newChar09;
    public static int newChar10;
    public static int newChar11;
    public static int newChar12;
    public static int newChar13;
    public static int newChar14;
    public static int newChar15;
    public static int newChar16;
    public static int newChar17;

    public static void GameSave()
    {
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

        PlayerPrefs.SetInt("currentStage", currentStage);
        PlayerPrefs.SetInt("currentEnemy_Id", currentEnemy_Id);
        PlayerPrefs.SetInt("freeHighScore", freeHighScore);
        PlayerPrefs.SetInt("isLanguage", isLanguage);

        PlayerPrefs.SetInt("newBook", newBook);

        PlayerPrefs.SetInt("isGameOver", isGameOver);
        PlayerPrefs.SetInt("Tutorial", isTutorial);
        PlayerPrefs.SetInt("ending", isEnding);
    }

    public static void GameLoad()
    {
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

        currentStage = PlayerPrefs.GetInt("currentStage");
        currentEnemy_Id = PlayerPrefs.GetInt("currentEnemy_Id");
        freeHighScore = PlayerPrefs.GetInt("freeHighScore");
        isLanguage = PlayerPrefs.GetInt("isLanguage");

        newBook = PlayerPrefs.GetInt("newBook");

        isGameOver = PlayerPrefs.GetInt("isGameOver");
        isTutorial = PlayerPrefs.GetInt("Tutorial");
        isEnding = PlayerPrefs.GetInt("ending");

    }

    public static void NewPlay()
    {
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

        currentStage = 0;
        currentEnemy_Id = 0;
        isLanguage = 0;
        freeHighScore = 0;

        newBook = 0;

        isGameOver = 0;
        isTutorial = 0;
        isEnding = 0;
    }
}
