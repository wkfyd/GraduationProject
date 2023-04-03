using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject BlockPrefab;
    public Block lastBlock;
    GameBoard gameBoard;

    public int maxLevel;

    void Awake()
    {
        gameBoard = new GameBoard();
    }
    void Start()
    {
        Spawn();
    }
    Block GetBlock(int i)  //블럭생성
    {     
        //오브젝트 생성
        GameObject instant = Instantiate(BlockPrefab, gameBoard.blockGridPos[0,i], Quaternion.identity);
        Block instantBlock = instant.GetComponent<Block>(); //반환값을 Block하기위한 변환
        return instantBlock;
    }

    void NextBlock(int i) //다음 블럭
    {
        Block newBlock = GetBlock(i); //블럭함수 컨트롤하기위한 변수선언
        lastBlock = newBlock;
        lastBlock.manager = this;
        lastBlock.level = Random.Range(0, maxLevel); //maxLevel값은 포함 안 됨
        lastBlock.gameObject.SetActive(true);

        StartCoroutine(WaitNext(i));
    }

    /*void test()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        for (int j = 0; j < blocks.Length; j++)
        {
            Vector3 newPos = blocks[j].transform.position;
            newPos.y += 1.12f;
            blocks[j].transform.position = newPos;
        }
    }*/

    IEnumerator WaitNext(int i)
    {
        yield return new WaitForSeconds(4f);

        NextBlock(i);
    }
    void StartSpawn()
    {

    }

    void Spawn()
    {
        for (int i = 0; i < 6; i++)
            {
                NextBlock(i);
            }
    }
}
