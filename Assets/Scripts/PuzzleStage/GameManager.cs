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
    public GameObject[,] blocks = new GameObject[9, 8]; //오브젝트 2차원배열선언,초기화

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
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //현재 블럭들 배열

        Debug.Log(curt_turns);

        //밑 칸에 블럭이 없으면 내려주기
        BlockDown();

        //머지 할 수 있는 블럭 있는지 찾기
        FindDuplication();

        //머지 할 수 있는 블럭이 없다면 스폰 실행
        if (isSpawn)
            Spawn();

        //게임승리
        if (enemyManager.enemy_Health <= 0)
            GameWin();

        //게임패배
        if (enemyManager.enemy_Health <= 0)
            GameWin();
    }

    //시작스폰
    void StartSpawn()
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
        int[] startLevels = new int[6];

        for (int i = 0; i < 6; i++)
        {
            GameObject instantEffectobj = Instantiate(effectPrefab, effectGroup);
            ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

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
            lastBlock.enemy = enemyManager;
            lastBlock.tuto = tuto;
            lastBlock.level = random.Next(0, maxLevel); //일정범위 랜덤 레벨

            startLevels[i] = lastBlock.level; //레벨 배열

            //최소 두 쌍 같은 레벨
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

            currentLevels = new int[currentBlock.Length];   //레벨배열

            //현재 블럭들 레벨배열에 참조
            for (int i = 0; i < currentBlock.Length; i++)
            {
                Block block = currentBlock[i].gameObject.GetComponent<Block>();

                currentLevels[i] = block.level;
            }

            //중복된 값이 있는지 없는지 확인
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
            Debug.Log("레벨" + currentLevels[i]);
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

        //맨 밑 줄에 6개 생성
        for (int i = 0; i < 6; i++)
        {
            GameObject instantEffectobj = Instantiate(effectPrefab, effectGroup);
            ParticleSystem instantEffect = instantEffectobj.GetComponent<ParticleSystem>();

            blocks[blocks.GetLength(0) - 1, i + 1] =
                Instantiate(BlockPrefab, gameBoard.blockGridPos[blocks.GetLength(0) - 1, i + 1], Quaternion.identity);

            lastBlock = blocks[blocks.GetLength(0) - 1, i + 1].GetComponent<Block>();

            lastBlock.gridX = blocks.GetLength(0) - 1;
            lastBlock.gridY = i + 1;

            //생성되는 블록과 기존에 생성된 블록에 적힌 문자의 순번은 서로 ( + 3 ) 또는 ( - 3 ) 이상의 차이가 나선 안 된다
            //생성되는 블록은 기존 생성된 블록 중 가장 앞순번의 블록을 기준으로 하여 3번 규칙대로 생성된다
            if (min - 2 < 0)
            {
                lastBlock.level = random.Next(0, min + 3);
            }
            else
            {
                lastBlock.level = random.Next(min - 2, min + 3);
            }

            spawnLevels[i] = lastBlock.level;       //레벨비교설정 위한 레벨배열

            //기존에 생성된 블록과 동일한 문자가 적힌 블록이 최소 1개 이상 생성
            if (i == 0)
            {
                int randomIndex = random.Next(0, currentLevels.Length);

                while (currentLevels[randomIndex] >= min + 3)
                {
                    randomIndex = random.Next(0, currentLevels.Length);
                }

                lastBlock.level = currentLevels[randomIndex];
            }

            //서로 같은 문자가 적힌 블록이 최소 1쌍 이상
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

        //스폰 한 다음 블럭 한 칸씩 올려주기
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
