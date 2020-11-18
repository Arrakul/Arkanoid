using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRender = null;
    [SerializeField] public BoxCollider2D boxCollider2D = null;
    [SerializeField] public Sprite fullXp;
    [SerializeField] public Sprite damageXp;
    [SerializeField] public EBlockType type = EBlockType.Empty;

    [SerializeField] private int primaryXp = 2;
    private int Xp;

    public int x = 0;
    public int y = 0;
    
    public enum EBlockType
    {
        Yellow = 0,
        Red,
        Blue,
        Pink,
        Orange,
        Green,
        Empty
    }
    
    void Start()
    {
        //ResetSettings();
    }

    public void InstallSettings()
    {
        int index = 0;
        switch (type)
        {
            case EBlockType.Yellow: index = 0;
                break;
            case EBlockType.Red: index = 1;
                break;
            case EBlockType.Blue: index = 2;
                break;
            case EBlockType.Pink: index = 3;
                break;
            case EBlockType.Orange: index = 4;
                break;
            case EBlockType.Green: index = 5;
                break;
        }
        
        Sprite[] sprites = BlockController.Instance.GetSprite(index);

        fullXp = sprites[0];
        damageXp = sprites[1];

        Xp = primaryXp;
        spriteRender.sprite = fullXp;
    }

    public void RegistredDamage(int damage)
    {
        Xp -= damage;
        spriteRender.sprite = damageXp;

        if(Xp <= 0)
        {
            gameObject.SetActive(false);
            spriteRender.sprite = null;
            BlockController.Instance.CheckWin();;
            //Destroy(gameObject);
        }
    }
}
