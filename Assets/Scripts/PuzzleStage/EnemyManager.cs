using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    System.Random random = new System.Random();

    public GameManager gameManager;
    public Enemy enemyData;
    public Player player;

    public GameObject start;
    public GameObject enemyTalk;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI enemyNameText;

    public Image enemy_HealthBar;

    public SpriteRenderer enemy_Idle;
    public SpriteRenderer player_Idle;
    public Sprite[] enemy_Sprite;
    public Sprite[] player_Sprite;

    public TextMeshProUGUI NextSp_Text;
    public GameObject SpAtkObj;
    public Image SpAtkImg;
    public Animator spAnim;
    public TextMeshProUGUI spNameText;
    public TextMeshProUGUI spText;
    public TextMeshProUGUI NextAtk_Text;
    public GameObject damagePrefabs;        //대미지 텍스트 프리팹
    public Transform damageTextGroup;       //대미지 텍스트 위치
    public GameObject playerPrefabs;        //콤보 텍스트 프리팹
    public Transform playerTextGroup;       //콤보 텍스트 위치

    public float enemy_MaxHealth;     //적 최대 체력
    public float enemy_Health;        //적 현재 체력
    [HideInInspector]
    public float enemy_DamageHP;      //적이 데미지를 받고 난 후 체력 (피격 유/무 판단 시 사용)
    public int enemy_AtkTurn;       //적 일반 공격 턴 수
    public int enemy_NomAtk;        //적 일반 공격 남은 턴
    public int enemy_Atk_Damage;    //적 일반 공격 데미지
    public int enemy_SpTurn;        //적 스폐셜 공격 턴 수
    public int enemy_NomSp;         //적 스폐셜 공격 남은 턴

    public bool isEnemy_Sp;         //적이 스폐셜 공격 중인지 확인
    public bool isEnemy_Atk;        //적이 일반 공격 중인지 확인
    public bool isPlyaer_Atk;       //플레이어가 공격 중인지 확인

    public bool isAristo_Sp;        //아리스토텔레스 스폐셜 효과 상태
    public int aristo_Sp_NomTurn;   //아리스토텔레스 스폐셜 남은 턴
    public bool isArchi_Sp;         //아르키메데스 스폐셜 효과 상태
    public int archi_Sp_NomTurn;    //아르키메데스 스폐셜 남은 턴
    public bool isThales_Sp;        //탈레스 스폐셜 효과 상태
    public int thales_Sp_NomTurn;   //탈레스 스폐셜 남은 턴
    public bool isEpicuru_Sp;       //에피쿠로스 스폐셜 효과 상태
    public int epicuru_Sp_NomTurn;  //에피쿠로스 스폐셜 남은 턴
    public bool isZeno_Sp;          //제논 스폐셜 효과 상태

    Coroutine enemyAtked;
    Coroutine enemyAtk;
    Coroutine playerAtked;
    Coroutine playerAtk;

    //흔들기효과 변수
    [HideInInspector]
    public GameObject enemy;

    void Start()
    {
        enemy_Idle = GameObject.FindWithTag("Enemy").GetComponent<SpriteRenderer>();

        //Enemy정보 받아오기
        enemy_MaxHealth = enemyData.GetMaxHealth();
        enemy_Health = enemy_MaxHealth;
        enemy_DamageHP = enemy_Health;

        enemy_AtkTurn = enemyData.Get_AtkTurn();
        enemy_NomAtk = enemy_AtkTurn;

        enemy_Atk_Damage = enemyData.Get_Atk_Damage();

        enemy_SpTurn = enemyData.Get_SpTurn();
        enemy_NomSp = enemy_SpTurn;

        enemyNameText.text = enemyData.Get_enemyName();
        spNameText.text = enemyData.Get_SpNameText();
        spText.text = enemyData.Get_SpText();

        SpAtkImg.sprite = enemyData.SetEnemySp();

        //남은 턴 str형식으로 변환
        NextSp_Text.text = enemy_NomSp.ToString();
        NextAtk_Text.text = enemy_NomAtk.ToString();

        enemy_Sprite = enemyData.GetSprite(enemyData.get_Enemy_Id);
    }

    void Update()
    {
        enemy_HealthBar.fillAmount = (float)enemy_Health / enemy_MaxHealth;


        //플레이어 공격, 적 피격 (적의 체력이 닳음이 조건 -> 아리스토텔레스 처럼 무적경우 조건 충족 못 함)
        if (enemy_Health > enemy_DamageHP)
        {
            PlayerAtk();

            if (enemy_NomSp != 1)
                Enemy_atked();

            enemy_Health = enemy_DamageHP;
        }

        //플레이어 피격, 적 공격
        if (gameManager.turns < gameManager.curt_turns)
        {
            Enemy_Atk();

            //스폐셜 공격이 0이 아닐시 = 적이 1, 2등급일시
            if (enemy_SpTurn != 0)
                Enemy_Sp();

            //아르키메데스 스폐셜 공격효과
            if (SaveData.currentEnemy_Id == 1005)
            {
                if (isArchi_Sp && (archi_Sp_NomTurn >= 1 && archi_Sp_NomTurn <= 7))
                {
                    player.pc_Health -= (enemy_Atk_Damage);
                    archi_Sp_NomTurn--;

                    if (archi_Sp_NomTurn == 0)
                        isArchi_Sp = false;
                }
            }

            gameManager.turns = gameManager.curt_turns;
        }

        //게임 승리시 플레이어 공격, 적 피격
        if (gameManager.gameWin)
        {
            enemy_Idle.sprite = enemy_Sprite[2];
            player_Idle.sprite = player_Sprite[1];
        }

    }

    //플레이어 공격
    public void PlayerAtk()
    {
        isPlyaer_Atk = true;

        if (playerAtk != null)             //코루틴정지
            StopCoroutine(playerAtk);

        playerAtk = StartCoroutine(Player_Atk_Routine());
    }


    //일반공격
    void Enemy_Atk()
    {
        enemy_NomAtk--;

        if (enemy_NomAtk == 0)
        {
            isEnemy_Atk = true;

            //탈레스 스폐셜 효과
            if (SaveData.currentEnemy_Id == 1006 && isThales_Sp
                && (thales_Sp_NomTurn >= 1 && thales_Sp_NomTurn <= 2))
            {
                player.pc_Health -= enemy_Atk_Damage * 4;

                thales_Sp_NomTurn--;

                if (thales_Sp_NomTurn == 0)
                    isThales_Sp = false;
            }

            else
                player.pc_Health -= enemy_Atk_Damage;

            if (enemyAtk != null)             //코루틴정지
                StopCoroutine(enemyAtk);

            enemyAtk = StartCoroutine(Change_Enemy_Atk());

            if (playerAtked != null)          //코루틴정지
                StopCoroutine(playerAtked);
            playerAtked = StartCoroutine(Player_Atked_Routine());

            enemy_NomAtk = enemy_AtkTurn;
        }

        NextAtk_Text.text = enemy_NomAtk.ToString();
    }

    //적 피격
    void Enemy_atked()
    {
        //대미지 텍스트 변경 및 재생
        damagePrefabs.GetComponent<TextMeshProUGUI>().text = (enemy_Health - enemy_DamageHP).ToString();
        Instantiate(damagePrefabs, damageTextGroup);


        if (enemyAtked != null)             //코루틴정지
            StopCoroutine(enemyAtked);

        enemyAtked = StartCoroutine(Change_Enemy_Atked());
    }

    IEnumerator Change_Enemy_Atked()
    {
        enemy_Idle.sprite = enemy_Sprite[2];

        yield return new WaitForSeconds(1f);

        enemy_Idle.sprite = enemy_Sprite[0];
    }

    IEnumerator Change_Enemy_Atk()
    {
        enemy_Idle.sprite = enemy_Sprite[1];

        yield return new WaitForSeconds(1f);

        enemy_Idle.sprite = enemy_Sprite[0];

        isEnemy_Atk = false;
    }

    public IEnumerator Player_Atk_Routine()
    {
        player_Idle.sprite = player_Sprite[1];

        yield return new WaitForSeconds(1f);

        player_Idle.sprite = player_Sprite[0];

        isPlyaer_Atk = false;
    }

    IEnumerator Player_Atked_Routine()
    {
        player_Idle.sprite = player_Sprite[2];

        yield return new WaitForSeconds(1f);

        if (gameManager.gameWin)
        {
            player_Idle.sprite = player_Sprite[1];
        }
        else if (gameManager.isOver)
        {
            player_Idle.sprite = player_Sprite[2];
        }
        else
            player_Idle.sprite = player_Sprite[0];
    }


    //스폐셜공격
    void Enemy_Sp()
    {
        enemy_NomSp--;

        if (enemy_NomSp == 0)
        {
            StopAllCoroutines();
            Use_EnemySp();

            enemy_NomSp = enemy_SpTurn;
        }

        NextSp_Text.text = enemy_NomSp.ToString();
    }

    public void Use_EnemySp()
    {
        isEnemy_Sp = true;
        isPlyaer_Atk = false;

        StartCoroutine(SpCoroutine());
    }

    IEnumerator SpCoroutine()
    {
        SpAtkObj.SetActive(true);

        spAnim.SetBool("isSpAtk", true);

        enemy_Idle.sprite = enemy_Sprite[1];
        player_Idle.sprite = player_Sprite[0];

        yield return new WaitForSeconds(4.0f);

        spAnim.SetBool("isSpAtk", false);

        enemy_Idle.sprite = enemy_Sprite[0];


        //SpAtk 효과
        switch (SaveData.currentEnemy_Id)
        {
            case 1001:
                player.pc_Health /= 2;
                break;

            case 1002:
                player.pc_Health -= random.Next(90, 121);
                break;

            case 1003:
                isAristo_Sp = true;
                aristo_Sp_NomTurn = 3;
                break;

            case 1004:
                player.pc_Health -= 100;
                break;

            case 1005:
                isArchi_Sp = true;
                archi_Sp_NomTurn = 7;
                break;

            case 1006:
                isThales_Sp = true;
                thales_Sp_NomTurn = 2;
                break;

            case 1007:
                isEpicuru_Sp = true;
                epicuru_Sp_NomTurn = 3;
                break;

            case 1008:
                isZeno_Sp = true;
                Zeno_Sp();
                break;

        }

        isEnemy_Sp = false;
        SpAtkObj.SetActive(false);
    }

    public void Zeno_Sp()
    {
        StartCoroutine(Zeno_Sp_Routine());

        IEnumerator Zeno_Sp_Routine()
        {
            int count = 0;

            while (count < 5)
            {
                yield return new WaitForSeconds(0.2f);
                player.pc_Health -= 7;

                player_Idle.sprite = player_Sprite[2];

                count++;
            }

            isZeno_Sp = false;
            player_Idle.sprite = player_Sprite[0];
        }
    }

    public void GameStart()
    {
        start.SetActive(false);
        enemyTalk.SetActive(true);
        enemyText.text = enemyData.GetEnemyTalk(enemyData.get_Enemy_Id, 0);

        gameManager.StartSpawn();

        gameManager.spawnTrigger = true;

        StartCoroutine(EndTalk());
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(5.0f);

        enemyTalk.SetActive(false);

        enemyText.text = enemyData.GetEnemyTalk(enemyData.get_Enemy_Id, 1);
    }
}
