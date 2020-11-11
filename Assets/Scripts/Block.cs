using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private Sprite fullXp;
    [SerializeField] private Sprite damageXp;

    [SerializeField]private int primaryXp = 2;
    private int Xp;
    
    void Start()
    {
        ResetSettings();
    }

    public void ResetSettings()
    {
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
            //Destroy(gameObject);
        }
    }
}
