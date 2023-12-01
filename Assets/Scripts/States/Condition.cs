using UnityEngine;

[CreateAssetMenu(menuName = "Config/Condition")]
public class Condition: ScriptableObject
{
    public bool IsCanControlCharacter;
    public bool IsUsedGravity;
    public bool IsDamageable;
}
