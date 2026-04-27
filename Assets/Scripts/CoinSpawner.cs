using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private float _repeatRate = 3.0f;

    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (coin) => OnGetAction(coin),
            actionOnRelease: (coin) => ActionOnRelease(coin),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        foreach (var point in _spawnPoints)
        {
            var coin = _pool.Get();
            coin.transform.position = point.position;
        }
    }

    private void OnGetAction(Coin coin)
    {
        coin.gameObject.SetActive(true);
        coin.Touched += ReturnToPool;
    }

    private void ActionOnRelease(Coin coin)
    {
        coin.gameObject.SetActive(false);
        coin.Touched -= ReturnToPool;
    }

    private void ReturnToPool(Coin coin)
    {
        _pool.Release(coin);
        Vector3 position = coin.transform.position;
        StartCoroutine(SpawnCoin(position));
    }
    
    private IEnumerator SpawnCoin(Vector3 position)
    {
        yield return new WaitForSeconds(_repeatRate);
        var coin = _pool.Get();
        coin.transform.position = position;        
    }   
}