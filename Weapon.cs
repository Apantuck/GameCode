using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AnimatorOverrideController weaponAnimations;
    public DamageTypes dmg;
    public int damage;
    public int poiseDmg;
    protected uint swingNum = 0;
    public uint SwingNum
    {
        get
        {
            return swingNum;
        }
    }

    protected Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.Log(gameObject.name + " is a weapon with no collider. Disabling...");
            this.enabled = false;
        }
        col.enabled = false;
    }

    public void Set_Collider(bool state)
    {
        col.enabled = state;
    }

    public void Add_Swing()
    {
        swingNum++;
    }
}