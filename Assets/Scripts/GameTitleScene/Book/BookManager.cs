using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;

    public int currentPage;

    public GameObject[] layout;
    public GameObject[] enemyPortrait;
    public GameObject[] PortraitPanel;
    public GameObject[] pageButton;
    public GameObject titleEffect;

    void Start()
    {
        Color color = new Color(255, 255, 255);

        if (SaveData.isSocra == 1)
        {
            enemyPortrait[0].GetComponent<Image>().color = color;
            PortraitPanel[0].SetActive(true);
        }

        if (SaveData.isPlato == 1)
        {
            enemyPortrait[1].GetComponent<Image>().color = color;
            PortraitPanel[1].SetActive(true);
        }

        if (SaveData.isAristo == 1)
        {
            enemyPortrait[2].GetComponent<Image>().color = color;
            PortraitPanel[2].SetActive(true);
        }

        if (SaveData.isPytha == 1)
        {
            enemyPortrait[3].GetComponent<Image>().color = color;
            PortraitPanel[3].SetActive(true);
        }

        if (SaveData.isArchi == 1)
        {
            enemyPortrait[4].GetComponent<Image>().color = color;
            PortraitPanel[4].SetActive(true);
        }

        if (SaveData.isThales == 1)
        {
            enemyPortrait[5].GetComponent<Image>().color = color;
            PortraitPanel[5].SetActive(true);
        }

        if (SaveData.isEpicuru == 1)
        {
            enemyPortrait[6].GetComponent<Image>().color = color;
            PortraitPanel[6].SetActive(true);
        }

        if (SaveData.isZeno == 1)
        {
            enemyPortrait[7].GetComponent<Image>().color = color;
            PortraitPanel[7].SetActive(true);
        }

        if (SaveData.isDiog == 1)
        {
            enemyPortrait[8].GetComponent<Image>().color = color;
            PortraitPanel[8].SetActive(true);
        }

        if (SaveData.isProta == 1)
        {
            enemyPortrait[9].GetComponent<Image>().color = color;
            PortraitPanel[9].SetActive(true);
        }

        if (SaveData.isThrasy == 1)
        {
            enemyPortrait[10].GetComponent<Image>().color = color;
            PortraitPanel[10].SetActive(true);
        }

        if (SaveData.isGorgi == 1)
        {
            enemyPortrait[11].GetComponent<Image>().color = color;
            PortraitPanel[11].SetActive(true);
        }

        if (SaveData.isHippa == 1)
        {
            enemyPortrait[12].GetComponent<Image>().color = color;
            PortraitPanel[12].SetActive(true);
        }

        if (SaveData.isEucli == 1)
        {
            enemyPortrait[13].GetComponent<Image>().color = color;
            PortraitPanel[13].SetActive(true);
        }

        if (SaveData.isStoicism == 1)
        {
            enemyPortrait[14].GetComponent<Image>().color = color;
            PortraitPanel[14].SetActive(true);
        }

        if (SaveData.isEpicuri == 1)
        {
            enemyPortrait[15].GetComponent<Image>().color = color;
            PortraitPanel[15].SetActive(true);
        }

        if (SaveData.isSophist == 1)
        {
            enemyPortrait[16].GetComponent<Image>().color = color;
            PortraitPanel[16].SetActive(true);
        }
    }

    public void Page_1()
    {
        for (int i = 0; i < 4; i++)
        {
            pageButton[i].SetActive(true);
            layout[i].SetActive(false);
        }

        currentPage = 1;

        pageButton[0].SetActive(false);
        layout[0].SetActive(true);
    }

    public void Page_2()
    {
        for (int i = 0; i < 4; i++)
        {
            pageButton[i].SetActive(true);
            layout[i].SetActive(false);
        }

        currentPage = 2;

        pageButton[1].SetActive(false);
        layout[1].SetActive(true);
    }

    public void Page_3()
    {
        for (int i = 0; i < 4; i++)
        {
            pageButton[i].SetActive(true);
            layout[i].SetActive(false);
        }

        currentPage = 3;

        pageButton[2].SetActive(false);
        layout[2].SetActive(true);
    }

    public void Page_4()
    {
        for (int i = 0; i < 4; i++)
        {
            pageButton[i].SetActive(true);
            layout[i].SetActive(false);
        }

        currentPage = 4;

        pageButton[3].SetActive(false);
        layout[3].SetActive(true);
    }

    public void Page_Left()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (currentPage == 1)
                return;
            else if (currentPage == i)
            {
                pageButton[i - 1].SetActive(true);
                layout[i - 1].SetActive(false);

                pageButton[i - 2].SetActive(false);
                layout[i - 2].SetActive(true);

                currentPage = i - 1;
                break;
            }
        }
    }

    public void Page_Right()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (currentPage == 4)
                return;
            else if (currentPage == i)
            {
                pageButton[i - 1].SetActive(true);
                layout[i - 1].SetActive(false);

                pageButton[i].SetActive(false);
                layout[i].SetActive(true);

                currentPage = i + 1;
                break;
            }
        }
    }

    public void OpenBook()
    {
        bookCanvas.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            layout[i].SetActive(false);
            pageButton[i].SetActive(true);
        }

        currentPage = 1;

        pageButton[0].SetActive(false);
        layout[0].SetActive(true);
    }

    public void BackButton()
    {
        bookCanvas.SetActive(false);
        titleEffect.SetActive(true);
    }
}
