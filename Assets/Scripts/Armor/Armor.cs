using UnityEngine;

public abstract class Armor : Item
{
    [Range(0, 100)] public float BlockedDamagePercentage;
    public float MaxStrength;
    public float TimeToStrengthRestoration;
    [Range(0,100)] public float RecoveryPercentageBySecond;
    public float Strength { get; protected set; }

    private void OnEnable()
    {
        Strength = MaxStrength;
    }

    public void ExecuteStrengthRestoration(float recoveryPercentage)
    {
        Strength += MaxStrength * recoveryPercentage / 100 * Time.deltaTime;
        Strength = Mathf.Clamp(Strength, 0, MaxStrength);
    }
    public abstract float ConvertDamage(float defaultDamage);
}
