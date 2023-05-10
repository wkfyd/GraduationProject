using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;

    public GameObject[] currentBlock;
    public GameObject[,] blocks = new GameObject[9, 8]; //������Ʈ 2�����迭����,�ʱ�ȭ

    public int maxLevel;
    public bool gameOver;
    public bool isSpawn;

    GameBoard gameBoard = new GameBoard();
    System.Random random = new System.Random();

    void Awake()
    {

    }
    void Start()
    {
        StartSpawn();
    }

    void Update()
    {
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //���� ���� �迭

        //�� ĭ�� ���� ������ �����ֱ�
        BlockDown();

        //�� ������
        Spawn();

        Debug.Log(blocks[7, 6]);
    }
    public void Check()
    {
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 6; j++)
            {
                if (blocks[i, j] == null)
                    Debug.Log(i + ", " + j + "�� X");
                if (blocks[i, j] != null)
                    Debug.Log(i + ", " + j + "�� O");
            }
        }
    }
    //���۽���
    public void StartSpawn()
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
        int[] levels = new int[6];

        for (int i = 0; i < 6; i++)
        {
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
            lastBlock.level = random.Next(0, maxLevel); //�������� ���� ����

            levels[i] = lastBlock.level; //���� �迭

            //�ּ� �� �� ���� ����
            if (i == 1)
            {
                while (levels[0] != levels[1])
                {
                    levels[1] = random.Next(0, maxLevel);
                }
                lastBlock.level = levels[1];
            }

            if (i == 3)
            {
                while (levels[2] != levels[3])
                {
                    levels[3] = random.Next(0, maxLevel);
                }
                lastBlock.level = levels[3];
            }

            lastBlock.gameObject.SetActive(true);
        }
    }

    public void Spawn()
    {
        bool hasDuplicates = false;

        int[] levels = new int[currentBlock.Length];

        //���� ���� �����迭�� ����
        for (int i = 0; i < currentBlock.Length; i++)
        {
            Block block = currentBlock[i].gameObject.GetComponent<Block>();

            levels[i] = block.level;
        }

        //�ߺ��� ���� �ִ��� ������ Ȯ��
        for (int i = 0; i < levels.Length; i++)
        {
            for (int j = i + 1; j < levels.Length; j++)
            {
                if (levels[i] == levels[j])
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
        }

        //�ߺ� ���ٸ� ��������
        if (isSpawn)
        {
            //�� �� �ٿ� 6�� ����
            for (int i = 1; i <= 6; i++)
            {
                blocks[blocks.GetLength(0) - 1, i] = Instantiate(BlockPrefab, gameBoard.blockGridPos[blocks.GetLength(0) - 1, i], Quaternion.identity);

                lastBlock = blocks[blocks.GetLength(0) - 1, i].GetComponent<Block>();

                lastBlock.gridX = blocks.GetLength(0) - 1;
                lastBlock.gridY = i;

                lastBlock.manager = this;
                lastBlock.level = random.Next(0, maxLevel);

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

                        blocks[block.gridX, block.gridY] = null;
                        blocks[block.gridX - 1, block.gridY] = block.gameObject;

                        StartCoroutine(UpSpawnBlock(block, block.gridX - 1, block.gridY));

                        block.gridX -= 1;
                    }
                }
            }

            isSpawn = false;
            Debug.Log("���� �Ұž�");
        }
        
    }

    IEnumerator UpSpawnBlock(Block currentBlock, int x, int y)
    {
        int frameCount = 0;

        while (frameCount <= 60)
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
}
