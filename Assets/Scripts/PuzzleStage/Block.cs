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
                    manager.blocks[i, j].gameObject.GetComponent<Block>().level == level)
                {
                    Block other = manager.blocks[i, j].gameObject.GetComponent<Block>();

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

            else if (manager.blocks[i + 1, newY] != null && manager.blocks[i, newY] == gameObject && //�� ĭ��null�� �ƴϰ� i�� �ڱ��ڽ��̸鼭
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
                DropMerge();

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
                DropMerge();

                //��� ������
                other.DropLevelUp();

                break;
            }
        }
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

        if (!tuto.tuto_merge)
        {
            tuto.TutoTextLast();
            tuto.tuto_merge = true;
        }

        StartCoroutine(DropLevelUpRoutine());
    }

    IEnumerator DropLevelUpRoutine() //�ִϸ��̼� ��ٸ���
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
