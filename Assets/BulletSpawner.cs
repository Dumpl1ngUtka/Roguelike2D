using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    private float _timeToSpawn = 2f;
    private float _bulletSpeed = 4f;
    private float _timer = 0;
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeToSpawn)
        {
            _timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        var bullet = Instantiate(_prefab,_spawnPoint.position,_spawnPoint.rotation).GetComponent<BulletMover>();
        bullet.Init(_bulletSpeed, Vector2.left);
    }
}
