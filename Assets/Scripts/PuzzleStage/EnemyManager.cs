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

    public GameObject atkedEffect;
    public Transform pcEffectGroup;
    public Transform npcEffectGroup;

    public TextMeshProUGUI NextSp_Text;
    public GameObject SpAtkObj;
    public Image SpAtkImg;
    public Animator spAnim;
    public TextMeshProUGUI spNameText;
    public TextMeshProUGUI spText;
    public TextMeshProUGUI NextAtk_Text;
    public GameObject damagePrefabs;        //����� �ؽ�Ʈ ������
    public Transform enemyDamageTextGroup;  //����� �ؽ�Ʈ ��ġ
    public Transform playerDamageTextGroup; //����� �ؽ�Ʈ ��ġ
    public TextMeshProUGUI comboText;       //�޺� �ؽ�Ʈ ������

    public int enemy_MaxHealth;     //�� �ִ� ü��
    public int enemy_Health;        //�� ���� ü��
    [HideInInspector]
    public int enemy_DamageHP;      //���� �������� �ް� �� �� ü�� (�ǰ� ��/�� �Ǵ� �� ���)
    public int enemy_AtkTurn;       //�� �Ϲ� ���� �� ��
    public int enemy_NomAtk;        //�� �Ϲ� ���� ���� ��
    public int enemy_Atk_Damage;    //�� �Ϲ� ���� ������
    public int enemy_SpTurn;        //�� ����� ���� �� ��
    public int enemy_NomSp;         //�� ����� ���� ���� ��

    public bool isEnemy_Sp;         //���� ����� ���� ������ Ȯ��
    public bool isEnemy_Atk;        //���� �Ϲ� ���� ������ Ȯ��
    public bool isPlayer_Atk;       //�÷��̾ ���� ������ Ȯ��

    public bool isAristo_Sp;        //�Ƹ������ڷ��� ����� ȿ�� ����
    public int aristo_Sp_NomTurn;   //�Ƹ������ڷ��� ����� ���� ��
    public bool isArchi_Sp;         //�Ƹ�Ű�޵��� ����� ȿ�� ����
    public int archi_Sp_NomTurn;    //�Ƹ�Ű�޵��� ����� ���� ��
    public bool isThales_Sp;        //Ż���� ����� ȿ�� ����
    public int thales_Sp_NomTurn;   //Ż���� ����� ���� ��
    public bool isEpicuru_Sp;       //������ν� ����� ȿ�� ����
    public int epicuru_Sp_NomTurn;  //������ν� ����� ���� ��
    public bool isZeno_Sp;          //���� ����� ȿ�� ����

    public AudioClip sp1;
    public AudioClip sp2;

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

        enemyNameText.text = enemyData.Get_enemyName();
        spNameText.text = enemyData.Get_SpNameText();
        spText.text = enemyData.Get_SpText();

        SpAtkImg.sprite = enemyData.SetEnemySp();

        //���� �� str�������� ��ȯ
        NextSp_Text.text = enemy_NomSp.ToString();
        NextAtk_Text.text = enemy_NomAtk.ToString();

        enemy_Sprite = enemyData.GetSprite(enemyData.get_Enemy_Id);
    }

    void Update()
    {
        enemy_HealthBar.fillAmount = (float)enemy_Health / enemy_MaxHealth;

        //�÷��̾� ����, �� �ǰ� (���� ü���� ������ ���� -> �Ƹ������ڷ��� ó�� ������� ���� ���� �� ��)
        if (enemy_Health > enemy_DamageHP)
        {
            PlayerAtk();

            if (enemy_NomSp != 1)
                Enemy_atked();

            enemy_Health = enemy_DamageHP;
        }

        //�÷��̾� �ǰ�, �� ����
        if (gameManager.turns < gameManager.curt_turns)
        {
            Enemy_Atk();

            //����� ������ 0�� �ƴҽ� = ���� 1, 2����Ͻ�
            if (enemy_SpTurn != 0)
                Enemy_Sp();

            //�Ƹ�Ű�޵��� ����� ����ȿ��
            if (SaveData.currentEnemy_Id == 1005)
            {
                if (isArchi_Sp && (archi_Sp_NomTurn >= 1 && archi_Sp_NomTurn <= 7))
                {
                    //����� �ؽ�Ʈ ���� �� ���
                    damagePrefabs.GetComponent<TextMeshProUGUI>().text = (enemy_Atk_Damage).ToString();
                    Instantiate(damagePrefabs, playerDamageTextGroup);

                    //�ǰ� ����Ʈ ���
                    GameObject instantEffectobj = Instantiate(atkedEffect, pcEffectGroup);
                    ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

                    instantEffect.Play();

                    player.pc_curntHealth -= (enemy_Atk_Damage);
                    player.pc_Health = player.pc_curntHealth;
                    archi_Sp_NomTurn--;

                    if (archi_Sp_NomTurn == 0)
                        isArchi_Sp = false;
                }
            }

            gameManager.turns = gameManager.curt_turns;
        }

        //���� �¸��� �÷��̾� ����, �� �ǰ�
        if (gameManager.gameWin)
        {
            enemy_Idle.sprite = enemy_Sprite[2];
            player_Idle.sprite = player_Sprite[1];
        }

    }

    //�÷��̾� ����
    public void PlayerAtk()
    {
        isPlayer_Atk = true;

        if (playerAtk != null)             //�ڷ�ƾ����
            StopCoroutine(playerAtk);

        if (playerAtked != null)             //�ڷ�ƾ����
            StopCoroutine(playerAtked);

        playerAtk = StartCoroutine(Player_Atk_Routine());
    }


    //�Ϲݰ���
    void Enemy_Atk()
    {
        enemy_NomAtk--;

        if (enemy_NomAtk == 0)
        {
            isEnemy_Atk = true;

            //Ż���� ����� ȿ��
            if (SaveData.currentEnemy_Id == 1006 && isThales_Sp
                && (thales_Sp_NomTurn >= 1 && thales_Sp_NomTurn <= 2))
            {
                player.pc_curntHealth -= enemy_Atk_Damage * 4;

                //����� �ؽ�Ʈ ���� �� ���
                damagePrefabs.GetComponent<TextMeshProUGUI>().text = (player.pc_Health - player.pc_curntHealth).ToString();
                Instantiate(damagePrefabs, playerDamageTextGroup);

                player.pc_Health = player.pc_curntHealth;

                thales_Sp_NomTurn--;

                if (thales_Sp_NomTurn == 0)
                    isThales_Sp = false;
            }

            else
            {
                player.pc_curntHealth -= enemy_Atk_Damage;

                //����� �ؽ�Ʈ ���� �� ���
                damagePrefabs.GetComponent<TextMeshProUGUI>().text = enemy_Atk_Damage.ToString();
                Instantiate(damagePrefabs, playerDamageTextGroup);

                player.pc_Health = player.pc_curntHealth;
            }

            if (enemyAtk != null)             //�ڷ�ƾ����
                StopCoroutine(enemyAtk);

            enemyAtk = StartCoroutine(Change_Enemy_Atk());

            if (playerAtked != null)          //�ڷ�ƾ����
                StopCoroutine(playerAtked);
            playerAtked = StartCoroutine(Player_Atked_Routine());

            enemy_NomAtk = enemy_AtkTurn;
        }

        NextAtk_Text.text = enemy_NomAtk.ToString();
    }

    //�� �ǰ�
    void Enemy_atked()
    {
        //����� �ؽ�Ʈ ���� �� ���
        damagePrefabs.GetComponent<TextMeshProUGUI>().text = (enemy_Health - enemy_DamageHP).ToString();
        Instantiate(damagePrefabs, enemyDamageTextGroup);

        //�ǰ� ����Ʈ ���
        GameObject instantEffectobj = Instantiate(atkedEffect, npcEffectGroup);
        ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

        instantEffect.Play();

        if (enemyAtked != null)             //�ڷ�ƾ����
            StopCoroutine(enemyAtked);

        if (enemyAtk != null)             //�ڷ�ƾ����
            StopCoroutine(enemyAtk);

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

        isPlayer_Atk = false;
    }

    IEnumerator Player_Atked_Routine()
    {
        //�ǰ� ����Ʈ ���
        GameObject instantEffectobj = Instantiate(atkedEffect, pcEffectGroup);
        ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

        instantEffect.Play();

        player_Idle.sprite = player_Sprite[2];

        yield return new WaitForSeconds(1f);

        if (gameManager.gameWin)
        {
            player_Idle.sprite = player_Sprite[1];
        }
        else if (gameManager.gameOver)
        {
            player_Idle.sprite = player_Sprite[2];
        }
        else
            player_Idle.sprite = player_Sprite[0];
    }


    //����Ȱ���
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
        isPlayer_Atk = false;

        StartCoroutine(SpCoroutine());
    }

    IEnumerator SpCoroutine()
    {
        SpAtkObj.SetActive(true);

        PlaySound(sp1, 0.3f);
        PlaySound(sp2, 0.3f);

        spAnim.SetBool("isSpAtk", true);

        enemy_Idle.sprite = enemy_Sprite[1];
        player_Idle.sprite = player_Sprite[0];

        yield return new WaitForSeconds(4.0f);

        spAnim.SetBool("isSpAtk", false);

        //SpAtk ȿ��
        switch (SaveData.currentEnemy_Id)
        {
            case 1001:
                //����� �ؽ�Ʈ ���� �� ���
                damagePrefabs.GetComponent<TextMeshProUGUI>().text = (player.pc_curntHealth / 2).ToString();
                Instantiate(damagePrefabs, playerDamageTextGroup);

                StartCoroutine(Player_Atked_Routine());

                player.pc_curntHealth /= 2;
                player.pc_Health = player.pc_curntHealth;
                break;

            case 1002:
                int rand = random.Next(90, 121);

                //����� �ؽ�Ʈ ���� �� ���
                damagePrefabs.GetComponent<TextMeshProUGUI>().text = rand.ToString();
                Instantiate(damagePrefabs, playerDamageTextGroup);

                StartCoroutine(Player_Atked_Routine());

                player.pc_curntHealth -= random.Next(90, 121);
                player.pc_Health = player.pc_curntHealth;
                break;

            case 1003:
                isAristo_Sp = true;
                aristo_Sp_NomTurn = 3;
                break;

            case 1004:
                //����� �ؽ�Ʈ ���� �� ���
                damagePrefabs.GetComponent<TextMeshProUGUI>().text = 100.ToString();
                Instantiate(damagePrefabs, playerDamageTextGroup);

                StartCoroutine(Player_Atked_Routine());

                player.pc_curntHealth -= 100;
                player.pc_Health = player.pc_curntHealth;
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

        if (SaveData.currentEnemy_Id == 1002)
            yield return new WaitForSeconds(1f);

        enemy_Idle.sprite = enemy_Sprite[0];
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
                player.pc_curntHealth -= 7;

                //�ǰ� ����Ʈ ���
                GameObject instantEffectobj = Instantiate(atkedEffect, pcEffectGroup);
                ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

                instantEffect.Play();

                //����� �ؽ�Ʈ ���� �� ���
                damagePrefabs.GetComponent<TextMeshProUGUI>().text = 7.ToString();
                Instantiate(damagePrefabs, playerDamageTextGroup);

                player.pc_Health = player.pc_curntHealth;

                player_Idle.sprite = player_Sprite[2];

                count++;
            }

            yield return new WaitForSeconds(0.3f);
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

    public void PlaySound(AudioClip soundClip, float volume)
    {
        Debug.Log("Sound played: " + soundClip);


        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = soundClip;
        audioSource.PlayOneShot(soundClip);

        // ���� ����� ������ ���� ������Ʈ �ı�
        Destroy(soundObject, soundClip.length);
    }
}
