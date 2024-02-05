using UnityEngine;

[CreateAssetMenu(menuName = "Item/Armor/HeavyArmor")]
public class HeavyArmor : Armor
{
    public override float ConvertDamage(float defaultDamage)
    {
        defaultDamage -= defaultDamage * BlockedDamagePercentage / 100;
        float convertedDamage;
        convertedDamage = defaultDamage - (defaultDamage * Strength / MaxStrength);
        Strength -= defaultDamage;
        Strength = Mathf.Clamp(Strength, 0, MaxStrength);
        return convertedDamage;
    }
}
