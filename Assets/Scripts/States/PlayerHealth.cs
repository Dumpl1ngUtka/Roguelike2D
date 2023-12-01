using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Armor _armor;
    private PlayerParameters _parameters;
    private Player _player;
    private float _healthMax;
    private float _health;
    private float _timeWithoutDamage;
    public Action<float> OnHealthChange;
    public Action<float> OnArmorChange;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _parameters = _player.Parameters;
        _healthMax = _parameters.Health;
        _health = _healthMax;
    }
    public void TakeDamage(float damage)
    {
        _timeWithoutDamage = 0;
        if (_armor != null)
            damage = _armor.ConvertDamage(damage);
        _health -= damage;

        OnHealthChange(_health / _healthMax);
        OnArmorChange(_armor.Strength / _armor.MaxStrength);

        if (_health < 0)
            Debug.Log("game over");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitBox = other.GetComponent<EnemyHitBox>();
        if (hitBox != null && _player.CurrentCondition.IsDamageable)
            TakeDamage(hitBox.Damage);
    }

    private void Update()
    {
        _timeWithoutDamage += Time.deltaTime;
        if (_timeWithoutDamage >= _armor.TimeToStrengthRestoration)
        {
            OnArmorChange(_armor.Strength / _armor.MaxStrength);
            _armor.ExecuteStrengthRestoration(_armor.RecoveryPercentageBySecond);
        }
    }
}
