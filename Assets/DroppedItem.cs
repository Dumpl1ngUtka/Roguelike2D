using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayerMask;
    
    public Item Item;

    public void SetItem(Item item)
    {
        Item = item;
    }
}
