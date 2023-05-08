using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;

    public GameObject[] currentBlock;
    public GameObject[,] blocks = new GameObject[9, 8]; //오브젝트 2차원배열선언,초기화

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
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //현재 블럭들 배열

        //밑 칸에 블럭이 없으면 내려주기
        BlockDown();

        //블럭 리스폰
        Spawn();
    }

    //시작스폰
    public void StartSpawn()
    {
        //기존블럭삭제
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

        //블럭 스폰
        int x = 0, y = 0;
        int[] levels = new int[6];

        for (int i = 0; i < 6; i++)
        {
            //좌표 지정
            while (true) { x = random.Next(6, 8); y = random.Next(1, 7); if (blocks[x, y] == null) break; }

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
            lastBlock.level = random.Next(0, maxLevel); //일정범위 랜덤 레벨

            levels[i] = lastBlock.level; //레벨 배열

            //최소 두 쌍 같은 레벨
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

        //현재 블럭들 레벨배열에 참조
        for (int i = 0; i < currentBlock.Length; i++)   
        {
            Block block = currentBlock[i].gameObject.GetComponent<Block>();

            levels[i] = block.level;
        }

        //중복된 값이 있는지 없는지 확인
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

        Debug.Log(hasDuplicates);

        if (!hasDuplicates) {
            isSpawn = true;
        }

        //중복 없다면 스폰실행
        if(isSpawn){
            Debug.Log("스폰 할거야");

            for (int i = 1; i <= 6; i++)
            {
                blocks[blocks.GetLength(0) - 1, i] = Instantiate(BlockPrefab, gameBoard.blockGridPos[blocks.GetLength(0) - 1, i], Quaternion.identity);
                Debug.Log("스폰 해쓰");

                lastBlock = blocks[blocks.GetLength(0) - 1, i].GetComponent<Block>();

                lastBlock.gridX = blocks.GetLength(0) - 1;
                lastBlock.gridY = i;

                lastBlock.manager = this;
                lastBlock.level = random.Next(0, maxLevel);

                lastBlock.gameObject.SetActive(true);
            }

            isSpawn = false;
        }
    }

    void BlockDown()
    {
        for (int i = 0; i < currentBlock.Length; i++) //모든 블럭
        {
            Block block = currentBlock[i].gameObject.GetComponent<Block>();

            //선택중이 아닌 블럭과 그 블럭 밑 칸이 null이면
            if (!block.select && block.gridX <= 6 && blocks[block.gridX + 1, block.gridY] == null)
            {
                StartCoroutine(DownRoutine(block, block.gridX + 1, block.gridY));

                blocks[block.gridX, block.gridY] = null;
                blocks[block.gridX + 1, block.gridY] = block.gameObject;

                block.gridX++;
            }
        }
    }

    IEnumerator DownRoutine(Block myPos, int x, int y)
    {
        int frameCount = 0;

        while (frameCount <= 60)
        {
            myPos.transform.position = Vector3.MoveTowards(myPos.transform.position, gameBoard.blockGridPos[x, y], 0.02f);

            frameCount++;

            yield return Time.deltaTime;
        }

    }
}
