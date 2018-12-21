using UnityEngine;

/* Alex Pantuck
 * Killable abstract class
 * Base interface for all things that can die or be damaged
 */

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BodyPartManager))]
public class Killable : Saveable
{
    const string DIE_TRIGGER = "Die";
    const string HEALTH_TAG = "_Health";

    [Header("Health Settings")]
    [SerializeField] protected int maxHealth = 100;
    protected int health;

    [Header("Damage Audio")]
    [SerializeField] protected AudioClip[] hitSounds;
    [SerializeField] protected AudioClip[] deathSounds;

    protected Animator animator;
    public Animator Animator { get; set; }
    protected AudioSource speaker;

    public override void Awake()
    {
        if(string.IsNullOrEmpty(saveName)) base.Awake();
        health = PlayerPrefs.GetInt(saveName + HEALTH_TAG, maxHealth);
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.Log(gameObject.name + " is missing an animator, disabling Killable.");
            this.enabled = false;
        }
    }

    public virtual void Take_Damage(int dmg)
    {
        health -= dmg;
        health = (health < 0) ? 0 : health;
        PlayerPrefs.SetInt(saveName + HEALTH_TAG, health);
        if (health == 0)
            Die();
    }

    public virtual void Die()
    {
        animator.SetTrigger(DIE_TRIGGER);
    }
}
