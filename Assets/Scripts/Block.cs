using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private Sprite fullXP;
    [SerializeField] private Sprite damageXP;

    [SerializeField]private int primaryXP = 2;
    private int XP;
    
    void Start()
    {
        ResetSettings();
    }

    public void ResetSettings()
    {
        XP = primaryXP;
        spriteRender.sprite = fullXP;
    }

    public void RegistredDamage(int damage)
    {
        XP -= damage;
        spriteRender.sprite = damageXP;

        if(XP <= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
