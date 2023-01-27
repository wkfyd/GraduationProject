using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Block lastBlock;
    public GameObject BlockPrefab;
    public Transform BlockGroup;

    public int maxLevel;
    void Start()
    {
        NextBlock();
    }
    Block GetBlock()
    {
        GameObject instant = Instantiate(BlockPrefab, BlockGroup);
        Block instantBlock = instant.GetComponent<Block>();
        return instantBlock;
    }

    void NextBlock()
    {
        Block newBlock = GetBlock();
        lastBlock = newBlock;
        lastBlock.manager = this;
        lastBlock.level = Random.Range(0, maxLevel);
        lastBlock.gameObject.SetActive(true);

        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        yield return new WaitForSeconds(2.5f);

        NextBlock();
    }
}
