using System;
using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public GameBoard gameBoard;
    public EnemyUI enemyUI;

    public int level, gridX, gridY;
    public bool select, isMerge;

    Animator anim;
    BoxCollider2D box;

    Vector3 mousePos;
    Bounds bounds;  //Bounds 잘은 모르겠는데 콜라이더의 크기를 position기준으로 가져옴

    void Awake()
    {
        anim = GetComponent<Animator>();

        box = GetComponent<BoxCollider2D>();
        bounds = box.bounds;

        enemyUI = GameObject.Find("Slider").GetComponent<EnemyUI>();
    }
    void Start()
    {
        gameBoard = new GameBoard();
    }
    void Update()
    {
        Debug.Log("blocks[" + gridX + ", " + gridY + "]");
    }

    private void OnEnable() //스크립트가 활성화 될 때 실행되는 이벤트함수
    {
        anim.SetInteger("Level", level); //애니메이터 int형 파라미터
    }

    public void OnMouseDown()
    {
        select = true;
    }   //BoxCast();

    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mousePos.z = 0;

        transform.position = mousePos;
        //transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //선형보간

        //좌표변경 함수 + 드래그머지
        DragChangedGridPos();

        //보드판 밖으로 못 나가게
        if (transform.position.x >= gameBoard.blockGridPos[0, gameBoard.blockGridPos.GetLength(1) - 1].x)
        {
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }

        if (transform.position.x <= gameBoard.blockGridPos[0, 0].x)
        {
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }

        if (transform.position.y >= gameBoard.blockGridPos[0, 0].y)
        {
            transform.position = new Vector3(transform.position.x, 3.36f, 0);
        }

        if (transform.position.y <= gameBoard.blockGridPos[gameBoard.blockGridPos.GetLength(0) - 1, 0].y)
        {
            transform.position = new Vector3(transform.position.x, -3.36f, 0);
        }
    }

    public void OnMouseUp()
    {
        //스냅기능
        Snap();

        //2차원 배열 좌표변경 후 위치변경 + 머지
        DropChangedGridPos();

        select = false;
    }

    void DragChangedGridPos() //좌표 변경
    {
        for (int i = 0; i < gameBoard.blockGridPos.GetLength(0); i++)
        {
            int newX = 0, newY = 0;

            for (int j = 0; j < gameBoard.blockGridPos.GetLength(1); j++)
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

                else if (transform.position.x >= gameBoard.blockGridPos[i, j].x - bounds.size.x / 2 &&
                    transform.position.x <= gameBoard.blockGridPos[i, j].x + bounds.size.x / 2 &&
                    transform.position.y <= gameBoard.blockGridPos[i, j].y + bounds.size.y / 2 &&
                    transform.position.y >= gameBoard.blockGridPos[i, j].y - bounds.size.y / 2 &&
                    manager.blocks[i, j] != gameObject &&
                    manager.blocks[i, j].gameObject.GetComponent<Block>().level == level)
                {
                    Block other = manager.blocks[i, j].gameObject.GetComponent<Block>();

                    manager.blocks[gridX, gridY] = null;

                    //드래그상태인 블럭 숨기기
                    DragMerge(other.transform.position);

                    //상대블럭 레벨업
                    other.DragLevelUp();
                }
            }
        }
    }

    void DragMerge(Vector3 targetPos) //머지 함수
    {
        isMerge = true;

        box.enabled = false;

        StartCoroutine(DragMergeRoutine(targetPos)); //애니메이션 주기 위한 코루틴
    }

    IEnumerator DragMergeRoutine(Vector3 targetPos) //머지 애니메이션
    {
        int frameCount = 0;

        while (frameCount < 20) //이동 ********현재 이놈이 문제 고쳐야됨 level이 20번 더해짐 얘떄매
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f); //target까지 부드럽게 이동
            yield return null; //이게 없으면 한 프레임 안에서 반복문이 돌아서 의미X
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void DragLevelUp() //레벨업을 위한 함수
    {
        isMerge = true;

        StartCoroutine(DragLevelUpRoutine()); //애니메이션주기 위한 코루틴
    }

    IEnumerator DragLevelUpRoutine() //레벨업 애니메이션
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //Mathf(인자값 중 최대값반환);maxLevel 설정

        isMerge = false;
    }

    void DropChangedGridPos()
    {
        try
        {
            int newY = int.MaxValue;
            float minDis = float.MaxValue;
            for (int i = 0; i < 6; i++)
            {
                var distance = Mathf.Abs(transform.position.x - gameBoard.blockGridPos[0, i].x);
                if (minDis > distance)
                {
                    minDis = distance;
                    newY = i;
                }
            }

            int newX = int.MaxValue;
            for (int i = 6; i >= 0; i--)
            {
                if (manager.blocks[i, newY] == null || manager.blocks[i, newY] == gameObject)
                {
                    newX = i;

                    manager.blocks[gridX, gridY] = null;
                    manager.blocks[newX, newY] = gameObject;
                    gridX = newX;
                    gridY = newY;

                    StartCoroutine(GravityRoutine());  //중력 애니메이션
                    break;
                }

                else if (manager.blocks[i - 1, newY] == null && manager.blocks[i, newY].GetComponent<Block>().level == level)
                {
                    Block other = manager.blocks[i, newY].gameObject.GetComponent<Block>();

                    newX = i;

                    manager.blocks[gridX, gridY] = null;
                    gridX = newX;
                    gridY = newY;

                    StartCoroutine(GravityRoutine());

                    //나 숨기기
                    DropMerge(other.transform.position);

                    //상대 레벨업
                    other.DropLevelUp();
                    break;
                }
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            manager.gameOver = true;
        }
    }

    void DropMerge(Vector3 targetPos) //머지 함수
    {
        isMerge = true;

        box.enabled = false;

        StartCoroutine(DropMergeRoutine(targetPos)); //애니메이션 주기 위한 코루틴
    }

    IEnumerator DropMergeRoutine(Vector3 targetPos) //머지 애니메이션
    {
        yield return new WaitForSeconds(0.2f);

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void DropLevelUp() //레벨업을 위한 함수
    {
        isMerge = true;

        StartCoroutine(DropLevelUpRoutine()); //애니메이션주기 위한 코루틴
    }

    IEnumerator DropLevelUpRoutine() //레벨업 애니메이션
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //Mathf(인자값 중 최대값반환);maxLevel 설정

        isMerge = false;
    }

    IEnumerator GravityRoutine()   //중력 애니메이션 코루틴으로 구현
    {
        int frameCount = 0;

        while (frameCount < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        gameBoard.blockGridPos[gridX, gridY], 0.1f);
            frameCount++;

            yield return Time.deltaTime;
        }
    }

    void Snap()     //스냅함수
    {
        float[] distanceArray = new float[6];  //오브젝트 거리와 라인별 거리차이를 저장할 배열선언
        float min;

        for (int i = 0; i < 6; i++)  //오브젝트 거리계산 후 배열에 값 저장
        {
            distanceArray[i] = Vector3.Distance(transform.position, gameBoard.blockGridPos[0, i]);
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
        transform.position = new Vector3(gameBoard.blockGridPos[0, Array.IndexOf(distanceArray, min)].x,
                                                transform.position.y, 0);
    }

}
