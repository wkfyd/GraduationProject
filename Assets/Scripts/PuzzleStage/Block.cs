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
    Bounds bounds;  //Bounds ���� �𸣰ڴµ� �ݶ��̴��� ũ�⸦ position�������� ������

    //������ ũ�Ⱑ 1.155 2.205  0.525�� ������ ������ ũ��  �����հ��α��� 1.05
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
            //���� ��� ��ġ�� ��������
            float meX = transform.position.x;
            float meY = transform.position.y;
            float otherX = other.transform.position.x;
            float otherY = other.transform.position.y;

            //������ ������ ��, ���� �����ʿ� ���� ��
            if (meY < otherY || (meY == otherY && meX > otherX))
            {
                //������ �����
                other.Merge(transform.position);

                //���� ������
                LevelUp();
            }
        }
    }
    private void OnEnable() //��ũ��Ʈ�� Ȱ��ȭ �� �� ����Ǵ� �̺�Ʈ�Լ�
    {
        anim.SetInteger("Level", level); //�ִϸ����� int�� �Ķ����
        
        rigid.constraints =  RigidbodyConstraints2D.FreezeRotation; //������Ʈ Rotation��, x�� ����
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.velocity = Vector3.zero;
    }   //BoxCast();

    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������ǥ ���콺 ��ġ
        mousePos.z = 0;

        transform.position = mousePos;
        //transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //��������

        ChangedGridPos();

        //������ ������ �� ������
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
        //�������
        Snap();

        //2���� �迭 ��ǥ���� �� ��ġ����
        DropChangedGridPos();

        //����


        select = false;   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            Block other = collision.gameObject.GetComponent<Block>();

            if (level == other.level && !isMerge && !other.isMerge && level < 23)
            {
                //���� ��� ��ġ�� ��������
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                //������ ������ ��, ���� �����ʿ� ���� ��
                if (meY < otherY || (meY == otherY && meX > otherX))
                {
                    //������ �����
                    other.Merge(transform.position);

                    //���� ������
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

    public void Merge(Vector3 targetPos) //���� �Լ�
    {
        isMerge = true;

        enemyUI.curHp -= 10;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(MergeRoutine(targetPos)); //�ִϸ��̼� �ֱ� ���� �ڷ�ƾ
    }

    IEnumerator MergeRoutine(Vector3 targetPos) //���� �ִϸ��̼�
    {
        int frameCount = 0;

        while (frameCount < 20) //�̵�
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f); //target���� �ε巴�� �̵�
            yield return null; //�̰� ������ �� ������ �ȿ��� �ݺ����� ���Ƽ� �ǹ�X
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

        StartCoroutine("GravityRoutine");  //�߷� �ִϸ��̼�
    }

    IEnumerator GravityRoutine()   //�߷� �ִϸ��̼� �ڷ�ƾ���� ����
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

    

    void LevelUp() //�������� ���� �Լ�
    {
        isMerge = true;

        rigid.velocity = Vector2.zero; //������ �� ���ص� �� �ִ� �����ӵ� ���� �̵��ӵ�=velocity, 2d�� vector2
        rigid.angularVelocity = 0; //ȸ���ӵ� �ʱ�ȭ, +�ð�, -�ݽð�

        StartCoroutine(LevelUpRoutine()); //�ִϸ��̼��ֱ� ���� �ڷ�ƾ
    }

    IEnumerator LevelUpRoutine() //������ �ִϸ��̼�
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //Mathf(���ڰ� �� �ִ밪��ȯ);maxLevel ����

        isMerge = false;
    }

    void Snap()     //�����Լ�
    {
        float[] distanceArray = new float[6];  //������Ʈ �Ÿ��� ���κ� �Ÿ����̸� ������ �迭����
        float min;

        for (int i = 0; i < 6; i++)  //������Ʈ �Ÿ���� �� �迭�� �� ����
        {
            distanceArray[i] = Vector3.Distance(transform.position, gameBoard.blockGridPos[0, i]);
        }

        min = distanceArray[0];
        for (int i = 1; i < 6; i++)     //�迭���� ���� ���� �� ã�� = ���� ����� ����ã��
        {
            if (min > distanceArray[i])
            {
                min = distanceArray[i];
            }
        }

        //������Ʈ ��ġ�� ���� ����� ������ x���� ���� array.IndexOf(�迭, ã�°�)
        //���� �ڵ忡���� �������� ���� ã�� ��ġ ��ȯ
        transform.position = new Vector3(gameBoard.blockGridPos[0, Array.IndexOf(distanceArray, min)].x,
                                                transform.position.y, 0);
    }

    
}
