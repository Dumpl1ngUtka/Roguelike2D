using System;
using System.Collections;
using UnityEngine;

public class PlayerItemTaker : MonoBehaviour
{
    [SerializeField] private LayerMask _itemsLayerMask;
    private Player _player;
    private PlayerInputSystem _playerInputSystem;
    private const float _takeDistance = 3;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerInputSystem = _player.InputSystem;
        _playerInputSystem.Movement.TakeItem.performed += ctx => TakeItem();
    }

    private void TakeItem()
    {
        var items = Physics2D.CircleCastAll(transform.position, _takeDistance, Vector2.zero, _takeDistance, _itemsLayerMask);
    }
}
