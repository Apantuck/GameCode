using UnityEngine;

public class CriticalBodyPart : BodyPart
{
    [SerializeField] private int criticalModifier;

	void Start ()
    {
        dmgMod = criticalModifier;	
	}
}
