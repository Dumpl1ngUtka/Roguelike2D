using UnityEngine;

[CreateAssetMenu(menuName = "Config/Player Parameters")]
public class PlayerParameters : ScriptableObject
{
    [Header("Health")]
    [Min(0)] public float Health;//100
    [Min(0)] public float Stamina;//100
    [Min(0)] public float Strength;//100
    [Min(0)] public float Dexterity;//100

    [Header("MOVEMENT")]
    public float MaxSpeed;//7
    public float Acceleration;//120
    public float GroundDeceleration;//60
    public float AirDeceleration;//30
    [Range(0f, 0.5f)] public float GrounderDistance;//0.05f

    [Header("JUMP")]
    public float JumpPower;//36
    public float MaxFallSpeed;//40
    public float FallAcceleration;//110
    public float CoyoteTime;//0.15
    public float JumpBuffer;//0.2
    public float FastFallMaxSpeedModifier;//2

    [Header("DODGE")]
    public float DodgeTime;//0.2
    public float DodgeSpeed;//20

    [Header("Block")]
    public float BlockMaxSpeedModifier;//0.3
}
