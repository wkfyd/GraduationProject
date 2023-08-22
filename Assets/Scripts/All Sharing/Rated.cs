using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rated : MonoBehaviour
{
    float time = 0;
    private float fadeLength = 0.7f;    //페이드 아웃 수행 시간
    private float fadeStartTime = 5.0f; //페이드 아웃이 시작되는 초


    void Update()
    {
        float fadeTime = time - fadeStartTime;

        if (time < 1.0f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, time / 0.3f);  //페이드 인
        }
        else if (fadeTime < fadeLength)
        {
            float time3 = fadeTime / fadeLength;
            GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(1.0f, 0.0f, time3));    //선형보간으로 페이드 아웃

        }
        
        else if (time > 6.0f)
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        
    }

    
    
}
