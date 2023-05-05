using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;

    public GameObject[,] blocks = new GameObject[9, 6]; //오브젝트 2차원배열선언,초기화

    public int maxLevel;
    public bool gameOver;

    private int i = 0;

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
        //블럭 밑 칸 비어있으면 계속 내려주기
        if (!lastBlock.select)
        {
            for (int i = 2; i < blocks.GetLength(0) - 2; i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j] != null && blocks[i + 1, j] == null)
                    {
                        Block block = blocks[i, j].gameObject.GetComponent<Block>();

                        blocks[i, j] = null;
                        blocks[i + 1, j] = block.gameObject;

                        block.gridX = i + 1;
                        block.gridY = j;

                        block.transform.position = Vector3.MoveTowards(block.transform.position,
                                                                        gameBoard.blockGridPos[i+1, j], 20f * Time.deltaTime);

                        Debug.Log("[" + block.gridX + ", " + block.gridY + "]");
                    }
                }
            }
        }

    }

    /*gameBoard.blockGridPos[i + 1, j].x - lastBlock.bounds.size.x / 2 &&
                    gameBoard.blockGridPos[i + 1, j].x + lastBlock.bounds.size.x / 2 &&
                    gameBoard.blockGridPos[i + 1, j].y + lastBlock.bounds.size.y / 2 &&
                    gameBoard.blockGridPos[i + 1, j].y - lastBlock.bounds.size.y / 2 */

    //시작스폰
    public void StartSpawn()
    {
        //기존블럭삭제
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

        //블럭 스폰
        int x = 0, y = 0;
        int[] levels = new int[10];

        for (int i = 0; i < 6; i++)
        {
            //좌표 지정
            while (true) { x = Random.Range(6, 8); y = Random.Range(0, 6); if (blocks[x, y] == null) break; }

            //블럭 생성 및 생성좌표의 밑 칸에 블럭이 없으면 밑 칸에 생성
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
            lastBlock.level = Random.Range(0, maxLevel); //일정범위 랜덤 레벨

            levels[i] = lastBlock.level; //레벨 배열

            //최소 두 쌍 같은 레벨
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
