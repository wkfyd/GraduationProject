using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;

    public GameObject[,] blocks = new GameObject[7, 6]; //������Ʈ 2�����迭����,�ʱ�ȭ

    public int maxLevel;
    public bool gameOver;

    GameBoard gameBoard = new GameBoard();
    
    void Awake()
    {

    }
    void Start()
    {
        StartSpawn();
    }

    void Update()
    {
        Debug.Log(gameOver);
    }

    //���۽���
    public void StartSpawn()
    {
        //����������
        GameObject[] destroyBlock = GameObject.FindGameObjectsWithTag("Block");
        
        for (int j=0; j< destroyBlock.Length; j++)
            Destroy(destroyBlock[j]);

        for (int i = 5; i < 7; i++) {
            for (int j = 0; j < 6; j++) {
                blocks[i, j] = null;
            }
        }

        //�� ����
        int x=0, y=0;
        int[] levels = new int[6];

        for (int i = 0; i < 2; i++)
        {
            //��ǥ ����
            while (true) { x = Random.Range(5, 7); y = Random.Range(0, 6); if (blocks[x, y] == null) break; }

            //�� ���� �� ������ǥ�� �� ĭ�� ���� ������ �� ĭ�� ����
            if (x == 5 && blocks[x + 1, y] == null) {
                blocks[x + 1, y] = Instantiate(BlockPrefab, 
                                        gameBoard.blockGridPos[x + 1, y], Quaternion.identity);

                lastBlock = blocks[x + 1, y].GetComponent<Block>();

                lastBlock.gridY = y;
                lastBlock.gridX = x + 1;
            }
            else { 
                blocks[x, y] = Instantiate(BlockPrefab, 
                                        gameBoard.blockGridPos[x, y], Quaternion.identity);

                lastBlock = blocks[x, y].GetComponent<Block>();

                lastBlock.gridY = y;
                lastBlock.gridX = x;
            }

            lastBlock.manager = this;
            lastBlock.level = Random.Range(0, maxLevel); //�������� ���� ����
            
            levels[i] = lastBlock.level; //���� �迭

            //�ּ� �� �� ���� ����
            if (i == 1)
            {
                while (levels[0] != levels[1])
                {
                    levels[1] = Random.Range(0, maxLevel);
                }
                lastBlock.level = levels[1];
            }

            if(i == 3)
            {
                while (levels[2] != levels[3])
                {
                    levels[3] = Random.Range(0, maxLevel);
                }
                lastBlock.level = levels[3];
            }

            lastBlock.gameObject.SetActive(true);
        }
    }

    public void Spawn()
    {

    }
}
