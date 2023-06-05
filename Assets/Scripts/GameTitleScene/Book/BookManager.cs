using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;

    public int currentPage;

    public GameObject[] layout;
    public GameObject[] pageButton;
    public GameObject titleEffect;

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
