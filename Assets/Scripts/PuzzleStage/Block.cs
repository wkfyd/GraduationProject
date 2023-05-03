using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public GameBoard gameBoard;
    public EnemyUI enemyUI;

    public int level, gridX, gridY;
    public bool select = false;
    public bool isMerge;


    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D box;

    Vector3 mousePos;
    Bounds bounds;  //Bounds 잘은 모르겠는데 콜라이더의 크기를 position기준으로 가져옴

    //프리펩 크기가 1.155 2.205  0.525가 프리팹 반절의 크기  프리팹가로길이 1.05
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
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
        Debug.Log("blocks[" + gridY + ", " + gridX + "]");

        Block other = manager.blocks[6, 0].gameObject.GetComponent<Block>();


        if (manager.blocks[gridY, gridX].GetComponent<Block>().level == level)
        {
            //나와 상대 위치값 가져오기
            float meX = transform.position.x;
            float meY = transform.position.y;
            float otherX = other.transform.position.x;
            float otherY = other.transform.position.y;

            //동일한 높이일 때, 내가 오른쪽에 있을 때
            if (meY < otherY || (meY == otherY && meX > otherX))
            {
                //상대방은 숨기기
                other.Merge(transform.position);

                //나는 레벨업
                LevelUp();
            }
        }
    }
    private void OnEnable() //스크립트가 활성화 될 때 실행되는 이벤트함수
    {
        anim.SetInteger("Level", level); //애니메이터 int형 파라미터
        
        rigid.constraints =  RigidbodyConstraints2D.FreezeRotation; //오브젝트 Rotation값, x값 고정
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.velocity = Vector3.zero;
    }   //BoxCast();

    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mousePos.z = 0;

        transform.position = mousePos;
        //transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //선형보간

        ChangedGridPos();

        //보드판 밖으로 못 나가게
        if (transform.position.x >= gameBoard.blockGridPos[0, gameBoard.blockGridPos.GetLength(1)-1].x)
        {
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }

        if(transform.position.x <= gameBoard.blockGridPos[0, 0].x)
        {
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }

        if (transform.position.y >= gameBoard.blockGridPos[0, 0].y)
        {
            transform.position = new Vector3(transform.position.x, 3.36f, 0);
        }

        if (transform.position.y <= gameBoard.blockGridPos[gameBoard.blockGridPos.GetLength(0)-1, 0].y)
        {
            transform.position = new Vector3(transform.position.x, -3.36f, 0);
        }
    }

    public void OnMouseUp()
    {
        //스냅기능
        Snap();

        //2차원 배열 좌표변경 후 위치변경
        DropChangedGridPos();

        //머지


        select = false;   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            Block other = collision.gameObject.GetComponent<Block>();

            if (level == other.level && !isMerge && !other.isMerge && level < 23)
            {
                //나와 상대 위치값 가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                //동일한 높이일 때, 내가 오른쪽에 있을 때
                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    //상대방은 숨기기
                    other.Merge(transform.position);

                    //나는 레벨업
                    LevelUp();
                }
            }
        }
    }

    void ChangedGridPos()
    {
        for (int i = 0; i < gameBoard.blockGridPos.GetLength(0); i++)
        {
            int newX = 0, newY = 0;

            for (int j = 0; j < gameBoard.blockGridPos.GetLength(1); j++)
            {
                if (transform.position.x >= gameBoard.blockGridPos[i, j].x - bounds.size.x / 2 &&
                    transform.position.x <= gameBoard.blockGridPos[i, j].x + bounds.size.x / 2 &&
                    transform.position.y <= gameBoard.blockGridPos[i, j].y + bounds.size.y / 2 &&
                    transform.position.y >= gameBoard.blockGridPos[i, j].y - bounds.size.y / 2)
                {
                    newY = i;
                    newX = j;

                    manager.blocks[gridY, gridX] = null;
                    manager.blocks[newY, newX] = gameObject;

                    gridX = newX;
                    gridY = newY;
                }
            }
        }
    }

    public void Merge(Vector3 targetPos) //머지 함수
    {
        isMerge = true;

        enemyUI.curHp -= 10;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(MergeRoutine(targetPos)); //애니메이션 주기 위한 코루틴
    }

    IEnumerator MergeRoutine(Vector3 targetPos) //머지 애니메이션
    {
        int frameCount = 0;

        while (frameCount < 20) //이동
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f); //target까지 부드럽게 이동
            yield return null; //이게 없으면 한 프레임 안에서 반복문이 돌아서 의미X
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void DropChangedGridPos()
    {
        int newX = int.MaxValue;
        float minDis = float.MaxValue;
        for (int i = 0; i < 6; i++)
        {
            var distance = Mathf.Abs(transform.position.x - gameBoard.blockGridPos[0, i].x);
            if (minDis > distance)
            {
                minDis = distance;
                newX = i;
            }
        }

        int newY = int.MaxValue;
        for (int i = 6; i >= 0; i--)
        {
            if (manager.blocks[i, newX] == null || manager.blocks[i, newX].GetComponent<Block>().level == level)
            {
                newY = i;
                break;
            }
        }

        manager.blocks[gridY, gridX] = null;
        manager.blocks[newY, newX] = gameObject;
        gridX = newX;
        gridY = newY;

        StartCoroutine("GravityRoutine");  //중력 애니메이션
    }

    IEnumerator GravityRoutine()   //중력 애니메이션 코루틴으로 구현
    {
        int frameCount = 0;

        while (frameCount < 100)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                        gameBoard.blockGridPos[gridY, gridX], 35f * Time.deltaTime);
            frameCount++;
            yield return Time.deltaTime;
        }
    }

    

    void LevelUp() //레벨업을 위한 함수
    {
        isMerge = true;

        rigid.velocity = Vector2.zero; //레벨업 중 방해될 수 있는 물리속도 제거 이동속도=velocity, 2d라서 vector2
        rigid.angularVelocity = 0; //회전속도 초기화, +시계, -반시계

        StartCoroutine(LevelUpRoutine()); //애니메이션주기 위한 코루틴
    }

    IEnumerator LevelUpRoutine() //레벨업 애니메이션
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //Mathf(인자값 중 최대값반환);maxLevel 설정

        isMerge = false;
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
