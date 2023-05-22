using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TypeEffect talk;
    public GameObject Tutorial;
    public TextMeshProUGUI textPro;

    string text;

    void Start()
    {
        text = "아니야. 당황하지 말고 침착하자!\n난 몸과 마음이 건강한 철학자니까!" +
                "\n파피루스 든 녀석들을 전부 때려잡으면 내 발표 자료를 되찾을 수 있을 거야!";

        StartText();
    }
    public void Action()
    {
        Talk();
    }
    void StartText()
    {
        talk.SetMsg("시간이 없어. 서둘러 내 발표 자료를 되찾아야 해!" +
            "\n그런데 왜 오늘따라 너도나도 파피루스 두루마리를 들고 다니는 거지?", 0);
    }

    void Talk()
    {
        if (text == textPro.text)
        {
            Tutorial.SetActive(false);
        }
        else
        {
            talk.SetMsg("아니야. 당황하지 말고 침착하자!\n난 몸과 마음이 건강한 철학자니까!" +
                "\n파피루스 든 녀석들을 전부 때려잡으면 내 발표 자료를 되찾을 수 있을 거야!", 0);
        }
    }
}
