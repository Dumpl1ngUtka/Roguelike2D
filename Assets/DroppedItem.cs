using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private GameObject _takeIcon;
    private const float _distance = 3f;
    private void Update()
    {
        SetTakeImage();
    }

    private void SetTakeImage()
    {
        var player = Physics2D.CircleCast(transform.position, _distance, Vector2.zero, _distance, _playerLayerMask);
        if (player.collider != null && !_takeIcon.activeSelf)
            _takeIcon.SetActive(true);
        else if (player.collider == null && _takeIcon.activeSelf)
            _takeIcon.SetActive(false);
    }
}
