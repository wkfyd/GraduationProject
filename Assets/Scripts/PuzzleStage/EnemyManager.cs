using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameManager gm;
    public Enemy enemyData;
    public Player player;

    public GameObject start;
    public GameObject enemyTalk;
    public TextMeshProUGUI enemyText;

    public Image healthBar;

    public SpriteRenderer enemy_Status;
    public Sprite[] enemy_Sprite;

    public GameObject[] player_Status;

    public TextMeshProUGUI NextSp_Text;
    public TextMeshProUGUI NextAtk_Text;

    public int enemy_MaxHealth;
    public int enemy_Health;
    [HideInInspector]
    public int enemy_DamageHP;
    public int enemy_AtkTurn;
    public int enemy_NomAtk;
    public int enemy_SpTurn;
    public int enemy_NomSp;
    public int enemy_Atk_Damage;

    Coroutine enemyAtked;
    Coroutine enemyAtk;
    Coroutine playerAtked;

    //흔들기효과 변수
    public GameObject enemy;

    void Awake()
    {
        enemy_Status = GameObject.FindWithTag("Enemy").GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        enemy_MaxHealth = enemyData.GetMaxHealth();
        enemy_Health = enemyData.GetHealth();
        enemy_DamageHP = enemy_Health;

        enemy_AtkTurn = enemyData.Get_AtkTurn();
        enemy_NomAtk = enemy_AtkTurn;
        enemy_Atk_Damage = enemyData.Get_Atk_Damage();

        enemy_SpTurn = enemyData.Get_SpTurn();
        enemy_NomSp = enemy_SpTurn;

        NextSp_Text.text = enemy_NomSp.ToString();
        NextAtk_Text.text = enemy_NomAtk.ToString();

        enemy_Sprite = enemyData.GetSprite(1017);

    }

    void Update()
    {
        healthBar.fillAmount = (float)enemy_Health / enemy_MaxHealth;

        //피격
        if (enemy_Health > enemy_DamageHP)
        {
            Enemy_atked();
            enemy_Health = enemy_DamageHP;
        }

        //공격
        if (gm.turns < gm.curt_turns)
        {
            Set_Atk();

            if (enemy_SpTurn != 0)
                Set_Sp();

            gm.turns = gm.curt_turns;
        }
    }

    //적 피격
    void Enemy_atked()
    {
        //AtkedShake();

        if (enemyAtked != null)             //코루틴정지
            StopCoroutine(enemyAtked);

        enemyAtked = StartCoroutine(Change_Atked());
    }

    IEnumerator Change_Atked()
    {
        enemy_Status.sprite = enemy_Sprite[2];

        yield return new WaitForSeconds(0.5f);

        enemy_Status.sprite = enemy_Sprite[0];
    }

    //피격시 흔들림
    void AtkedShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float shakeIntensity = 1f; // 뒤흔들기 강도
        float shakeDuration = 10f; // 뒤흔들기 지속 시간

        Vector3 initialPosition; // 초기 위치
        float elapsedTime = 0f; // 경과 시간

        initialPosition = enemy.transform.localPosition;

        int framCount = 0;

        while (framCount < 50)
        {
            if (elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;

                // 뒤흔들기 효과를 위해 무작위로 값을 생성하여 카메라 위치를 변경
                Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
                enemy.transform.localPosition = initialPosition + shakeOffset;
                Debug.Log(enemy.transform.localPosition);
            }
            framCount++;
            yield return Time.deltaTime;
        }
    }

    //일반공격
    void Set_Atk()
    {
        enemy_NomAtk--;

        if (enemy_NomAtk == 0)
        {
            player.pc_Health = player.pc_Health - enemy_Atk_Damage;


            if (enemyAtk != null)             //코루틴정지
                StopCoroutine(enemyAtk);
            enemyAtk = StartCoroutine(Change_Atk());

            if (playerAtked != null)             //코루틴정지
                StopCoroutine(playerAtked);
            playerAtked = StartCoroutine(Player_Atked());

            enemy_NomAtk = enemy_AtkTurn;
        }

        NextAtk_Text.text = enemy_NomAtk.ToString();
    }

    IEnumerator Change_Atk()
    {
        enemy_Status.sprite = enemy_Sprite[1];

        yield return new WaitForSeconds(0.5f);

        enemy_Status.sprite = enemy_Sprite[0];
    }

    IEnumerator Player_Atked()
    {
        player_Status[0].SetActive(false);
        player_Status[1].SetActive(false);
        player_Status[2].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        player_Status[0].SetActive(true);
        player_Status[1].SetActive(false);
        player_Status[2].SetActive(false);
    }

    //스폐셜공격
    void Set_Sp()
    {
        enemy_NomSp--;

        if (enemy_NomSp == 0)
            enemy_NomSp = enemy_SpTurn;

        NextSp_Text.text = enemy_NomSp.ToString();
    }
    public void GameStart()
    {
        start.SetActive(false);
        enemyTalk.SetActive(true);

        StartCoroutine(EndTalk());
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(5.0f);

        enemyTalk.SetActive(false);
    }
}
