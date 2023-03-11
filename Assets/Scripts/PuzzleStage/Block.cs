using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public Snap snap = new Snap();

    public int level;
    public bool select = false;
    public bool isMerge;

    Vector3 mousePos;

    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D box;


    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable() //스크립트가 활성화 될 때 실행되는 이벤트함수
    {
        anim.SetInteger("Level", level); //애니메이터 int형 파라미터
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.simulated = false;
    }
    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //선형보간
    }
    public void OnMouseUp()
    {
        //스냅기능
        Snap();

        select = false;
        rigid.simulated = true;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            Block other = collision.gameObject.GetComponent<Block>();

            if(level == other.level && !isMerge && !other.isMerge && level < 7)
            {
                //나와 상대 위치값 가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;
                //1. 내가 아래에 있을때
                //2. 동일한 높이일 때, 내가 오른쪽에 있을 때
                if(meY < otherY || (meY == otherY && meX > otherX))
                {
                    //상대방은 숨기기
                    other.Hide(transform.position);
                    //나는 레벨업
                    LevelUp();
                }
            }
        }
    }

    public void Hide(Vector3 targetPos) //숨기기 함수
    {
        isMerge = true;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(HideRoutine(targetPos)); //애니메이션 주기 위한 코루틴
    }

    IEnumerator HideRoutine(Vector3 targetPos) //머지 애니메이션
    {
        int frameCount = 0;

        while (frameCount < 20) //이동
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f); //target까지 이동
            yield return null; //이게 없으면 한 프레임 안에서 반복문이 돌아서 의미X
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
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

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //maxLevel 설정

        isMerge = false;
    }

    void Snap()     //스냅함수
    {
        float[] distanceArray = new float[6];  //오브젝트 거리와 라인별 거리차이를 저장할 배열선언
        float min;

        for (int i = 0; i < snap.BlockGroupPos.Length; i++)  //오브젝트 거리계산 후 배열에 값 저장
        {
            distanceArray[i] = Vector3.Distance(transform.position, snap.BlockGroupPos[i]);
        }

        min = distanceArray[0];     //min 기본값설정

        for (int i = 1; i < snap.BlockGroupPos.Length; i++)     //배열에서 가장 작은 값 찾기 = 가장 가까운 라인찾기
        {
            if (min > distanceArray[i])
            {
                min = distanceArray[i];
            }
        }
        transform.position = new Vector3(snap.BlockGroupPos[Array.IndexOf(distanceArray, min)].x,
                                                transform.position.y, 0); //오브젝트 위치를 가장 가까운 라인의 x값만 대입
    }
}
