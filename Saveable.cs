using UnityEngine;

/* Alex Pantuck
 * Abstract base class for all objects which require
 * persistent attributes
 */

public abstract class Saveable : MonoBehaviour
{
    // For loading bools saved as ints.
    // effecctively an enum but this way you dont
    // have to cast it to int or index from the enum
    protected const int FALSE = 0, TRUE = 1;

    protected string saveName;
    public string SaveName
    {
        get { return saveName; }
    }

    public virtual void Awake()
    {
        saveName = SaveManager.getSaveName(gameObject);
    }
}
