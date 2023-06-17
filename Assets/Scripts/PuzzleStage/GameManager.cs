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

    public int turns;       //이전 턴
    public int curt_turns;  //현재 턴 (이전 턴과 현재 턴을 계산하기 위함)
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
    public GameObject[,] blocks = new GameObject[9, 8]; //오브젝트 2차원배열선언,초기화

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
        currentBlock = GameObject.FindGameObjectsWithTag("Block"); //현재 블럭들 배열

        for (int i = 0; i < currentBlock.Length; i++)
        {
            if (currentBlock[i].GetComponent<Block>().level == 24)
                Destroy(currentBlock[i]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            pause.SetActive(true);

        //밑 칸에 블럭이 없으면 내려주기
        BlockDown();

        //머지 할 수 있는 블럭 있는지 찾기
        FindDuplication();

        //머지 할 수 있는 블럭이 없다면 스폰 실행
        if (isSpawn)
            Spawn();

        //게임승리
        if (!gameWin && enemyManager.enemy_Health <= 0)
            GameWin();

        //게임패배
        if (!gameOver && player.pc_Health <= 0 || isOver)
            GameOver();
    }

    //시작스폰
    public void StartSpawn()
    {
        if (SaveData.isLanguage == 0)
            BlockPrefab = blockEng;
        else
            BlockPrefab = blockGrec;

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
            lastBlock.enemyManager = enemyManager;
            lastBlock.tuto = tuto;

            //적에 따라 시작블럭 레벨 다름
            //1등급일때 F부터
            if (SaveData.currentEnemy_Id == 1001 || SaveData.currentEnemy_Id == 1002 ||
                SaveData.currentEnemy_Id == 1003 || SaveData.currentEnemy_Id == 1004)
                lastBlock.level = random.Next(5, 8);

            //2등급일때 D부터
            else if (SaveData.currentEnemy_Id == 1005 || SaveData.currentEnemy_Id == 1006 ||
                SaveData.currentEnemy_Id == 1007 || SaveData.currentEnemy_Id == 1008)
                lastBlock.level = random.Next(3, 6);
            
            else
                lastBlock.level = random.Next(0, 3);

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

            //생성되는 블록에 적힌 문자의 순번은 생성된 블록 중 가장 순번이 낮은 블록을 기준으로 (+3), (-0) 이상의 차이가 나선 안 된다
            //생성되는 블록은 기존 생성된 블록 중 가장 앞순번의 블록을 기준으로 하여 3번 규칙대로 생성된다
            if (min - 2 < 0)
            {
                lastBlock.level = random.Next(0, min + 3);
            }
            else
            {
                lastBlock.level = random.Next(min, min + 3);
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
            lastBlock.enemyManager = enemyManager;
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
        //처치 판단
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

        //카메라 흔들기
        camera.SetActive(true);

        StartCoroutine(DelayWinMotion());
        gameWin = true;
        Invoke("InvokeWinImg", 5.0f);

        //처치 시 다음 적 생성을 위해 id 초기화
        SaveData.currentEnemy_Id = 0;

        //적 처치 시 세이브
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

        // 사운드 재생이 끝나면 게임 오브젝트 파괴
        Destroy(soundObject, soundClip.length);
    }
}
