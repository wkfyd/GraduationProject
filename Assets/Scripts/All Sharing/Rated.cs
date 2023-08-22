using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rated : MonoBehaviour
{
    float time = 0;
    private float fadeLength = 0.7f;    //���̵� �ƿ� ���� �ð�
    private float fadeStartTime = 5.0f; //���̵� �ƿ��� ���۵Ǵ� ��


    void Update()
    {
        float fadeTime = time - fadeStartTime;

        if (time < 1.0f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, time / 0.3f);  //���̵� ��
        }
        else if (fadeTime < fadeLength)
        {
            float time3 = fadeTime / fadeLength;
            GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(1.0f, 0.0f, time3));    //������������ ���̵� �ƿ�

        }
        
        else if (time > 6.0f)
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        
    }

    
    
}
