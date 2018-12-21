using System.Collections;
using UnityEngine;

/* Alex Pantuck
 * Enemy
 * Broadest enemy implementation 
 */
public class Enemy : Combatant
{
    [SerializeField] private uint aggroDistance;
    [SerializeField] private uint unaggroDistance;
    [SerializeField] private float reactionTime = 0.2f;

    [SerializeField] private int exp;
    public int EXP { get; set; }

    public delegate void EnemyDeath(Enemy enemy);
    public static event EnemyDeath OnEnemyDiedHandler;

    protected Vector3 startPos;
    protected int isDead;

    public override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        isDead = PlayerPrefs.GetInt(saveName + "_IsDead", 0);
        if (isDead == 0) Destroy(gameObject);
    }

    public virtual void Update()
    {
        StartCoroutine(Take_Action());
    }

    IEnumerator Take_Action()
    {
        yield return new WaitForSeconds(reactionTime);
    }

    public override void Die()
    {
        base.Die();
        PlayerPrefs.SetInt(saveName + "_IsDead", 1);
        // Signal OnEnemyDied event if anyone is subscribed
        if (OnEnemyDiedHandler != null) OnEnemyDiedHandler(this);
    }
}
