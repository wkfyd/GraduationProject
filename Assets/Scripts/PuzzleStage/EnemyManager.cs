using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameManager gm;
    public Enemy enemyData;

    public GameObject start;
    public GameObject enemy_StartText;

    public SpriteRenderer enemy_Status;
    public Image healthBar;
    public Sprite[] enemy_Sprite;

    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI NextSp_Text;
    public TextMeshProUGUI NextAtk_Text;

    public int enemy_MaxHealth;
    public int enemy_Health;
    public int enemy_AtkTurn;
    public int enemy_NomAtk;
    public int enemy_SpTurn;
    public int enemy_NomSp;
    public int enemy_Atk_Damage;

    //흔들기효과 변수
    public GameObject enemy;

    void Awake()
    {
        enemy_Status = GameObject.FindWithTag("Enemy").GetComponent<SpriteRenderer>();
        enemy = GameObject.FindWithTag("Enemy");
    }

    void Start()
    {
        enemy_MaxHealth = enemyData.GetMaxHealth();
        enemy_Health = enemyData.GetHealth();

        enemy_AtkTurn = enemyData.Get_AtkTurn();
        enemy_NomAtk = enemy_AtkTurn;
        enemy_Atk_Damage = enemyData.Get_Atk_Damage();

        enemy_SpTurn = enemyData.Get_SpTurn();
        enemy_NomSp = enemy_SpTurn;

        NextAtk_Text.text = enemy_NomAtk.ToString();

        enemy_Sprite = enemyData.GetSprite(1017);

    }

    void Update()
    {
        healthBar.fillAmount = (float)enemy_Health / enemy_MaxHealth;

        if (gm.turns < gm.curt_turns)
        {
            SetNext_Atk();
            AtkedShake();

            gm.turns = gm.curt_turns;
        }
    }

    //피격시 흔들림
    void AtkedShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float shakeIntensity = 1f; // 뒤흔들기 강도
        float shakeDuration = 0.4f; // 뒤흔들기 지속 시간

        Vector3 initialPosition; // 초기 위치
        float elapsedTime = 0f; // 경과 시간

        initialPosition = enemy.transform.position;

        int framCount = 0;

        while (framCount == 50)
        {
            if (elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;

                // 뒤흔들기 효과를 위해 무작위로 값을 생성하여 카메라 위치를 변경
                Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
                enemy.transform.position = initialPosition + shakeOffset;
                Debug.Log(shakeOffset);
            }
            framCount++;
            yield return Time.deltaTime;
        }
        
    }
    //일반공격
    void SetNext_Atk()
    {
        enemy_NomAtk--;

        if (enemy_NomAtk == 0)
        {
            StartCoroutine(Change_Atk());

            enemy_NomAtk = enemy_AtkTurn;
        }

        NextAtk_Text.text = enemy_NomAtk.ToString();
    }

    IEnumerator Change_Atk()
    {
        enemy_Status.sprite = enemy_Sprite[1];

        yield return new WaitForSeconds(1f);

        enemy_Status.sprite = enemy_Sprite[0];
    }

    public void GameStart()
    {
        start.SetActive(false);
        enemy_StartText.SetActive(true);

        StartCoroutine(EndTalk());
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(5.0f);

        enemy_StartText.SetActive(false);
    }
}
