using UnityEngine;

[CreateAssetMenu(menuName = "Config/Player Parameters")]
public class PlayerParameters : ScriptableObject
{
    [Header("Health")]
    [Min(0)] public float Health = 100;
    [Min(0)] public float Stamina = 100;
    [Min(0)] public float Strength = 100;
    [Min(0)] public float Dexterity = 100;
}
