using UnityEngine;

/* Alex Pantuck
 * A list of of Defense types
 * and a method to calculate how
 * defense decreases damage.
 * 
 * I'm sorta cheating because I represent
 * the defense types as DamageTypes.
 */

public class DefenseTypes : MonoBehaviour
{
    public DamageTypes Defences;
    public int Poise;

    public static int Calc_Damage(DefenseTypes defense, DamageTypes damage)
    {
        int sum = 1;

        int[] dmg = damage.Get_Damages();
        int[] def = defense.Defences.Get_Damages();
        for (int i = 0; i < dmg.Length; i++)
        {
            int temp = dmg[i] - Dmg_Reduction(def[i]);
            sum += (temp > 0) ? temp : 0;
        }

        return sum;
    }

    private static int Dmg_Reduction(int defense)
    {
        return (int)(3 * (Mathf.Pow(defense, 2 / 3)));
    }
}
