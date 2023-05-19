using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    public GameObject[] layout;

    void Start()
    {

    }


    void Update()
    {

    }

    public void openBook()
    {
        bookCanvas.gameObject.SetActive(true);
    }
}
