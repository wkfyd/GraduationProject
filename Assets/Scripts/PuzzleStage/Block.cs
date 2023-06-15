using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public TutorialManager tuto;
    public EnemyManager enemyManager;

    public GameBoard gameBoard;

    public ParticleSystem particle;

    public int level, gridX, gridY;
    public int downGrid_X, downGrid_Y;
    public int blockDamage;
    public bool isParticle;
    public bool select, isMerge, levelUpOnce; //레벨업 한번만 시켜주기위함

    public Bounds bounds;  //Bounds 잘은 모르겠는데 콜라이더의 크기를 position기준으로 가져옴

    Animator anim;
    BoxCollider2D box;

    Vector3 mouse_Pos;

    void Awake()
    {
        anim = GetComponent<Animator>();

        box = GetComponent<BoxCollider2D>();
        bounds = box.bounds;
    }
    void Start()
    {
        gameBoard = new GameBoard();
    }

    void OnEnable() //스크립트가 활성화 될 때 실행되는 이벤트함수
    {
        anim.SetInteger("Level", level); //애니메이터 int형 파라미터
    }

    public void OnMouseDown()
    {
        if (!manager.gameWin)
        {
            if (!enemyManager.isEnemy_Sp)
            {
                if (!enemyManager.isZeno_Sp)
                {
                    downGrid_X = gridX;
                    downGrid_Y = gridY;

                    levelUpOnce = true;
                    select = true;
                }
            }
        }
    }

    public void OnMouseDrag()
    {
        if (!manager.gameWin)
        {
            if (!enemyManager.isEnemy_Sp)
            {
                if (!enemyManager.isZeno_Sp)
                {
                    mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
                    mouse_Pos.z = 0;

                    transform.position = Vector3.Lerp(transform.position, mouse_Pos, 0.2f);

                    //블럭이 있으면 부딪히게
                    BumpBlock();

                    //보드판 밖으로 못 나가게
                    outOfRangeBoard();

                    //좌표변경 함수 + 드래그머지
                    DragChangedGridPos();
                }
            }
        }
    }

    public void OnMouseUp()
    {
        if (!manager.gameWin)
        {
            if (!enemyManager.isEnemy_Sp)
            {
                if (!enemyManager.isZeno_Sp)
                {
                    //스냅기능
                    Snap();

                    //2차원 배열 좌표변경 후 위치변경 + 머지
                    if ((downGrid_X != gridX || downGrid_Y != gridY) || manager.blocks[gridX + 1, gridY] != null &&
                        manager.blocks[gridX + 1, gridY].gameObject.GetComponent<Block>().level == level)
                    {
                        DropChangedGridPos();
                    }

                    select = false;
                    levelUpOnce = false;
                }
            }
        }
    }

    //블럭 대미지
    int GetDamage()
    {
        switch (level)
        {
            //A
            case 0:
                blockDamage = 4;
                break;

            //B
            case 1:
                blockDamage = 8;
                break;

            //C
            case 2:
                blockDamage = 16;
                break;

            //D
            case 3:
                blockDamage = 32;
                break;

            //E
            case 4:
                blockDamage = 64;
                break;

            //F
            case 5:
                blockDamage = 120;
                break;

            //G
            case 6:
                blockDamage = 240;
                break;

            //H
            case 7:
                blockDamage = 520;
                break;

            //I
            case 8:
                blockDamage = 2080;
                break;

            //J
            case 9:
                blockDamage = 5000;
                break;

            //K
            case 10:
                blockDamage = 15000;
                break;

            //L
            case 11:
                blockDamage = 20000;
                break;

            //M
            case 12:
                blockDamage = 25000;
                break;

            //N
            case 13:
                blockDamage = 30000;
                break;

            //O
            case 14:
                blockDamage = 35000;
                break;

            //P
            case 15:
                blockDamage = 40000;
                break;

            //Q
            case 16:
                blockDamage = 45000;
                break;

            //R
            case 17:
                blockDamage = 50000;
                break;

            //S
            case 18:
                blockDamage = 55000;
                break;

            //T
            case 19:
                blockDamage = 60000;
                break;

            //U
            case 20:
                blockDamage = 65000;
                break;

            //V
            case 21:
                blockDamage = 70000;
                break;

            //W
            case 22:
                blockDamage = 75000;
                break;

            //X
            case 23:
                blockDamage = 999999;
                break;
        }

        return blockDamage;
    }

    void outOfRangeBoard()
    {
        if (transform.position.x >= gameBoard.blockGridPos[0, gameBoard.blockGridPos.GetLength(1) - 2].x)
        {
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }

        if (transform.position.x <= gameBoard.blockGridPos[0, 1].x)
        {
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }

        if (transform.position.y >= gameBoard.blockGridPos[1, 0].y)
        {
            transform.position = new Vector3(transform.position.x, 3.36f, 0);
        }

        if (transform.position.y <= gameBoard.blockGridPos[gameBoard.blockGridPos.GetLength(0) - 2, 0].y)
        {
            transform.position = new Vector3(transform.position.x, -3.36f, 0);
        }
    }
    void DragChangedGridPos() //드래그 좌표 변경 + 머지
    {
        for (int i = 1; i < gameBoard.blockGridPos.GetLength(0); i++)
        {
            int newX = 0, newY = 0;

            for (int j = 1; j < gameBoard.blockGridPos.GetLength(1); j++)
            {
                if (transform.position.x >= gameBoard.blockGridPos[i, j].x - bounds.size.x / 2 &&
                    transform.position.x <= gameBoard.blockGridPos[i, j].x + bounds.size.x / 2 &&
                    transform.position.y <= gameBoard.blockGridPos[i, j].y + bounds.size.y / 2 &&
                    transform.position.y >= gameBoard.blockGridPos[i, j].y - bounds.size.y / 2 &&
                    manager.blocks[i, j] == null)
                {
                    newX = i;
                    newY = j;

                    manager.blocks[gridX, gridY] = null;
                    manager.blocks[newX, newY] = gameObject;

                    gridX = newX;
                    gridY = newY;
                }

                //내가 가는 칸에 같은 레벨의 블럭이 있으면 머지
                else if (transform.position.x >= gameBoard.blockGridPos[i, j].x - bounds.size.x / 2 &&
                    transform.position.x <= gameBoard.blockGridPos[i, j].x + bounds.size.x / 2 &&
                    transform.position.y <= gameBoard.blockGridPos[i, j].y + bounds.size.y / 2 &&
                    transform.position.y >= gameBoard.blockGridPos[i, j].y - bounds.size.y / 2 &&
                    manager.blocks[i, j] != gameObject &&
                    manager.blocks[i, j].gameObject.GetComponent<Block>().level == level)
                {
                    Block other = manager.blocks[i, j].gameObject.GetComponent<Block>();

                    manager.blocks[gridX, gridY] = null;

                    //드래그상태인 블럭 없애기
                    DragMerge();

                    //상대블럭 레벨업
                    other.DragLevelUp();
                }
            }
        }
    }

    void DragMerge() //머지 함수
    {
        EffectPlay();

        Destroy(gameObject);
    }

    void DragLevelUp() //레벨업을 위한 함수
    {
        EffectPlay();
        GetDamage();
        manager.PlaySound(manager.hitClip, 0.8f);

        //콤보
        manager.comboAtk++;

        if (manager.comboAtk >= 3)
        {
            blockDamage = blockDamage + (int)(blockDamage * 0.1);
        }

        //콤보 텍스트 변경 및 재생
        if (manager.comboAtk != 0)
            enemyManager.comboText.gameObject.SetActive(true);

        enemyManager.comboText.text = manager.comboAtk.ToString() + "combo";

        //데미지 계산
        //아리스토 텔레스 무적
        if (enemyManager.isAristo_Sp &&
            (enemyManager.aristo_Sp_NomTurn >= 1 && enemyManager.aristo_Sp_NomTurn <= 3))
        {
            enemyManager.enemy_DamageHP = enemyManager.enemy_Health;

            //대미지 텍스트 변경 및 재생
            enemyManager.damagePrefabs.GetComponent<TextMeshProUGUI>().text = "0";
            Instantiate(enemyManager.damagePrefabs, enemyManager.enemyDamageTextGroup);

            enemyManager.aristo_Sp_NomTurn--;

            enemyManager.PlayerAtk();

            if (enemyManager.aristo_Sp_NomTurn == 0)
                enemyManager.isAristo_Sp = false;
        }

        //에피쿠로스 스폐셜
        else if (enemyManager.isEpicuru_Sp &&
                (enemyManager.epicuru_Sp_NomTurn >= 1 && enemyManager.epicuru_Sp_NomTurn <= 3))
        {
            float x = 2 / 3f;
            enemyManager.enemy_DamageHP -= (float)blockDamage * x;

            enemyManager.epicuru_Sp_NomTurn--;

            if (enemyManager.epicuru_Sp_NomTurn == 0)
                enemyManager.isEpicuru_Sp = false;
        }

        else
            enemyManager.enemy_DamageHP -= blockDamage;

        //턴 계산
        manager.curt_turns++;

        //레벨+
        level++;

        anim.SetInteger("Level", level);

        if (!tuto.tuto_merge)
        {
            manager.TutoTextLast();
            tuto.tuto_merge = true;
        }

        isMerge = false;
    }

    void BumpBlock() //블럭 부딪히기
    {
        //왼쪽 블럭
        if (manager.blocks[gridX, gridY - 1] != null && manager.blocks[gridX, gridY - 1] != gameObject &&
            manager.blocks[gridX, gridY - 1].gameObject.GetComponent<Block>().level != level)
        {
            Block otherBlock = manager.blocks[gridX, gridY - 1].gameObject.GetComponent<Block>();

            if (transform.position.x <= otherBlock.transform.position.x + bounds.size.x - 0.1f)
            {
                transform.position = new Vector3(otherBlock.transform.position.x + bounds.size.x - 0.1f,
                                               transform.position.y, 0f);
            }
        }

        //아래쪽 블럭
        if (manager.blocks[gridX + 1, gridY] != null && manager.blocks[gridX + 1, gridY] != gameObject &&
            manager.blocks[gridX + 1, gridY].gameObject.GetComponent<Block>().level != level)
        {
            Block otherBlock = manager.blocks[gridX + 1, gridY].gameObject.GetComponent<Block>();

            if (transform.position.y <= otherBlock.transform.position.y + bounds.size.y - 0.1f)
            {
                transform.position =
                    new Vector3(transform.position.x, otherBlock.transform.position.y + bounds.size.y - 0.1f, 0f);
            }
        }

        //오른쪽 블럭
        if (manager.blocks[gridX, gridY + 1] != null && manager.blocks[gridX, gridY + 1] != gameObject &&
            manager.blocks[gridX, gridY + 1].gameObject.GetComponent<Block>().level != level)
        {
            Block otherBlock = manager.blocks[gridX, gridY + 1].gameObject.GetComponent<Block>();

            if (transform.position.x >= otherBlock.transform.position.x - bounds.size.x + 0.1f)
            {
                transform.position = new Vector3(otherBlock.transform.position.x - bounds.size.x + 0.1f,
                                               transform.position.y, 0f);
            }
        }

        //위쪽 블럭
        if (manager.blocks[gridX - 1, gridY] != null && manager.blocks[gridX - 1, gridY] != gameObject &&
            manager.blocks[gridX - 1, gridY].gameObject.GetComponent<Block>().level != level)
        {
            Block otherBlock = manager.blocks[gridX - 1, gridY].gameObject.GetComponent<Block>();

            if (transform.position.y >= otherBlock.transform.position.y - bounds.size.y + 0.1f)
            {
                transform.position =
                    new Vector3(transform.position.x, otherBlock.transform.position.y - bounds.size.y + 0.1f, 0f);
            }
        }
    }

    void DropChangedGridPos() //드랍 좌표 변경 + 머지
    {
        int newY = int.MaxValue;
        float minDis = float.MaxValue;

        for (int i = 1; i < 7; i++)
        {
            var distance = Mathf.Abs(transform.position.x - gameBoard.blockGridPos[0, i].x);
            if (minDis > distance)
            {
                minDis = distance;
                newY = i;
            }
        }

        int newX = int.MaxValue;

        for (int i = 7; i >= 0; i--)
        {
            if (manager.blocks[i, newY] == null ||   //칸이 null일때
                manager.blocks[i + 1, newY] == null && manager.blocks[i, newY] == gameObject) // or 밑 칸이 null이고 i는 자기자신일 때
            {
                newX = i;

                manager.blocks[gridX, gridY] = null;
                manager.blocks[newX, newY] = gameObject;
                gridX = newX;
                gridY = newY;

                manager.comboAtk = 0;
                enemyManager.comboText.gameObject.SetActive(false);

                StartCoroutine(GravityRoutine());  //중력 애니메이션

                break;
            }

            else if (manager.blocks[i + 1, newY] != null && manager.blocks[i, newY] == gameObject && //밑 칸이null이 아니고 i가 자기자신이면서
                manager.blocks[i + 1, newY].gameObject.GetComponent<Block>().level != level)  //밑 블럭 레벨이 다를 때 (같으면 내려가야하기때문
            {
                transform.position = new Vector3(gameBoard.blockGridPos[i, newY].x, gameBoard.blockGridPos[i, newY].y, 0);

                manager.comboAtk = 0;
                enemyManager.comboText.gameObject.SetActive(false);

                break;
            }

            else if (manager.blocks[i - 1, newY] == null && manager.blocks[i, newY] != gameObject && //윗 칸이 null이면서 자신이 아니고
                     manager.blocks[i, newY].gameObject.GetComponent<Block>().level == level)       //레벨이 같을 때
            {
                Block other = manager.blocks[i, newY].gameObject.GetComponent<Block>();

                newX = i;

                manager.blocks[gridX, gridY] = null;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());

                //나 숨기기
                DropMerge();

                //상대 레벨업
                other.DropLevelUp();

                break;
            }

            else if (manager.blocks[i, newY] == gameObject &&  //
                     manager.blocks[i + 1, newY].gameObject.GetComponent<Block>().level == level)           //자신이 아니면서 같은 레벨일때
            {
                Block other = manager.blocks[i + 1, newY].gameObject.GetComponent<Block>();

                newX = i + 1;

                manager.blocks[gridX, gridY] = null;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());

                //나 숨기기
                DropMerge();

                //상대 레벨업
                other.DropLevelUp();

                break;
            }
        }

        if (gridX == 1)
            manager.isOver = true;
    }

    void DropMerge() //머지 함수
    {
        isMerge = true;

        box.enabled = false;

        StartCoroutine(DropMergeRoutine()); //애니메이션 주기 위한 코루틴
    }

    IEnumerator DropMergeRoutine() //머지 애니메이션
    {
        yield return new WaitForSeconds(0.2f);

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void DropLevelUp() //레벨업을 위한 함수
    {
        isMerge = true;

        if (!tuto.tuto_merge)
        {
            manager.TutoTextLast();
            tuto.tuto_merge = true;
        }

        StartCoroutine(DropLevelUpRoutine());
    }

    IEnumerator DropLevelUpRoutine() //애니메이션 기다리기
    {
        yield return new WaitForSeconds(0.2f);

        EffectPlay();
        GetDamage();
        manager.PlaySound(manager.hitClip, 0.8f);

        //콤보
        manager.comboAtk++;

        if (manager.comboAtk >= 3)
        {
            blockDamage = blockDamage + (int)(blockDamage * 0.1);
        }

        //콤보 텍스트 변경 및 재생
        if (manager.comboAtk != 0)
            enemyManager.comboText.gameObject.SetActive(true);

        enemyManager.comboText.text = manager.comboAtk.ToString() + "combo";

        //데미지 계산
        //아리스토 텔레스 무적
        if (enemyManager.isAristo_Sp &&
            (enemyManager.aristo_Sp_NomTurn >= 1 && enemyManager.aristo_Sp_NomTurn <= 3))
        {
            enemyManager.enemy_DamageHP = enemyManager.enemy_Health;

            //대미지 텍스트 변경 및 재생
            enemyManager.damagePrefabs.GetComponent<TextMeshProUGUI>().text = "0";
            Instantiate(enemyManager.damagePrefabs, enemyManager.enemyDamageTextGroup);

            enemyManager.aristo_Sp_NomTurn--;

            enemyManager.PlayerAtk();

            if (enemyManager.aristo_Sp_NomTurn == 0)
                enemyManager.isAristo_Sp = false;
        }

        else if (enemyManager.isEpicuru_Sp &&
                (enemyManager.epicuru_Sp_NomTurn >= 1 && enemyManager.epicuru_Sp_NomTurn <= 3))
        {
            float x = 2 / 3f;
            enemyManager.enemy_DamageHP -= (float)blockDamage * x;

            enemyManager.epicuru_Sp_NomTurn--;

            if (enemyManager.epicuru_Sp_NomTurn == 0)
                enemyManager.isEpicuru_Sp = false;
        }

        else
            enemyManager.enemy_DamageHP -= blockDamage;

        //턴 계산
        manager.curt_turns++;

        //레벨+
        anim.SetInteger("Level", level + 1);

        level++;

        isMerge = false;
    }

    IEnumerator GravityRoutine()   //중력 애니메이션 코루틴으로 구현
    {
        int frameCount = 0;

        while (frameCount < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        gameBoard.blockGridPos[gridX, gridY], 30f * Time.deltaTime);

            frameCount++;

            yield return null;
        }
    }

    void Snap()     //스냅함수
    {
        float[] distanceArray = new float[6];  //오브젝트 거리와 라인별 거리차이를 저장할 배열선언
        float min;

        for (int i = 0; i < 6; i++)  //오브젝트 거리계산 후 배열에 값 저장
        {
            distanceArray[i] = Vector3.Distance(transform.position, gameBoard.blockGridPos[0, i + 1]);
        }

        min = distanceArray[0];
        for (int i = 1; i < 6; i++)     //배열에서 가장 작은 값 찾기 = 가장 가까운 라인찾기
        {
            if (min > distanceArray[i])
            {
                min = distanceArray[i];
            }
        }

        //오브젝트 위치를 가장 가까운 라인의 x값만 대입 array.IndexOf(배열, 찾는값)
        //밑의 코드에서는 가장작은 값을 찾고 위치 반환
        transform.position = new Vector3(gameBoard.blockGridPos[0, Array.IndexOf(distanceArray, min) + 1].x,
                                                gameBoard.blockGridPos[gridX, 0].y, 0);

    }

    void EffectPlay()
    {
        particle.transform.position = transform.position;
        particle.transform.localScale = transform.localScale;
        particle.Play();
    }


}
