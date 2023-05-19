using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    public Animator anim;
    public GameObject[] layout;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
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

    public void BackButton()
    {
        anim.SetBool("isTrigger", false);
    }

    public void CanvasAnim()
    {

    }
}
