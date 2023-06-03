using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void SkipButtonDown()  //On Click() �Լ�
    {
        //��Ʈ�� �� ��ŵ ��
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            SceneManager.LoadScene("Select Stage");
        }

        //���� �� ��ŵ ��
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            SceneManager.LoadScene("EndingCredit");
        }

        //���� ũ���� ��ŵ ��
        if (SceneManager.GetActiveScene().name == "EndingCredit")
        {
            SceneManager.LoadScene("GameTitle");
        }

        //���� ���� �� ��ŵ ��
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("GameTitle");
        }

        //�̿��� ��ŵ ��
        if (SceneManager.GetActiveScene().name == "Simulation") /* && [� ĳ�������� �Ǻ� ������ ����] */
        {
            SceneManager.LoadScene("GameTitle"); /*&& ĳ���� �� ���� ȭ���� ����� �־�� �մϴ�*/
        }
    }
}
