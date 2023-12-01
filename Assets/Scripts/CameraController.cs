using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Vector3 _cameraPosition;

    private void LateUpdate()
    {
        Move();
        Offset();
        transform.position = _cameraPosition;
    }
    private void Move()
    {
        _cameraPosition = Vector2.Lerp(_cameraPosition, _player.transform.position, 0.7f * Time.deltaTime);
    }

    private void Offset()
    {
        _cameraPosition.z = -20f;
    }
}
