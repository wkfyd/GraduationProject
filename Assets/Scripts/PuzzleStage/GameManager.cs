using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public TutorialManager tuto;

    public GameObject BlockPrefab;
    public Block lastBlock;

    public GameObject effectPrefab;
    public Transform effectGroup;

    public GameObject winImg;

    public Animator overAim;

    public int turns;
    public int curt_turns;
    public int maxLevel;
    public bool gameOver;
    public bool gameWin;
    public bool isSpawn;
    public bool spawnTrigger;

    public GameObject[] currentBlock;
    public GameObject[,] blocks = new GameObject[9, 8]; //������Ʈ 2�����迭����,�ʱ�ȭ

    public int[] currentLevels;

    GameBoard gameBoard = new GameBoard();
    System.Random random = new System.Random();

    void Awake()
    {
        overAim = GameObject.FindWithTag("Enemy").GetComponent<Animator>();
    }

    void Start()
    {
        spawnTrigger = true;

        StartSpawn();
    }

    void Update()
    {
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //���� ���� �迭

        Debug.Log(curt_turns);

        //�� ĭ�� ���� ������ �����ֱ�
        BlockDown();

        //���� �� �� �ִ� �� �ִ��� ã��
        FindDuplication();

        //���� �� �� �ִ� ���� ���ٸ� ���� ����
        if (isSpawn)
            Spawn();

        //���ӽ¸�
        if (enemyManager.enemy_Health <= 0)
            GameWin();

        //�����й�
        if (enemyManager.enemy_Health <= 0)
            GameWin();
    }

    //���۽���
    void StartSpawn()
    {
        //����������
        GameObject[] destroyBlock = GameObject.FindGameObjectsWithTag("Block");

        for (int j = 0; j < destroyBlock.Length; j++)
            Destroy(destroyBlock[j]);

        for (int i = 6; i < 8; i++)
        {
            for (int j = 1; j < 7; j++)
            {
                blocks[i, j] = null;
            }
        }

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
            lastBlock.enemy = enemyManager;
            lastBlock.tuto = tuto;
            lastBlock.level = random.Next(0, maxLevel); //�������� ���� ����

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
                {
                    break;
                }
            }

            if (!hasDuplicates)
            {
                isSpawn = true;
                spawnTrigger = false;
            }
        }

    }

    public void Check()
    {
        for (int i = 0; i < currentBlock.Length; i++)
        {
            Debug.Log("����" + currentLevels[i]);
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
            {
                min = currentLevels[i];
            }
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

            //�����Ǵ� ��ϰ� ������ ������ ��Ͽ� ���� ������ ������ ���� ( + 3 ) �Ǵ� ( - 3 ) �̻��� ���̰� ���� �� �ȴ�
            //�����Ǵ� ����� ���� ������ ��� �� ���� �ռ����� ����� �������� �Ͽ� 3�� ��Ģ��� �����ȴ�
            if (min - 2 < 0)
            {
                lastBlock.level = random.Next(0, min + 3);
            }
            else
            {
                lastBlock.level = random.Next(min - 2, min + 3);
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
            lastBlock.enemy = enemyManager;
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
                    Block block = blocks[i, j].gameObject.GetComponent<Block>();

                    blocks[i, j] = null;
                    blocks[i - 1, j] = block.gameObject;

                    StartCoroutine(UpSpawnBlock(block, i - 1, j));

                    block.gridX -= 1;
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

    public void GameWin()
    {
        gameWin = true;
        overAim.SetBool("isWin", true);
        Invoke("InvokeWinImg", 5.0f);
    }

    void InvokeWinImg()
    {
        winImg.SetActive(true);
    }
}
