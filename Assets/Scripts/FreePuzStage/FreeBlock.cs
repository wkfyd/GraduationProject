using System;
using System.Collections;
using UnityEngine;

public class FreeBlock : MonoBehaviour
{
    public FreeGameManager manager;

    public GameBoard gameBoard;

    public ParticleSystem particle;

    public int level, gridX, gridY;
    public int downGrid_X, downGrid_Y;
    public int blockScore;
    public bool isParticle;
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

    void OnEnable() //��ũ��Ʈ�� Ȱ��ȭ �� �� ����Ǵ� �̺�Ʈ�Լ�
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
            manager.blocks[gridX + 1, gridY].gameObject.GetComponent<FreeBlock>().level == level)
        {
            DropChangedGridPos();
        }

        select = false;
        levelUpOnce = false;
    }

    //�� ���ھ�
    int GetScore()
    {
        switch (level)
        {
            //A
            case 0:
                blockScore = 10;
                break;

            //B
            case 1:
                blockScore = 30;
                break;

            //C
            case 2:
                blockScore = 90;
                break;

            //D
            case 3:
                blockScore = 270;
                break;

            //E
            case 4:
                blockScore = 810;
                break;

            //F
            case 5:
                blockScore = 2430;
                break;

            //G
            case 6:
                blockScore = 7290;
                break;

            //H
            case 7:
                blockScore = 14580;
                break;

            //I
            case 8:
                blockScore = 29160;
                break;

            //J
            case 9:
                blockScore = 34992;
                break;

            //K
            case 10:
                blockScore = 41990;
                break;

            //L
            case 11:
                blockScore = 50388;
                break;

            //M
            case 12:
                blockScore = 60465;
                break;

            //N
            case 13:
                blockScore = 72558;
                break;

            //O
            case 14:
                blockScore = 87069;
                break;

            //P
            case 15:
                blockScore = 104482;
                break;

            //Q
            case 16:
                blockScore = 125378;
                break;

            //R
            case 17:
                blockScore = 150453;
                break;

            //S
            case 18:
                blockScore = 180543;
                break;

            //T
            case 19:
                blockScore = 216651;
                break;

            //U
            case 20:
                blockScore = 259981;
                break;

            //V
            case 21:
                blockScore = 311977;
                break;

            //W
            case 22:
                blockScore = 374372;
                break;

            //X
            case 23:
                blockScore = 449246;
                break;
        }

        return blockScore;
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

                //���� ���� ĭ�� ���� ������ ���� ������ ����
                else if (transform.position.x >= gameBoard.blockGridPos[i, j].x - bounds.size.x / 2 &&
                    transform.position.x <= gameBoard.blockGridPos[i, j].x + bounds.size.x / 2 &&
                    transform.position.y <= gameBoard.blockGridPos[i, j].y + bounds.size.y / 2 &&
                    transform.position.y >= gameBoard.blockGridPos[i, j].y - bounds.size.y / 2 &&
                    manager.blocks[i, j] != gameObject &&
                    manager.blocks[i, j].gameObject.GetComponent<FreeBlock>().level == level)
                {
                    FreeBlock other = manager.blocks[i, j].gameObject.GetComponent<FreeBlock>();

                    manager.blocks[gridX, gridY] = null;

                    //�巡�׻����� �� ���ֱ�
                    DragMerge();

                    //���� ������
                    other.DragLevelUp();
                }
            }
        }
    }

    void DragMerge() //���� �Լ�
    {
        EffectPlay();

        Destroy(gameObject);
    }

    void DragLevelUp() //�������� ���� �Լ�
    {
        EffectPlay();
        GetScore();

        //�޺����
        manager.combo++;
        manager.comboText.text = manager.combo.ToString();

        if (manager.combo >= 3)
        {
            blockScore = blockScore + (int)(blockScore * 0.1);
        }

        //���� ���
        manager.scoreText.text = (manager.score += blockScore).ToString();

        //����+
        level++;

        anim.SetInteger("Level", level);

        isMerge = false;
    }

    void BumpBlock() //�� �ε�����
    {
        //���� ��
        if (manager.blocks[gridX, gridY - 1] != null && manager.blocks[gridX, gridY - 1] != gameObject &&
            manager.blocks[gridX, gridY - 1].gameObject.GetComponent<FreeBlock>().level != level)
        {
            FreeBlock otherBlock = manager.blocks[gridX, gridY - 1].gameObject.GetComponent<FreeBlock>();

            if (transform.position.x <= otherBlock.transform.position.x + bounds.size.x - 0.1f)
            {
                transform.position = new Vector3(otherBlock.transform.position.x + bounds.size.x - 0.1f,
                                               transform.position.y, 0f);
            }
        }

        //�Ʒ��� ��
        if (manager.blocks[gridX + 1, gridY] != null && manager.blocks[gridX + 1, gridY] != gameObject &&
            manager.blocks[gridX + 1, gridY].gameObject.GetComponent<FreeBlock>().level != level)
        {
            FreeBlock otherBlock = manager.blocks[gridX + 1, gridY].gameObject.GetComponent<FreeBlock>();

            if (transform.position.y <= otherBlock.transform.position.y + bounds.size.y - 0.1f)
            {
                transform.position =
                    new Vector3(transform.position.x, otherBlock.transform.position.y + bounds.size.y - 0.1f, 0f);
            }
        }

        //������ ��
        if (manager.blocks[gridX, gridY + 1] != null && manager.blocks[gridX, gridY + 1] != gameObject &&
            manager.blocks[gridX, gridY + 1].gameObject.GetComponent<FreeBlock>().level != level)
        {
            FreeBlock otherBlock = manager.blocks[gridX, gridY + 1].gameObject.GetComponent<FreeBlock>();

            if (transform.position.x >= otherBlock.transform.position.x - bounds.size.x + 0.1f)
            {
                transform.position = new Vector3(otherBlock.transform.position.x - bounds.size.x + 0.1f,
                                               transform.position.y, 0f);
            }
        }

        //���� ��
        if (manager.blocks[gridX - 1, gridY] != null && manager.blocks[gridX - 1, gridY] != gameObject &&
            manager.blocks[gridX - 1, gridY].gameObject.GetComponent<FreeBlock>().level != level)
        {
            FreeBlock otherBlock = manager.blocks[gridX - 1, gridY].gameObject.GetComponent<FreeBlock>();

            if (transform.position.y >= otherBlock.transform.position.y - bounds.size.y + 0.1f)
            {
                transform.position =
                    new Vector3(transform.position.x, otherBlock.transform.position.y - bounds.size.y + 0.1f, 0f);
            }
        }
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

                manager.combo = 0;

                StartCoroutine(GravityRoutine());  //�߷� �ִϸ��̼�

                break;
            }

            else if (manager.blocks[i + 1, newY] != null && manager.blocks[i, newY] == gameObject && //�� ĭ��null�� �ƴϰ� i�� �ڱ��ڽ��̸鼭
                manager.blocks[i + 1, newY].gameObject.GetComponent<FreeBlock>().level != level)  //�� �� ������ �ٸ� �� (������ ���������ϱ⶧��
            {
                transform.position = new Vector3(gameBoard.blockGridPos[i, newY].x, gameBoard.blockGridPos[i, newY].y, 0);

                break;
            }

            else if (manager.blocks[i - 1, newY] == null && manager.blocks[i, newY] != gameObject && //�� ĭ�� null�̸鼭 �ڽ��� �ƴϰ�
                     manager.blocks[i, newY].gameObject.GetComponent<FreeBlock>().level == level)       //������ ���� ��
            {
                FreeBlock other = manager.blocks[i, newY].gameObject.GetComponent<FreeBlock>();

                newX = i;

                manager.blocks[gridX, gridY] = null;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());

                //�� �����
                DropMerge();

                //��� ������
                other.DropLevelUp();

                break;
            }

            else if (manager.blocks[i, newY] == gameObject &&  //
                     manager.blocks[i + 1, newY].gameObject.GetComponent<FreeBlock>().level == level)           //�ڽ��� �ƴϸ鼭 ���� �����϶�
            {
                FreeBlock other = manager.blocks[i + 1, newY].gameObject.GetComponent<FreeBlock>();

                newX = i + 1;

                manager.blocks[gridX, gridY] = null;
                gridX = newX;
                gridY = newY;

                StartCoroutine(GravityRoutine());

                //�� �����
                DropMerge();

                //��� ������
                other.DropLevelUp();

                break;
            }
        }

        if (gridX == 1)
            manager.isOver = true;
    }

    void DropMerge() //���� �Լ�
    {
        isMerge = true;

        box.enabled = false;

        StartCoroutine(DropMergeRoutine()); //�ִϸ��̼� �ֱ� ���� �ڷ�ƾ
    }

    IEnumerator DropMergeRoutine() //���� �ִϸ��̼�
    {
        yield return new WaitForSeconds(0.2f);

        isMerge = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void DropLevelUp() //�������� ���� �Լ�
    {
        isMerge = true;

        StartCoroutine(DropLevelUpRoutine());
    }

    IEnumerator DropLevelUpRoutine() //�ִϸ��̼� ��ٸ���
    {
        yield return new WaitForSeconds(0.2f);

        EffectPlay();
        GetScore();

        //�޺����
        manager.combo++;
        manager.comboText.text = manager.combo.ToString();

        if (manager.combo >= 3)
        {
            blockScore = blockScore + (int)(blockScore * 0.1);
        }

        //���� ���
        manager.scoreText.text = (manager.score += blockScore).ToString();

        //����+
        anim.SetInteger("Level", level + 1);

        level++;

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

    void EffectPlay()
    {
        particle.transform.position = transform.position;
        particle.transform.localScale = transform.localScale;
        particle.Play();
    }
}
