using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;

    public GameObject[] currentBlock;
    public GameObject[,] blocks = new GameObject[9, 6]; //������Ʈ 2�����迭����,�ʱ�ȭ

    public int maxLevel;
    public bool gameOver;

    GameBoard gameBoard = new GameBoard();

    void Awake()
    {

    }
    void Start()
    {
        StartSpawn();
    }

    void Update()
    {
        currentBlock = GameObject.FindGameObjectsWithTag("Block");

        for(int i = 0; i < currentBlock.Length; i++)
        {
            Block block = currentBlock[i].gameObject.GetComponent<Block>();

            if (block.gridX <= 6 && blocks[block.gridX + 1, block.gridY] == null)
            {
                block.transform.position = Vector3.MoveTowards(block.transform.position,
                                                gameBoard.blockGridPos[block.gridX + 1, block.gridY], 0.2f);

                Debug.Log("������");
            }

            Debug.Log(block.level);
        }

        /*//�� �� ĭ ��������� ��� �����ֱ�
        if (!lastblock.select)
        {
            for (int i = 2; i < blocks.getlength(0) - 2; i++)
            {
                for (int j = 0; j < blocks.getlength(1); j++)
                {
                    if (blocks[i, j] != null && blocks[i + 1, j] == null)
                    {
                        block block = blocks[i, j].gameobject.getcomponent<block>();

                        blocks[i, j] = null;
                        blocks[i + 1, j] = block.gameobject;

                        block.gridx = i + 1;
                        block.gridy = j;

                        block.transform.position = vector3.movetowards(block.transform.position,
                                                                        gameboard.blockgridpos[i + 1, j], 20f * time.deltatime);

                        debug.log("[" + block.gridx + ", " + block.gridy + "]");
                    }
                }
            }
        }*/

    }

    /*gameBoard.blockGridPos[i + 1, j].x - lastBlock.bounds.size.x / 2 &&
                    gameBoard.blockGridPos[i + 1, j].x + lastBlock.bounds.size.x / 2 &&
                    gameBoard.blockGridPos[i + 1, j].y + lastBlock.bounds.size.y / 2 &&
                    gameBoard.blockGridPos[i + 1, j].y - lastBlock.bounds.size.y / 2 */

    //���۽���
    public void StartSpawn()
    {
        //����������
        GameObject[] destroyBlock = GameObject.FindGameObjectsWithTag("Block");

        for (int j = 0; j < destroyBlock.Length; j++)
            Destroy(destroyBlock[j]);

        for (int i = 6; i < 8; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                blocks[i, j] = null;
            }
        }

        //�� ����
        int x = 0, y = 0;
        int[] levels = new int[10];

        for (int i = 0; i < 6; i++)
        {
            //��ǥ ����
            while (true) { x = Random.Range(6, 8); y = Random.Range(0, 6); if (blocks[x, y] == null) break; }

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
            lastBlock.level = Random.Range(0, maxLevel); //�������� ���� ����

            levels[i] = lastBlock.level; //���� �迭

            //�ּ� �� �� ���� ����
            if (i == 1)
            {
                while (levels[0] != levels[1])
                {
                    levels[1] = Random.Range(0, maxLevel);
                }
                lastBlock.level = levels[1];
            }

            if (i == 3)
            {
                while (levels[2] != levels[3])
                {
                    levels[3] = Random.Range(0, maxLevel);
                }
                lastBlock.level = levels[3];
            }

            lastBlock.gameObject.SetActive(true);
        }
    }

    public void Spawn()
    {

    }
}
