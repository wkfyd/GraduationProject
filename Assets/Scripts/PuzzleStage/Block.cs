using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public GameManager manager;
    public TutorialManager tuto;
    public EnemyManager enemyManager;

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
        if (!manager.gameWin)
        {
            if (!enemyManager.isEnemy_Sp)
            {
                if (!enemyManager.isZeno_Sp)
                {
                    downGrid_X = gridX;
                    downGrid_Y = gridY;

                    levelUpOnce = true;
                    select = true;
                }
            }
        }
    }

    public void OnMouseDrag()
    {
        if (!manager.gameWin)
        {
            if (!enemyManager.isEnemy_Sp)
            {
                if (!enemyManager.isZeno_Sp)
                {
                    mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //������ǥ ���콺 ��ġ
                    mouse_Pos.z = 0;

                    transform.position = Vector3.Lerp(transform.position, mouse_Pos, 0.2f);

                    //���� ������ �ε�����
                    BumpBlock();

                    //������ ������ �� ������
                    outOfRangeBoard();

                    //��ǥ���� �Լ� + �巡�׸���
                    DragChangedGridPos();
                }
            }
        }
    }

    public void OnMouseUp()
    {
        if (!manager.gameWin)
        {
            if (!enemyManager.isEnemy_Sp)
            {
                if (!enemyManager.isZeno_Sp)
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
            }
        }
    }

    //�� �����
    int GetDamage()
    {
        switch (level)
        {
            //A
            case 0:
                blockDamage = 4;
                break;

            //B
            case 1:
                blockDamage = 8;
                break;

            //C
            case 2:
                blockDamage = 16;
                break;

            //D
            case 3:
                blockDamage = 32;
                break;

            //E
            case 4:
                blockDamage = 64;
                break;

            //F
            case 5:
                blockDamage = 120;
                break;

            //G
            case 6:
                blockDamage = 240;
                break;

            //H
            case 7:
                blockDamage = 520;
                break;

            //I
            case 8:
                blockDamage = 2080;
                break;

            //J
            case 9:
                blockDamage = 5000;
                break;

            //K
            case 10:
                blockDamage = 15000;
                break;

            //L
            case 11:
                blockDamage = 20000;
                break;

            //M
            case 12:
                blockDamage = 25000;
                break;

            //N
            case 13:
                blockDamage = 30000;
                break;

            //O
            case 14:
                blockDamage = 35000;
                break;

            //P
            case 15:
                blockDamage = 40000;
                break;

            //Q
            case 16:
                blockDamage = 45000;
                break;

            //R
            case 17:
                blockDamage = 50000;
                break;

            //S
            case 18:
                blockDamage = 55000;
                break;

            //T
            case 19:
                blockDamage = 60000;
                break;

            //U
            case 20:
                blockDamage = 65000;
                break;

            //V
            case 21:
                blockDamage = 70000;
                break;

            //W
            case 22:
                blockDamage = 75000;
                break;

            //X
            case 23:
                blockDamage = 999999;
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
        manager.PlaySound(manager.hitClip, 0.8f);

        //�޺�
        manager.comboAtk++;

        if (manager.comboAtk >= 3)
        {
            blockDamage = blockDamage + (int)(blockDamage * 0.1);
        }

        //�޺� �ؽ�Ʈ ���� �� ���
        if (manager.comboAtk != 0)
            enemyManager.comboText.gameObject.SetActive(true);

        enemyManager.comboText.text = manager.comboAtk.ToString() + "combo";

        //������ ���
        //�Ƹ����� �ڷ��� ����
        if (enemyManager.isAristo_Sp &&
            (enemyManager.aristo_Sp_NomTurn >= 1 && enemyManager.aristo_Sp_NomTurn <= 3))
        {
            enemyManager.enemy_DamageHP = enemyManager.enemy_Health;

            //����� �ؽ�Ʈ ���� �� ���
            enemyManager.damagePrefabs.GetComponent<TextMeshProUGUI>().text = "0";
            Instantiate(enemyManager.damagePrefabs, enemyManager.enemyDamageTextGroup);

            enemyManager.aristo_Sp_NomTurn--;

            enemyManager.PlayerAtk();

            if (enemyManager.aristo_Sp_NomTurn == 0)
                enemyManager.isAristo_Sp = false;
        }

        //������ν� �����
        else if (enemyManager.isEpicuru_Sp &&
                (enemyManager.epicuru_Sp_NomTurn >= 1 && enemyManager.epicuru_Sp_NomTurn <= 3))
        {
            float x = 2 / 3f;
            enemyManager.enemy_DamageHP -= (float)blockDamage * x;

            enemyManager.epicuru_Sp_NomTurn--;

            if (enemyManager.epicuru_Sp_NomTurn == 0)
                enemyManager.isEpicuru_Sp = false;
        }

        else
            enemyManager.enemy_DamageHP -= blockDamage;

        //�� ���
        manager.curt_turns++;

        //����+
        level++;

        anim.SetInteger("Level", level);

        if (!tuto.tuto_merge)
        {
            manager.TutoTextLast();
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

                manager.comboAtk = 0;
                enemyManager.comboText.gameObject.SetActive(false);

                StartCoroutine(GravityRoutine());  //�߷� �ִϸ��̼�

                break;
            }

            else if (manager.blocks[i + 1, newY] != null && manager.blocks[i, newY] == gameObject && //�� ĭ��null�� �ƴϰ� i�� �ڱ��ڽ��̸鼭
                manager.blocks[i + 1, newY].gameObject.GetComponent<Block>().level != level)  //�� �� ������ �ٸ� �� (������ ���������ϱ⶧��
            {
                transform.position = new Vector3(gameBoard.blockGridPos[i, newY].x, gameBoard.blockGridPos[i, newY].y, 0);

                manager.comboAtk = 0;
                enemyManager.comboText.gameObject.SetActive(false);

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

        if (!tuto.tuto_merge)
        {
            manager.TutoTextLast();
            tuto.tuto_merge = true;
        }

        StartCoroutine(DropLevelUpRoutine());
    }

    IEnumerator DropLevelUpRoutine() //�ִϸ��̼� ��ٸ���
    {
        yield return new WaitForSeconds(0.2f);

        EffectPlay();
        GetDamage();
        manager.PlaySound(manager.hitClip, 0.8f);

        //�޺�
        manager.comboAtk++;

        if (manager.comboAtk >= 3)
        {
            blockDamage = blockDamage + (int)(blockDamage * 0.1);
        }

        //�޺� �ؽ�Ʈ ���� �� ���
        if (manager.comboAtk != 0)
            enemyManager.comboText.gameObject.SetActive(true);

        enemyManager.comboText.text = manager.comboAtk.ToString() + "combo";

        //������ ���
        //�Ƹ����� �ڷ��� ����
        if (enemyManager.isAristo_Sp &&
            (enemyManager.aristo_Sp_NomTurn >= 1 && enemyManager.aristo_Sp_NomTurn <= 3))
        {
            enemyManager.enemy_DamageHP = enemyManager.enemy_Health;

            //����� �ؽ�Ʈ ���� �� ���
            enemyManager.damagePrefabs.GetComponent<TextMeshProUGUI>().text = "0";
            Instantiate(enemyManager.damagePrefabs, enemyManager.enemyDamageTextGroup);

            enemyManager.aristo_Sp_NomTurn--;

            enemyManager.PlayerAtk();

            if (enemyManager.aristo_Sp_NomTurn == 0)
                enemyManager.isAristo_Sp = false;
        }

        else if (enemyManager.isEpicuru_Sp &&
                (enemyManager.epicuru_Sp_NomTurn >= 1 && enemyManager.epicuru_Sp_NomTurn <= 3))
        {
            float x = 2 / 3f;
            enemyManager.enemy_DamageHP -= (float)blockDamage * x;

            enemyManager.epicuru_Sp_NomTurn--;

            if (enemyManager.epicuru_Sp_NomTurn == 0)
                enemyManager.isEpicuru_Sp = false;
        }

        else
            enemyManager.enemy_DamageHP -= blockDamage;

        //�� ���
        manager.curt_turns++;

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
