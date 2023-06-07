using System.Collections;
using System.Collections.Generic;
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

    public static int Tutorial;
    public static int ending;

    public static int currentStage;
    public static int currentEnemy_Id = 1008;

    void Awake()
    {
        SaveData save = FindObjectOfType<SaveData>();

        if (save != null)
            DontDestroyOnLoad(gameObject);
        else
            return;
    }

    public void GameSave()
    {
        PlayerPrefs.SetInt("Tutorial", Tutorial);
        PlayerPrefs.SetInt("ending", ending);

        PlayerPrefs.SetInt("currentStage", currentStage);
        PlayerPrefs.SetInt("currentEnemy_Id", currentEnemy_Id);

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

    public void GameLoad()
    {
        Tutorial = PlayerPrefs.GetInt("Tutorial");
        ending = PlayerPrefs.GetInt("ending");

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
}
