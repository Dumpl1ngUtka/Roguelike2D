using UnityEngine;

[CreateAssetMenu(menuName = "Item/Armor/LightArmor")]
public class LightArmor : Armor
{
    [SerializeField] private PassiveEffects.PassiveEffect a;
    public override float ConvertDamage(float defaultDamage)
    {
        defaultDamage -= defaultDamage * BlockedDamagePercentage / 100;
        float convertedDamage = defaultDamage - Strength;
        if (convertedDamage < 0)
            convertedDamage = 0;
        Strength -= defaultDamage;
        Strength = Mathf.Clamp(Strength, 0, MaxStrength);
        return convertedDamage;
    }
}
