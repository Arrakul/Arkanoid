using System;
using System.Collections.Generic;
using Arkanoid.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockController : Singleton<BlockController>
{
    public int numberBlocks;
    [HideInInspector ]public int countActiveBlocks;

    public GameObject blocks;
    public SpriteBlock[] spriteBlocks;
    private List<GameObject> _poolBlocks;

    [Serializable]
    public struct SpriteBlock
    {
        public Sprite fullXpSprite;
        public Sprite damageXpSprite;
    }

    private void Awake()
    {
        CreateBlocks();
    }

    private void CreateBlocks()
    {
        _poolBlocks = new List<GameObject>();
        _poolBlocks = PoolManager.Instance.GetObjects(blocks, numberBlocks, gameObject.transform);
    }

    public Sprite[] GetSprite(int index)
    {
        Sprite[] sprites = new Sprite[2];

        sprites[0] = spriteBlocks[index].fullXpSprite;
        sprites[1] = spriteBlocks[index].damageXpSprite;

        return sprites;
    }
    
    public Block GetBlock()
    {
        foreach (var obj in _poolBlocks)
        {
            if (!obj.activeSelf)
            {
                var block = obj.GetComponent<Block>();
                block.boxCollider2D = block.GetComponent<BoxCollider2D>();
                AnimationController.Instance.AnimationPulsation(block.transform);
                    
                obj.SetActive(true);
                return block;
            }
        }

        return null;
    }

    public void HideAllBlocks()
    {
        foreach (var block in _poolBlocks)
        {
            block.transform.SetParent(gameObject.transform);
            //block.GetComponent<Block>().ResetSettings();
            block.SetActive(false);
        }
    }
    
    public void CheckWin()
    {
        --countActiveBlocks;

        if (countActiveBlocks <= 0)
        {
            GameController.Instance.WinGame();
        }
    }
}
