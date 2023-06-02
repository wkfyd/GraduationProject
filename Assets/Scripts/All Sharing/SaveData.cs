using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public int isSocra;
    public int isPlato;
    public int isAristo;
    public int isPytha;
    public int isArchi;
    public int isThales;
    public int isEpicuru;
    public int isZeno;
    public int isDiog;
    public int isProta;
    public int isThrasy;
    public int isGorgi;
    public int isHippa;
    public int isEucli;
    public int isStoicism;
    public int isEpicuri;
    public int isSophist;

    public int Tutorial;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GameSave()
    {
        PlayerPrefs.SetInt("Tutorial", Tutorial);

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
