using UnityEngine;

public class Player : Combatant
{
    const string EXP_TAG = "_Exp";
    const string DIED_TAG = "_IsDead";
    public static string DiedTag
    {
        get { return DIED_TAG; }
    }

    [Header("Default Weapon Settings")]
    [SerializeField] protected WeaponItem defaultWeapon;
    public WeaponItem DefaultWeapon { get; set; }
    [SerializeField] protected Transform wpnHand;
    protected WeaponItem curWeapon;
    public RuntimeAnimatorController AnimController
    {
        get
        {
            if (animator.runtimeAnimatorController == null) print("wtf");
            return animator.runtimeAnimatorController;
        }
        set
        {
            animator.runtimeAnimatorController = value;
        }
    }
    public Transform WeaponHand
    {
        get { return wpnHand; }
    }

    [Header("Player Characteristics")]
    [SerializeField] private string playerName;
    [SerializeField] private int level;
    [SerializeField] private int exp;
    

    public delegate void PlayerDeath(Player player);
    public static event PlayerDeath OnPlayerDiedHandler;
    public delegate void ExpChanged();
    public static event ExpChanged ExpChangedHandler;

    public override void Awake()
    {
        saveName = SaveManager.getPlayerSaveName();
        base.Awake();
    }

    public virtual void Start()
    {
        if (wpnHand == null)
        {
            Debug.Log(gameObject.name + " has no defined weapn hand even though it is a Combatant.");
            return;
        }

        // Load last equipped weapon (or default)
        int wpnCode = PlayerPrefs.GetInt(saveName + WeaponItem.WpnTag, defaultWeapon.GetHashCode());
        curWeapon = Inventory.Get_At(wpnCode) as WeaponItem;
        try
        {
            curWeapon.Equip(wpnHand);
        }
        catch (System.NullReferenceException)
        {
            Debug.Log(gameObject.name + " could not equip item");
        }

        exp = PlayerPrefs.GetInt(saveName + EXP_TAG, 0);
        Enemy.OnEnemyDiedHandler += OnEnemyDied;

    }

    public virtual void Update()
    {
        // player controls here
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    void OnEnemyDied(Enemy enemy)
    {
        exp += enemy.EXP;
        if (ExpChangedHandler != null) ExpChangedHandler();
    }

    public override void Die()
    {
        base.Die();
        PlayerPrefs.SetInt(saveName + EXP_TAG, 0);
        PlayerPrefs.SetInt(saveName + DIED_TAG, TRUE);
        exp = 0;

        if (ExpChangedHandler != null) ExpChangedHandler();
        if (OnPlayerDiedHandler != null) OnPlayerDiedHandler(this);
    }

}
