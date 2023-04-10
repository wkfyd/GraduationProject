using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;
    public int maxLevel;
    public GameObject[,] blocks = new GameObject[7, 6]; //오브젝트 2차원배열선언,초기화

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
        
    }
    //시작스폰
    public void StartSpawn()
    {
        //기존블럭삭제
        GameObject[] destroyBlock = GameObject.FindGameObjectsWithTag("Block");
        for(int i = 5; i < 7; i++) {
            for(int j = 0; j < 6; j++){
                blocks[i, j] = null;
            }
        }

        for (int j=0; j< destroyBlock.Length; j++)
            Destroy(destroyBlock[j]);

        //블럭 스폰
        int x=0, y=0;
        int[] levels = new int[6];

        for (int i = 0; i < 6; i++)
        {
            //좌표 지정
            while (true) { x = Random.Range(5, 7); y = Random.Range(0, 6); if (blocks[x, y] == null) break; }
            //블럭 생성 및 생성좌표의 밑 칸에 블럭이 없으면 밑 칸에 생성
            if (x == 5 && blocks[x + 1, y] == null) {
                blocks[x + 1, y] = Instantiate(BlockPrefab, 
                                        gameBoard.blockGridPos[x + 1, y], Quaternion.identity);
                lastBlock = blocks[x + 1, y].GetComponent<Block>();
            }
            else { 
                blocks[x, y] = Instantiate(BlockPrefab, 
                                        gameBoard.blockGridPos[x, y], Quaternion.identity);
                lastBlock = blocks[x, y].GetComponent<Block>();
            }

            lastBlock.manager = this;
            lastBlock.level = Random.Range(0, maxLevel); //일정범위 랜덤 레벨
            levels[i] = lastBlock.level; //레벨 배열
            //최소 두 쌍 같은 레벨
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
