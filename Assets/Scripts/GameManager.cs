using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Block lastBlock;
    public GameObject BlockPrefab;
    public Transform[] BlockGroup;

    public int maxLevel;

    void Start()
    {
        Spawn();
    }
    Block GetBlock(int i)        //블럭생성
    {     
        GameObject instant = Instantiate(BlockPrefab, BlockGroup[i]); //오브젝트 생성
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

    IEnumerator WaitNext(int i)
    {
        yield return new WaitForSeconds(4f);

        NextBlock(i);
    }

    void Spawn()
    {
        NextBlock(0);
        NextBlock(1);
        NextBlock(2);
        NextBlock(3);
        NextBlock(4);
        NextBlock(5);
    }
}
