using UnityEngine;

public class BodyPartManager : MonoBehaviour {

    private Killable killable;
    private uint swingTracker = 0;

    private void Awake()
    {
        killable = GetComponent<Killable>();
        if (killable == null)
        {
            Debug.Log(gameObject.name + " has a bodypartmanager but isnt killable. Disabling...");
            this.enabled = false;
        }
    }

    public bool Bodypart_Hit(uint swingNum, int dmg)
    {
        if (swingNum == swingTracker) return false;

        swingTracker = swingNum;
        killable.Take_Damage(dmg);
        return true;
    }
}
