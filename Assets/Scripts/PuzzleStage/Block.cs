using System;
using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public GameBoard gameBoard;
    public EnemyUI enemyUI;

    public int level, gridX, gridY;
    public int downGrid_X, downGrid_Y;
    public bool select, isMerge, levelUpOnce; //������ �ѹ��� �����ֱ�����

    public Bounds bounds;  //Bounds ���� �𸣰ڴµ� �ݶ��̴��� ũ�⸦ position�������� ������

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
    void Update()
    {
    }

    private void OnEnable() //��ũ��Ʈ�� Ȱ��ȭ �� �� ����Ǵ� �̺�Ʈ�Լ�
    {
        anim.SetInteger("Level", level); //�ִϸ����� int�� �Ķ����
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
        mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������ǥ ���콺 ��ġ
        mouse_Pos.z = 0;

        transform.position = Vector3.Lerp(transform.position, mouse_Pos, 50f * Time.deltaTime);

        //���� ������ �ε�����
        BumpBlock();

        //������ ������ �� ������
        outOfRangeBoard();

        //��ǥ���� �Լ� + �巡�׸���
        DragChangedGridPos();
    }

    public void OnMouseUp()
    {
        //�������
        Snap();

        //2���� �迭 ��ǥ���� �� ��ġ���� + ����
        if ((downGrid_X != gridX || downGrid_Y != gridY) || manager.blocks[gridX + 1, gridY] != null &&
            manager.blocks[gridX + 1, gridY].gameObject.GetComponent<Block>().level == level)
        {
        DropChangedGridPos();
        }
        

        select = false;
        levelUpOnce = false;
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
    void DragChangedGridPos() //�巡�� ��ǥ ���� + ����
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

                else if (transform.position.x >= gameBoard.blockGridPos[i, j].x - bounds.size.x / 2 &&
                    transform.position.x <= gameBoard.blockGridPos[i, j].x + bounds.size.x / 2 &&
                    transform.position.y <= gameBoard.blockGridPos[i, j].y + bounds.size.y / 2 &&
                    transform.position.y >= gameBoard.blockGridPos[i, j].y - bounds.size.y / 2 &&
                    manager.blocks[i, j] != gameObject && 
                    manager.blocks[i, j].gameObject.GetComponent<Block>().level == level)
                {
                    Block other = manager.blocks[i, j].gameObject.GetComponent<Block>();

                    manager.blocks[gridX, gridY] = null;

                    //�巡�׻����� �� �����
                    DragMerge(other.transform.position);

                    //���� ������
                    if (levelUpOnce) //���� ������ �������Ͽ� �ѹ��� �����ֱ�����
                    {
                        other.DragLevelUp();

                        levelUpOnce = false;
                    }
                }
            }
        }
    }

    void DragMerge(Vector3 targetPos) //���� �Լ�
    {
        isMerge = true;

        box.enabled = false;

        StartCoroutine(DragMergeRoutine(targetPos)); //�ִϸ��̼� �ֱ� ���� �ڷ�ƾ
    }

    IEnumerator DragMergeRoutine(Vector3 targetPos) //���� �ִϸ��̼�
    {
        int frameCount = 0;

        while (frameCount < 5)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f); //target���� �ε巴�� �̵�

            frameCount++;

            yield return null; //�̰� ������ �� ������ �ȿ��� �ݺ����� ���Ƽ� �ǹ�X, null�� �����Ӹ��� ����
        }

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void BumpBlock() //�� �ε�����
    {
        //���� ��
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

        //�Ʒ��� ��
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

        //������ ��
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

        //���� ��
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

    void DragLevelUp() //�������� ���� �Լ�
    {
        isMerge = true;

        StartCoroutine(DragLevelUpRoutine()); //�ִϸ��̼��ֱ� ���� �ڷ�ƾ
    }

    IEnumerator DragLevelUpRoutine() //������ �ִϸ��̼�
    {
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //Mathf(���ڰ� �� �ִ밪��ȯ);maxLevel ����

        yield return new WaitForSeconds(0.1f);

        anim.SetInteger("Level", level);

        isMerge = false;
    }

    void DropChangedGridPos() //��� ��ǥ ���� + ����
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
            if (manager.blocks[i, newY] == null ||   //ĭ�� null�϶�
                manager.blocks[i + 1, newY] == null && manager.blocks[i, newY] == gameObject) // or �� ĭ�� null�̰� i�� �ڱ��ڽ��� ��
            {
                newX = i;

                manager.blocks[gridX, gridY] = null;
                manager.blocks[newX, newY] = gameObject;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());  //�߷� �ִϸ��̼�

                break;
            }

            else if(manager.blocks[i + 1, newY] != null && manager.blocks[i, newY] == gameObject && //�� ĭ��null�� �ƴϰ� i�� �ڱ��ڽ��̸鼭
                manager.blocks[i + 1, newY].gameObject.GetComponent<Block>().level != level)  //�� �� ������ �ٸ� �� (������ ���������ϱ⶧��
            {
                transform.position = new Vector3(gameBoard.blockGridPos[i, newY].x, gameBoard.blockGridPos[i, newY].y, 0);

                break;
            }

            else if (manager.blocks[i - 1, newY] == null && manager.blocks[i, newY] != gameObject && //�� ĭ�� null�̸鼭 �ڽ��� �ƴϰ�
                     manager.blocks[i, newY].gameObject.GetComponent<Block>().level == level)       //������ ���� ��
            {
                Block other = manager.blocks[i, newY].gameObject.GetComponent<Block>();

                newX = i;

                manager.blocks[gridX, gridY] = null;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());

                //�� �����
                DropMerge(other.transform.position);

                //��� ������
                other.DropLevelUp();

                break;
            }

            else if (manager.blocks[i, newY] == gameObject &&  //
                     manager.blocks[i + 1, newY].gameObject.GetComponent<Block>().level == level)           //�ڽ��� �ƴϸ鼭 ���� �����϶�
            {
                Block other = manager.blocks[i + 1, newY].gameObject.GetComponent<Block>();

                newX = i + 1;

                manager.blocks[gridX, gridY] = null;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());

                //�� �����
                DropMerge(other.transform.position);

                //��� ������
                other.DropLevelUp();

                break;
            }
        }
    }

    void DropMerge(Vector3 targetPos) //���� �Լ�
    {
        isMerge = true;

        box.enabled = false;

        StartCoroutine(DropMergeRoutine(targetPos)); //�ִϸ��̼� �ֱ� ���� �ڷ�ƾ
    }

    IEnumerator DropMergeRoutine(Vector3 targetPos) //���� �ִϸ��̼�
    {
        yield return new WaitForSeconds(0.2f);

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void DropLevelUp() //�������� ���� �Լ�
    {
        isMerge = true;

        StartCoroutine(DropLevelUpRoutine()); //�ִϸ��̼��ֱ� ���� �ڷ�ƾ
    }

    IEnumerator DropLevelUpRoutine() //������ �ִϸ��̼�
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);
        level++;

        manager.maxLevel = Mathf.Max(level, manager.maxLevel); //Mathf(���ڰ� �� �ִ밪��ȯ);maxLevel ����

        isMerge = false;
    }

    IEnumerator GravityRoutine()   //�߷� �ִϸ��̼� �ڷ�ƾ���� ����
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

    void Snap()     //�����Լ�
    {
        float[] distanceArray = new float[6];  //������Ʈ �Ÿ��� ���κ� �Ÿ����̸� ������ �迭����
        float min;

        for (int i = 0; i < 6; i++)  //������Ʈ �Ÿ���� �� �迭�� �� ����
        {
            distanceArray[i] = Vector3.Distance(transform.position, gameBoard.blockGridPos[0, i + 1]);
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
        transform.position = new Vector3(gameBoard.blockGridPos[0, Array.IndexOf(distanceArray, min) + 1].x,
                                                gameBoard.blockGridPos[gridX, 0].y, 0);
        
    }
}
