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

    public Image enemy_HealthBar;

    public SpriteRenderer enemy_Idle;
    public SpriteRenderer player_Idle;
    public Sprite[] enemy_Sprite;
    public Sprite[] player_Sprite;

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
    Coroutine playerAtk;

    //����ȿ�� ����
    [HideInInspector]
    public GameObject enemy;

    void Start()
    {
        enemy_Idle = GameObject.FindWithTag("Enemy").GetComponent<SpriteRenderer>();

        //Enemy���� �޾ƿ���
        enemy_MaxHealth = enemyData.GetMaxHealth();
        enemy_Health = enemy_MaxHealth;
        enemy_DamageHP = enemy_Health;

        enemy_AtkTurn = enemyData.Get_AtkTurn();
        enemy_NomAtk = enemy_AtkTurn;

        enemy_Atk_Damage = enemyData.Get_Atk_Damage();

        enemy_SpTurn = enemyData.Get_SpTurn();
        enemy_NomSp = enemy_SpTurn;

        //���� �� str�������� ��ȯ
        NextSp_Text.text = enemy_NomSp.ToString();
        NextAtk_Text.text = enemy_NomAtk.ToString();

        enemy_Sprite = enemyData.GetSprite(enemyData.getId);
    }

    void Update()
    {
        enemy_HealthBar.fillAmount = (float)enemy_Health / enemy_MaxHealth;

        //�÷��̾� ����, �� �ǰ�
        if (enemy_Health > enemy_DamageHP)
        {
            Enemy_atked();

            enemy_Health = enemy_DamageHP;
        }

        //�÷��̾� �ǰ�, �� ����
        if (gm.turns < gm.curt_turns)
        {
            Enemy_Atk();

            if (enemy_SpTurn != 0)
                Enemy_Sp();

            gm.turns = gm.curt_turns;
        }
    }

    //�Ϲݰ���
    void Enemy_Atk()
    {
        enemy_NomAtk--;

        if (enemy_NomAtk == 0)
        {
            player.pc_Health = player.pc_Health - enemy_Atk_Damage;

            if (enemyAtk != null)             //�ڷ�ƾ����
                StopCoroutine(enemyAtk);
            enemyAtk = StartCoroutine(Change_Atk());

            if (playerAtked != null)          //�ڷ�ƾ����
                StopCoroutine(playerAtked);
            playerAtked = StartCoroutine(Player_Atked());

            enemy_NomAtk = enemy_AtkTurn;
        }

        NextAtk_Text.text = enemy_NomAtk.ToString();
    }

    //�� �ǰ�
    void Enemy_atked()
    {
        if (enemyAtked != null)             //�ڷ�ƾ����
            StopCoroutine(enemyAtked);

        enemyAtked = StartCoroutine(Change_Atked());
    }

    IEnumerator Change_Atked()
    {
        enemy_Idle.sprite = enemy_Sprite[2];

        yield return new WaitForSeconds(1f);

        enemy_Idle.sprite = enemy_Sprite[0];
    }

    IEnumerator Change_Atk()
    {
        enemy_Idle.sprite = enemy_Sprite[1];

        yield return new WaitForSeconds(1f);

        enemy_Idle.sprite = enemy_Sprite[0];
    }
    IEnumerator Player_Atk()
    {

        yield return new WaitForSeconds(1f);

    }

    IEnumerator Player_Atked()
    {

        yield return new WaitForSeconds(1f);

    }

    //����Ȱ���
    void Enemy_Sp()
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
        enemyText.text = enemyData.GetEnemyTalk(enemyData.getId, 0);

        StartCoroutine(EndTalk());
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(5.0f);

        enemyTalk.SetActive(false);
    }
}
