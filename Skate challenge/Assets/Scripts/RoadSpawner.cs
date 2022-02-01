using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Генерация уровня
 */
public class RoadSpawner : MonoBehaviour
{
    public Transform PlayerTransf;
    
    public GameObject[] RoadBlockPrefabs;
    public GameObject StartBlock;
    public GameObject FinishBlock;
    
    float blockLenght = 0;
    float startBlockXPos= 0;
    public int blockCount = 7;

    List<GameObject> CurrentBlocks = new List<GameObject>();
    private Vector3 startPlayerPos;
    void Start()
    {
        startBlockXPos = PlayerTransf.position.x+12;
        blockLenght = 30;
        StartGame();
    }

    //Создаём уровень
    public void StartGame()
    {
        foreach (var go in CurrentBlocks)
            Destroy(go);
        
        CurrentBlocks.Clear();
        
        SpawnStartBlok();
        
        for (int i = 0; i < blockCount; i++)
        {
            SpawnBlock();
        }
        SpawnFinishBlock();
    }

    //Создаём стартовую дорогу (префаб) - всегда первый
    public void SpawnStartBlok()
    {
        GameObject startBlock = Instantiate(StartBlock);
        CurrentBlocks.Add(startBlock);
    }
    //Создаём финишную дорогу(префаб) - всегда последний
    void SpawnFinishBlock()
    {
        GameObject finishBlock = Instantiate(FinishBlock);
        finishBlock.transform.position = new Vector3(blockLenght*(blockCount+1), 0, 0);
        CurrentBlocks.Add(finishBlock);
    }
    
    //рандомно создаём блоки от установленного количества
    void SpawnBlock()
    {
        GameObject block = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], transform);
        Vector3 blockPos;
        if (CurrentBlocks.Count > 0)
            blockPos = CurrentBlocks[CurrentBlocks.Count - 1].transform.position + new Vector3(blockLenght, 0, 0);
        else
            blockPos = new Vector3(startBlockXPos, 0, 0);
        
        block.transform.position = blockPos;
        CurrentBlocks.Add(block);
    }
}
