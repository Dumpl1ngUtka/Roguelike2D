using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTaker : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo _itemInfo;
    [SerializeField] private TakeItemMenu _takeItemMenu;
    private Player _player;
    private Inventory _playerInventory;
    private PlayerInputSystem _playerInputSystem;
    private List<DroppedItem> _items = new List<DroppedItem>();
    private int _currentIndex = 0;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerInventory = GetComponent<Inventory>();
        _playerInputSystem = _player.InputSystem;
        _playerInputSystem.Movement.TakeItem.started += ctx => TakeItem();
        _playerInputSystem.Movement.SwitchTakeItem.performed += ctx => ChangeCurrentIndex(1);
    }

    private void ChangeCurrentIndex(int value)
    {
        _currentIndex += value;
        if (_currentIndex >= _items.Count)
            _currentIndex = 0;
        UpdateItemInfo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DroppedItem>(out var droppedItem))
        {
            _items.Add(droppedItem);
            OpenItemInfo();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DroppedItem>(out var droppedItem))
        {
            _items.Remove(droppedItem);
            if (_items.Count == 0)
            {
                CloseItemInfo();
            }
            else
            {
                ChangeCurrentIndex(0);
                UpdateItemInfo();
            }
        }
    }

    private void OpenItemInfo()
    {
        _itemInfo.gameObject.SetActive(true);
        UpdateItemInfo();
    }    

    private void UpdateItemInfo()
    {
        _itemInfo.Render(_items[_currentIndex].Item);
    }

    private void CloseItemInfo()
    {
        _itemInfo.gameObject.SetActive(false);
    }

    private void TakeItem()
    {
        if (_items.Count > 0)
        {
            var item = _items[_currentIndex].Item;
            var collection = _playerInventory.GetItemCollection(item);
            for (int i = 0; i < collection.Length; i++)
            {
                if (_playerInventory.TryAddItem(item, i))
                {
                    DestroyCurrentItem();
                    return;
                }
            }
            StartCoroutine(_player.OpenMenu(MenuController.OpenType.TakeItem));
            _takeItemMenu.SetTakeItem(item);
        }
    }

    private void DestroyCurrentItem()
    {
        Destroy(_items[_currentIndex].gameObject);
    }

    private void OnEnable()
    {
        _takeItemMenu.TookItem += DestroyCurrentItem;
    }

    private void OnDisable()
    {
        _takeItemMenu.TookItem -= DestroyCurrentItem;
    }
}