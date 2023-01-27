using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public int level;
    public bool select = false;
    public bool isMerge;

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
        anim.SetInteger("Level", level);
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.simulated = false;
    }
    public void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f);
    }
    public void OnMouseUp()
    {
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

    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;

        while (frameCount < 20)
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
            yield return null;
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void LevelUp()
    {
        isMerge = true;

        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine());
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel);

        isMerge = false;
    }
}
