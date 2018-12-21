using UnityEngine;

/* Alex Pantuck
 * Combatant base class
 * Base interface for all things that participate
 * in combat, be it players, enemies, bosses, etc.
 */

public abstract class Combatant : Killable
{
    const string staggerTrigger = "Stagger";

    [Header("Stamina Settings")]
    [SerializeField] protected int maxStamina = 10000;
    [SerializeField] protected int staminaRegenRate = 10;
    protected int stamina;

    [Header("Poise Settings")]
    [SerializeField] protected int maxPoise = 10000;
    [SerializeField] protected int poiseRegenRate = 10;
    protected int poise;

    public override void Awake()
    {
        base.Awake();
        stamina = maxStamina;
        poise = maxPoise;
    }

    public virtual void LateUpdate()
    {
        Regen_Stamina();
        Regen_Poise();
    }




    public virtual void Attack(string trigger, int stamCost)
    {
        if (stamina - stamCost > 0)
        {
            animator.SetTrigger(trigger);
            Consume_Stamina(stamCost);
        }
    }
    
    public virtual void Consume_Stamina(int stamCost)
    {
        stamina -= stamCost;
        if (stamina < 0) stamina = 0;
    }

    public virtual void Regen_Stamina()
    {
        if (stamina < maxStamina) stamina += Mathf.RoundToInt(staminaRegenRate * Time.deltaTime);
        else if (stamina > maxStamina) stamina = maxStamina;
    }

    public virtual void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;
    }

    public virtual void Take_Damage(int dmg, int poiseDmg)
    {
        Take_Damage(dmg);

        poise -= poiseDmg;
        if (poise <= 0) animator.SetTrigger(staggerTrigger);   
    }

    public virtual void Regen_Poise()
    {
        if (poise < maxPoise) poise += Mathf.RoundToInt(poiseRegenRate * Time.deltaTime);
        else if (poise > maxPoise) poise = maxPoise;
    }
    
}
