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
        text = "�ƴϾ�. ��Ȳ���� ���� ħ������!\n�� ���� ������ �ǰ��� ö���ڴϱ�!" +
                "\n���Ƿ罺 �� �༮���� ���� ���������� �� ��ǥ �ڷḦ ��ã�� �� ���� �ž�!";

        StartText();
    }
    public void Action()
    {
        Talk();
    }
    void StartText()
    {
        talk.SetMsg("�ð��� ����. ���ѷ� �� ��ǥ �ڷḦ ��ã�ƾ� ��!" +
            "\n�׷��� �� ���õ��� �ʵ����� ���Ƿ罺 �η縶���� ��� �ٴϴ� ����?", 0);
    }

    void Talk()
    {
        if (text == textPro.text)
        {
            Tutorial.SetActive(false);
        }
        else
        {
            talk.SetMsg("�ƴϾ�. ��Ȳ���� ���� ħ������!\n�� ���� ������ �ǰ��� ö���ڴϱ�!" +
                "\n���Ƿ罺 �� �༮���� ���� ���������� �� ��ǥ �ڷḦ ��ã�� �� ���� �ž�!", 0);
        }
    }
}
