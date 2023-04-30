using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    public Block block;

    public float maxHp = 100;
    public float curHp = 100;
    void Start()
    {
        hpbar.value = (float)curHp / (float)maxHp;
    }
    void Update()
    {
        HandleHp();
    }
    public void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)curHp / (float)maxHp, Time.deltaTime * 10);
    }
}
