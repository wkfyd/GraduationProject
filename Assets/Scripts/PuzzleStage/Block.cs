using System;
using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public TutorialManager tuto;
    public EnemyManager bl_enemyManager;

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
        downGrid_X = gridX;
        downGrid_Y = gridY;

        levelUpOnce = true;
        select = true;
    }

    public void OnMouseDrag()
    {

        mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mouse_Pos.z = 0;

        transform.position = Vector3.Lerp(transform.position, mouse_Pos, 50f * Time.deltaTime);

        //블럭이 있으면 부딪히게
        BumpBlock();

        //보드판 밖으로 못 나가게
        outOfRangeBoard();

        //좌표변경 함수 + 드래그머지
        DragChangedGridPos();
    }

    public void OnMouseUp()
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

    int GetDamage()
    {
        switch (level)
        {
            case 0:
                blockDamage = 1;
                break;

            case 1:
                blockDamage = 2;
                break;

            case 2:
                blockDamage = 4;
                break;

            case 3:
                blockDamage = 8;
                break;

            case 4:
                blockDamage = 16;
                break;

            case 5:
                blockDamage = 80;
                break;

            case 6:
                blockDamage = 240;
                break;

            case 7:
                blockDamage = 520;
                break;

            case 8:
                blockDamage = 2080;
                break;

            case 9:
                blockDamage = 5000;
                break;

            case 10:
                blockDamage = 15000;
                break;

            case 11:
                blockDamage = 45000;
                break;

            case 12:
                blockDamage = 89000;
                break;

            case 13:
                blockDamage = 165000;
                break;

            case 14:
                blockDamage = 337000;
                break;

            case 15:
                blockDamage = 503400;
                break;

            case 16:
                blockDamage = 875600;
                break;

            case 17:
                blockDamage = 1034000;
                break;

            case 18:
                blockDamage = 1578900;
                break;

            case 19:
                blockDamage = 1867800;
                break;

            case 20:
                blockDamage = 2346700;
                break;

            case 21:
                blockDamage = 2789900;
                break;

            case 22:
                blockDamage = 3134500;
                break;

            case 23:
                blockDamage = 9999999;
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
        Player_Atk();

        StopCoroutine(Player_Atk());
        StartCoroutine(Player_Atk());

        bl_enemyManager.enemy_DamageHP = bl_enemyManager.enemy_DamageHP - blockDamage;
        manager.curt_turns++;

        level++;

        anim.SetInteger("Level", level);

        if (!tuto.tuto_merge)
        {
            tuto.TutoTextLast();
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

                StartCoroutine(GravityRoutine());  //중력 애니메이션

                break;
            }

            else if (manager.blocks[i + 1, newY] != null && manager.blocks[i, newY] == gameObject && //밑 칸이null이 아니고 i가 자기자신이면서
                manager.blocks[i + 1, newY].gameObject.GetComponent<Block>().level != level)  //밑 블럭 레벨이 다를 때 (같으면 내려가야하기때문
            {
                transform.position = new Vector3(gameBoard.blockGridPos[i, newY].x, gameBoard.blockGridPos[i, newY].y, 0);

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
            tuto.TutoTextLast();
            tuto.tuto_merge = true;
        }

        StartCoroutine(DropLevelUpRoutine());
    }

    IEnumerator DropLevelUpRoutine() //애니메이션 기다리기
    {
        yield return new WaitForSeconds(0.2f);

        EffectPlay();
        GetDamage();

        StopCoroutine(Player_Atk());
        StartCoroutine(Player_Atk());

        bl_enemyManager.enemy_DamageHP = bl_enemyManager.enemy_DamageHP - blockDamage;

        manager.curt_turns++;

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

    IEnumerator Player_Atk()
    {
        if (bl_enemyManager.enemy_NomAtk == 0)
        {
            bl_enemyManager.player_Status[0].SetActive(false);
            bl_enemyManager.player_Status[2].SetActive(true);

            yield return new WaitForSeconds(1.0f);

            bl_enemyManager.player_Status[0].SetActive(true);
            bl_enemyManager.player_Status[2].SetActive(false);
        }
        else
        {
            bl_enemyManager.player_Status[0].SetActive(false);
            bl_enemyManager.player_Status[1].SetActive(true);

            yield return new WaitForSeconds(1.0f);

            bl_enemyManager.player_Status[0].SetActive(true);
            bl_enemyManager.player_Status[1].SetActive(false);
        }
    }
}
