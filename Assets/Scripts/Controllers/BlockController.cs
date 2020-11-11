using System;
using System.Collections;
using System.Collections.Generic;
using Arkanoid.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockController : Singleton<BlockController>
{
    public int numberBlocks;
    public float xMin, xMax, yMin, yMax;
    public int minDrop, maxDrop;
    public float timePause;
        
    public Transform blockStorage;
    public GameObject[] blocks;
    private List<GameObject>[] _poolBlocks;

    private void Awake()
    {
        CreateBlocks();
    }

    private void CreateBlocks()
    {
        _poolBlocks = new List<GameObject>[blocks.Length];
        
        for (int i = 0; i < blocks.Length; i++)
        {
            var blockStorageItem = new GameObject {name = blocks[i].name + "Storage"};
            blockStorageItem.transform.SetParent(blockStorage);

            _poolBlocks[i] = PoolManager.Instance.GetObjects(blocks[i], numberBlocks, blockStorageItem.transform);
        }
    }

    public void SpaunBlock()
    {
        int quantityPerPass = Random.Range(minDrop, maxDrop);

        for (int i = 0; i < quantityPerPass; i++)
        {
            int indexBlock = Random.Range(0, blocks.Length);

            foreach (var block in _poolBlocks[indexBlock])
            {
                if (!block.activeSelf)
                {
                    block.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
                    block.GetComponent<Block>().ResetSettings();
                    block.SetActive(true);
                    
                    AnimationController.Instance.AnimationPulsation(block.transform);
                    break;
                }
            }
        }
    }

    public void HideAllBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            foreach (var block in _poolBlocks[i])
            {
                if (block.activeSelf)
                {
                    block.SetActive(false);
                }
            }
        }
    }
}
