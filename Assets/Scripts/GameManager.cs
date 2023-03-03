using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Snap snap;
    public Block lastBlock;
    public GameObject BlockPrefab;
    public Transform BlockGroup;

    public int maxLevel;

    void Awake()
    {
    }
    void Start()
    {
        snap = new Snap();
        NextBlock(0);
        NextBlock(1);
        NextBlock(2);
        NextBlock(3);
        NextBlock(4);
        NextBlock(5);
    }
    Block GetBlock(int i)        //블럭생성
    {     
        GameObject instant = Instantiate(BlockPrefab, snap.gridPos[i], Quaternion.identity); //오브젝트 생성
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

        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        yield return new WaitForSeconds(10f);

        NextBlock(0);
        NextBlock(1);
        NextBlock(2);
        NextBlock(3);
        NextBlock(4);
        NextBlock(5);
    }
}
