using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeGameManager : MonoBehaviour
{
    public GameObject camera;

    public GameObject BlockPrefab;
    public GameObject blockEng;
    public GameObject blockGrec;
    public FreeBlock lastBlock;
    public GameObject startPanel;
    public GameObject pause;

    public GameObject effectPrefab;
    public Transform effectGroup;

    public int score;
    public TextMeshProUGUI scoreText;

    public int turns;       //���� ��
    public int curt_turns;  //���� �� (���� �ϰ� ���� ���� ����ϱ� ����)
    public bool isOver;
    public bool isSpawn;
    public bool isSpawning;
    public bool spawnTrigger;


    public GameObject[] currentBlock;
    public GameObject[,] blocks = new GameObject[9, 8]; //������Ʈ 2�����迭����,�ʱ�ȭ

    public int[] currentLevels;

    Coroutine spawnRepeating;

    GameBoard gameBoard = new GameBoard();
    System.Random random = new System.Random();

    void Start()
    {
        startPanel.SetActive(true);

        if (SaveData.isLanguage == 0)
            BlockPrefab = blockEng;
        else
            BlockPrefab = blockGrec;

        scoreText.text = "0";

        spawnRepeating = StartCoroutine(SpawnRepeating());
    }

    void Update()
    {
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //���� ���� �迭

        //�� ĭ�� ���� ������ �����ֱ�
        BlockDown();

        //���� �� �� �ִ� �� �ִ��� ã��
        FindDuplication();

        //���� �� �� �ִ� ���� ���ٸ� ���� ����
        if (isSpawn)
            Spawn();

        //���� ����
        if (isOver)
            GameEnd();

        if (Input.GetKeyDown(KeyCode.Escape))
            pause.SetActive(true);
    }

    IEnumerator SpawnRepeating()
    {
        while (!isOver)
        {
            yield return new WaitForSeconds(5.0f);
            Spawn();
        }
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

                lastBlock = blocks[x + 1, y].GetComponent<FreeBlock>();

                lastBlock.gridY = y;
                lastBlock.gridX = x + 1;
            }
            else
            {
                blocks[x, y] = Instantiate(BlockPrefab,
                                        gameBoard.blockGridPos[x, y], Quaternion.identity);

                lastBlock = blocks[x, y].GetComponent<FreeBlock>();

                lastBlock.gridY = y;
                lastBlock.gridX = x;
            }

            lastBlock.manager = this;
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
            Debug.Log(1);
            currentLevels = new int[currentBlock.Length];   //�����迭

            //���� ���� �����迭�� ����
            for (int i = 0; i < currentBlock.Length; i++)
            {
                FreeBlock block = currentBlock[i].gameObject.GetComponent<FreeBlock>();

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
        isSpawning = true;

        if (spawnRepeating != null)
            StopCoroutine(spawnRepeating);

        spawnRepeating = StartCoroutine(SpawnRepeating());
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

            lastBlock = blocks[blocks.GetLength(0) - 1, i + 1].GetComponent<FreeBlock>();

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
                    FreeBlock block = blocks[i, j].gameObject.GetComponent<FreeBlock>();

                    blocks[i, j] = null;
                    blocks[i - 1, j] = block.gameObject;

                    StartCoroutine(UpSpawnBlock(block, i - 1, j));

                    block.gridX -= 1;

                    if (block.gridX == 1)
                        isOver = true;
                }
            }
        }

        spawnTrigger = true;
        isSpawning = false;
    }

    IEnumerator UpSpawnBlock(FreeBlock currentBlock, int x, int y)
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
            FreeBlock block = currentBlock[i].gameObject.GetComponent<FreeBlock>();

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

    IEnumerator DownRoutine(FreeBlock currentBlock, int x, int y)
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

    public void StartPanel()
    {
        StartSpawn();
        spawnTrigger = true;
        startPanel.SetActive(false);
    }

    public void GameEnd()
    {
        pause.SetActive(true);
    }
}
