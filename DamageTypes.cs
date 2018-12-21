/* Alex Pantuck
 * List of damage types
 */

[System.Serializable]
public class DamageTypes
{
    public int physical;
    public int fire;
    public int electric;
    public int water;

    public int[] Get_Damages()
    {
        int[] temp = { physical, fire, electric, water };
        return temp;
    }
}
