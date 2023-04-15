using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public GameBoard gameBoard;

    public int level, gridX, gridY;
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
    void Start()
    {
        gameBoard = new GameBoard();
    }
    void Update()
    {
        
    }
    private void OnEnable() //��ũ��Ʈ�� Ȱ��ȭ �� �� ����Ǵ� �̺�Ʈ�Լ�
    {
        anim.SetInteger("Level", level); //�ִϸ����� int�� �Ķ����
        
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX |
        RigidbodyConstraints2D.FreezeRotation; //������Ʈ Rotation��, x�� ����
    }

    public void OnMouseDown()
    {
        select = true;
        rigid.simulated = false;
    }
    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������ǥ ���콺 ��ġ
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //��������

        //�̵��� 2�����迭������ ��ǥ������Ʈ


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

        //1�� ���ȸ� �߷� on
        rigid.gravityScale = 8f;
        Invoke("ChangeGravityScale", 1f);

        for (int i = 0; i < manager.blocks.GetLength(0); i++)
        {
            for (int j = 0; j < manager.blocks.GetLength(1); j++)
            {
                if (manager.blocks[i, j] != null)
                {
                    Debug.Log("���� ��� ������Ʈ�� (" + i + ", " + j + ") ��ġ�� �ֽ��ϴ�.");
                }
                if (manager.blocks[i, j] == null)
                    Debug.Log("���� ��� ������Ʈ�� (" + i + ", " + j + ") ��ġ�� XXXX");
            }
        }
        /*for (int k = 0; k < 7; k++)
        {
            for (int l = 0; l < 6; l++)
            {
                Debug.Log(k + "," + l);
                if (manager.blocks[k, l] != null)
                    Debug.Log("[ok]");
                if (manager.blocks[k, l] == null)
                    Debug.Log("null");
            }
        }*/

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
                //���� ��� ��ġ�� ��������
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;
                //������ ������ ��, ���� �����ʿ� ���� ��
                if(meY < otherY || (meY == otherY && meX > otherX))
                {
                    //������ �����
                    other.Hide(transform.position);
                    //���� ������
                    LevelUp();
                }
            }
        }
    }

    public void Hide(Vector3 targetPos) //����� �Լ�
    {
        isMerge = true;

        rigid.simulated = false;
        box.enabled = false;

        StartCoroutine(HideRoutine(targetPos)); //�ִϸ��̼� �ֱ� ���� �ڷ�ƾ
    }

    IEnumerator HideRoutine(Vector3 targetPos) //���� �ִϸ��̼�
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

        min = distanceArray[0];     //min �⺻������

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

    void ChangeGravityScale()
    {
        rigid.gravityScale = 0f;

        OnChangedGridPos();
        
    }

    public void OnChangedGridPos()
    {
        int newX = int.MaxValue;
        float minDis = float.MaxValue;
        for (int i = 0; i < 6; i++) {
            var distance = Mathf.Abs(transform.position.x - gameBoard.blockGridPos[0, i].x);
            if (minDis > distance)
            {
                minDis = distance;
                newX = i;
            }
        }

        minDis = float.MaxValue;
        int newY = int.MaxValue;
        for (int i = 6; i >= 0; i--)
        {
            float distance = Mathf.Abs(transform.position.y - gameBoard.blockGridPos[i, 0].y);
            if (minDis > distance)
            {
                minDis = distance;
                newY = i;
            }
        }


        manager.blocks[gridY, gridX] = null;
        manager.blocks[newY, newX] = gameObject;
        gridX = newX;
        gridY = newY;
        gameObject.transform.position = gameBoard.blockGridPos[gridY, gridX];
    }
}
