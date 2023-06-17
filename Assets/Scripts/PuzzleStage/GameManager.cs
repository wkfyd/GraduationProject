using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public TutorialManager tuto;
    public Player player;

    public GameObject camera;
    public GameObject pause;

    public GameObject BlockPrefab;
    public GameObject blockEng;
    public GameObject blockGrec;
    public Block lastBlock;

    public GameObject effectPrefab;
    public Transform effectGroup;

    public GameObject winImg;
    public GameObject loseImg;

    public Animator winAim;
    public Animator playerOverAim;

    public int turns;       //���� ��
    public int curt_turns;  //���� �� (���� �ϰ� ���� ���� ����ϱ� ����)
    public int comboAtk;
    public bool gameOver;
    public bool isOver;
    public bool gameWin;
    public bool isSpawn;
    public bool spawnTrigger;

    public AudioClip mergeClip;
    public AudioClip hitClip;
    public AudioClip loseClip;
    public AudioClip deathClip;
    public GameObject bgm;
    public GameObject winClip;

    public GameObject[] currentBlock;
    public GameObject[,] blocks = new GameObject[9, 8]; //������Ʈ 2�����迭����,�ʱ�ȭ

    public int[] currentLevels;

    GameBoard gameBoard = new GameBoard();
    System.Random random = new System.Random();

    void Start()
    {
        winAim = GameObject.FindWithTag("Enemy").GetComponent<Animator>();

        gameWin = false;
        gameOver = false;
    }

    void Update()
    {
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //���� ���� �迭

        for (int i = 0; i < currentBlock.Length; i++)
        {
            if (currentBlock[i].GetComponent<Block>().level == 24)
                Destroy(currentBlock[i]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            pause.SetActive(true);

        //�� ĭ�� ���� ������ �����ֱ�
        BlockDown();

        //���� �� �� �ִ� �� �ִ��� ã��
        FindDuplication();

        //���� �� �� �ִ� ���� ���ٸ� ���� ����
        if (isSpawn)
            Spawn();

        //���ӽ¸�
        if (!gameWin && enemyManager.enemy_Health <= 0)
            GameWin();

        //�����й�
        if (!gameOver && player.pc_Health <= 0 || isOver)
            GameOver();
    }

    //���۽���
    public void StartSpawn()
    {
        if (SaveData.isLanguage == 0)
            BlockPrefab = blockEng;
        else
            BlockPrefab = blockGrec;

        //�� ����
        int x = 0, y = 0;
        int[] startLevels = new int[6];

        for (int i = 0; i < 6; i++)
        {
            GameObject instantEffectobj = Instantiate(effectPrefab, effectGroup);
            ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

            //��ǥ ����
            while (true) { x = random.Next(6, 8); y = random.Next(1, 7); if (blocks[x, y] == null) break; }

            //�� ���� �� ������ǥ�� �� ĭ�� ���� ������ �� ĭ�� ����
            if (x == 6 && blocks[x + 1, y] == null)
            {
                blocks[x + 1, y] = Instantiate(BlockPrefab,
                                        gameBoard.blockGridPos[x + 1, y], Quaternion.identity);

                lastBlock = blocks[x + 1, y].GetComponent<Block>();

                lastBlock.gridY = y;
                lastBlock.gridX = x + 1;
            }
            else
            {
                blocks[x, y] = Instantiate(BlockPrefab,
                                        gameBoard.blockGridPos[x, y], Quaternion.identity);

                lastBlock = blocks[x, y].GetComponent<Block>();

                lastBlock.gridY = y;
                lastBlock.gridX = x;
            }

            lastBlock.manager = this;
            lastBlock.enemyManager = enemyManager;
            lastBlock.tuto = tuto;

            //���� ���� ���ۺ� ���� �ٸ�
            //1����϶� F����
            if (SaveData.currentEnemy_Id == 1001 || SaveData.currentEnemy_Id == 1002 ||
                SaveData.currentEnemy_Id == 1003 || SaveData.currentEnemy_Id == 1004)
                lastBlock.level = random.Next(5, 8);

            //2����϶� D����
            else if (SaveData.currentEnemy_Id == 1005 || SaveData.currentEnemy_Id == 1006 ||
                SaveData.currentEnemy_Id == 1007 || SaveData.currentEnemy_Id == 1008)
                lastBlock.level = random.Next(3, 6);
            
            else
                lastBlock.level = random.Next(0, 3);

            startLevels[i] = lastBlock.level; //���� �迭

            //�ּ� �� �� ���� ����
            if (i == 1)
            {
                lastBlock.level = startLevels[0];
            }

            if (i == 3)
            {
                lastBlock.level = startLevels[2];
            }

            lastBlock.particle = instantEffect;
            lastBlock.gameObject.SetActive(true);
        }
    }

    void FindDuplication()
    {
        if (spawnTrigger)
        {
            bool hasDuplicates = false;

            currentLevels = new int[currentBlock.Length];   //�����迭

            //���� ���� �����迭�� ����
            for (int i = 0; i < currentBlock.Length; i++)
            {
                Block block = currentBlock[i].gameObject.GetComponent<Block>();

                currentLevels[i] = block.level;
            }

            //�ߺ��� ���� �ִ��� ������ Ȯ��
            for (int i = 0; i < currentLevels.Length; i++)
            {
                for (int j = i + 1; j < currentLevels.Length; j++)
                {
                    if (currentLevels[i] == currentLevels[j])
                    {
                        hasDuplicates = true;
                        break;
                    }
                }

                if (hasDuplicates)
                    break;
            }

            if (!hasDuplicates)
            {
                isSpawn = true;
                spawnTrigger = false;
            }
        }
    }

    public void Spawn()
    {
        isSpawn = false;

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        int[] spawnLevels = new int[6];

        int min = int.MaxValue;

        for (int i = 0; i < currentLevels.Length; i++)
        {
            if (min > currentLevels[i])
                min = currentLevels[i];
        }

        //�� �� �ٿ� 6�� ����
        for (int i = 0; i < 6; i++)
        {
            GameObject instantEffectobj = Instantiate(effectPrefab, effectGroup);
            ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

            blocks[blocks.GetLength(0) - 1, i + 1] =
                Instantiate(BlockPrefab, gameBoard.blockGridPos[blocks.GetLength(0) - 1, i + 1], Quaternion.identity);

            lastBlock = blocks[blocks.GetLength(0) - 1, i + 1].GetComponent<Block>();

            lastBlock.gridX = blocks.GetLength(0) - 1;
            lastBlock.gridY = i + 1;

            //�����Ǵ� ��Ͽ� ���� ������ ������ ������ ��� �� ���� ������ ���� ����� �������� (+3), (-0) �̻��� ���̰� ���� �� �ȴ�
            //�����Ǵ� ����� ���� ������ ��� �� ���� �ռ����� ����� �������� �Ͽ� 3�� ��Ģ��� �����ȴ�
            if (min - 2 < 0)
            {
                lastBlock.level = random.Next(0, min + 3);
            }
            else
            {
                lastBlock.level = random.Next(min, min + 3);
            }

            spawnLevels[i] = lastBlock.level;       //�����񱳼��� ���� �����迭

            //������ ������ ��ϰ� ������ ���ڰ� ���� ����� �ּ� 1�� �̻� ����
            if (i == 0)
            {
                int randomIndex = random.Next(0, currentLevels.Length);

                while (currentLevels[randomIndex] >= min + 3)
                {
                    randomIndex = random.Next(0, currentLevels.Length);
                }

                lastBlock.level = currentLevels[randomIndex];
            }

            //���� ���� ���ڰ� ���� ����� �ּ� 1�� �̻�
            if (i == 5)
            {
                int randomIndex = random.Next(0, spawnLevels.Length - 1);
                lastBlock.level = spawnLevels[randomIndex];
            }

            lastBlock.manager = this;
            lastBlock.enemyManager = enemyManager;
            lastBlock.tuto = tuto;
            lastBlock.particle = instantEffect;
            lastBlock.gameObject.SetActive(true);
        }

        //���� �� ���� �� �� ĭ�� �÷��ֱ�
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 6; j++)
            {
                if (blocks[i, j] != null)
                {
                    if (blocks[i, j].gameObject.GetComponent<Block>().select != false)
                    {

                    }
                    else
                    {
                        Block block = blocks[i, j].gameObject.GetComponent<Block>();

                        blocks[i, j] = null;
                        blocks[i - 1, j] = block.gameObject;

                        StartCoroutine(UpSpawnBlock(block, i - 1, j));

                        block.gridX -= 1;

                        if (block.gridX == 1)
                            isOver = true;
                    }
                }
            }
        }

        spawnTrigger = true;
    }

    IEnumerator UpSpawnBlock(Block currentBlock, int x, int y)
    {
        int frameCount = 0;

        while (frameCount < 60)
        {

            currentBlock.transform.position = Vector3.MoveTowards(currentBlock.transform.position,
                                                                    gameBoard.blockGridPos[x, y], 10f * Time.deltaTime);
            frameCount++;

            yield return null;
        }
    }

    void BlockDown()
    {
        for (int i = 0; i < currentBlock.Length; i++) //��� ��
        {
            Block block = currentBlock[i].gameObject.GetComponent<Block>();

            //�������� �ƴ� ���� �� �� �� ĭ�� null�̸�
            if (!block.select && block.gridX <= 6 && blocks[block.gridX + 1, block.gridY] == null)
            {
                StartCoroutine(DownRoutine(block, block.gridX + 1, block.gridY));

                blocks[block.gridX, block.gridY] = null;
                blocks[block.gridX + 1, block.gridY] = block.gameObject;

                block.gridX++;
            }
        }
    }

    IEnumerator DownRoutine(Block currentBlock, int x, int y)
    {
        int frameCount = 0;

        while (frameCount <= 35)
        {
            currentBlock.transform.position = Vector3.MoveTowards(currentBlock.transform.position,
                                                                    gameBoard.blockGridPos[x, y], 30f * Time.deltaTime);

            frameCount++;

            yield return null;
        }

    }

    public void TutorialText()
    {
        if (SaveData.isTutorial == 0)
        {
            tuto.player_text.SetActive(true);
            SaveData.isTutorial = 1;
        }
        else
            return;
    }

    public void TutoTextLast()
    {
        tuto.tuto_text[0].SetActive(false);
        tuto.tuto_text[1].SetActive(true);
        Invoke("falseText", 4f);
    }

    void falseText()
    {
        tuto.player_text.SetActive(false);
    }

    public void GameWin()
    {
        //óġ �Ǵ�
        switch (SaveData.currentEnemy_Id)
        {
            case 1001:
                SaveData.isSocra = 1;
                SaveData.newBook = 1;
                break;

            case 1002:
                SaveData.isPlato = 1;
                SaveData.newBook = 1;
                break;

            case 1003:
                SaveData.isAristo = 1;
                SaveData.newBook = 1;
                break;

            case 1004:
                SaveData.isPytha = 1;
                SaveData.newBook = 1;
                break;

            case 1005:
                SaveData.isArchi = 1;
                SaveData.newBook = 1;
                break;

            case 1006:
                SaveData.isThales = 1;
                SaveData.newBook = 1;
                break;

            case 1007:
                SaveData.isEpicuru = 1;
                SaveData.newBook = 1;
                break;

            case 1008:
                SaveData.isZeno = 1;
                SaveData.newBook = 1;
                break;

            case 1009:
                SaveData.isDiog = 1;
                SaveData.newBook = 1;
                break;

            case 1010:
                SaveData.isProta = 1;
                SaveData.newBook = 1;
                break;

            case 1011:
                SaveData.isThrasy = 1;
                SaveData.newBook = 1;
                break;

            case 1012:
                SaveData.isGorgi = 1;
                SaveData.newBook = 1;
                break;

            case 1013:
                SaveData.isHippa = 1;
                SaveData.newBook = 1;
                break;

            case 1014:
                SaveData.isEucli = 1;
                SaveData.newBook = 1;
                break;

            case 1015:
                SaveData.isStoicism = 1;
                SaveData.newBook = 1;
                break;

            case 1016:
                SaveData.isEpicuri = 1;
                SaveData.newBook = 1;
                break;

            case 1017:
                SaveData.isSophist = 1;
                SaveData.newBook = 1;
                break;
        }

        //ī�޶� ����
        camera.SetActive(true);

        StartCoroutine(DelayWinMotion());
        gameWin = true;
        Invoke("InvokeWinImg", 5.0f);

        //óġ �� ���� �� ������ ���� id �ʱ�ȭ
        SaveData.currentEnemy_Id = 0;

        //�� óġ �� ���̺�
        SaveData.GameSave();
    }

    IEnumerator DelayWinMotion()
    {
        yield return new WaitForSeconds(1f);

        Time.timeScale = 0.2f;
        winAim.SetBool("isWin", true);

        yield return new WaitForSeconds(0.1f);

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        camera.SetActive(true);
        isOver = false;
        gameOver = true;
        PlaySound(deathClip, 0.5f);

        StartCoroutine(Player_DelayWinMotion());
        SaveData.isGameOver = 1;

        Invoke("InvokeLoseImg", 5.0f);
    }

    IEnumerator Player_DelayWinMotion()
    {
        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;
        playerOverAim.SetBool("isLose", true);

        yield return new WaitForSeconds(0.1f);

        Time.timeScale = 1f;
    }

    void InvokeWinImg()
    {
        gameWin = false;
        bgm.SetActive(false);
        winClip.SetActive(true);
        camera.SetActive(false);
        winImg.SetActive(true);
    }

    void InvokeLoseImg()
    {
        gameOver = false;
        bgm.SetActive(false);
        PlaySound(loseClip, 0.5f);
        camera.SetActive(false);
        loseImg.SetActive(true);
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
