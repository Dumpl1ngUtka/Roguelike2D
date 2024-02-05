using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    private Armor _armor;
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
        _inventory.InventoryChanged += SetArmor;
        SetArmor();
    }

    private void SetArmor()
    {
        _armor = _inventory.Armors[0];
        if (_armor == null)
            OnArmorChange?.Invoke(0);
        else
            OnArmorChange?.Invoke(_armor.Strength / _armor.MaxStrength);
    }

    public void TakeDamage(float damage)
    {
        _timeWithoutDamage = 0;
        if (_armor != null)
        {
            damage = _armor.ConvertDamage(damage);
            OnArmorChange?.Invoke(_armor.Strength / _armor.MaxStrength);
        }
        _health -= damage;

        OnHealthChange?.Invoke(_health / _healthMax);

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
        if (_armor != null && _timeWithoutDamage >= _armor.TimeToStrengthRestoration 
            && (_armor.Strength / _armor.MaxStrength) < 1)
        {
            _armor.ExecuteStrengthRestoration(_armor.RecoveryPercentageBySecond);
            OnArmorChange?.Invoke(_armor.Strength / _armor.MaxStrength);
        }
    }
}
