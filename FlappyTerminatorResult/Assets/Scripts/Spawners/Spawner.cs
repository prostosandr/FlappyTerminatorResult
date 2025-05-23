using System.Collections;
using UnityEngine;

public abstract class Spawner<TItem> : MonoBehaviour where TItem : MonoBehaviour, IItem<TItem>
{
    [SerializeField] private Collider2D _startPosition;
    [SerializeField] private TItem _prefab;
    [SerializeField] private int _capacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;
    private Pool<TItem> _pool;
    private bool _canSpawn;

    public Pool<TItem> Pool => _pool;
    public float Delay => _delay;

    private void Awake()
    {
        _pool = new Pool<TItem>();
        _pool.Initialize(_prefab, _capacity, _maxSize);
    }

    private void OnEnable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(GenerateItems());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    public void Reset()
    {
        _pool.Reset();
    }

    public void SetCapacity(int value)
    {
        _capacity = value;
    }

    public void SetMaxSize(int value)
    {
        _maxSize = value;
    }

    public TItem GetObject()
    {
        return _pool.GetObject();
    }

    public void SetCanSpawn(bool value)
    {
        _canSpawn = value;
    }

    protected Vector2 GetSpawnPosition()
    {
        Bounds bound = _startPosition.bounds;

        return new Vector2(
            Random.Range(bound.center.x, bound.center.x),
            Random.Range(bound.min.y, bound.max.y)
            );
    }

    protected virtual void CreateObject()
    {
        Vector2 spawnPoint = GetSpawnPosition();

        var item = Pool.GetObject();
        item.gameObject.SetActive(true);
        item.transform.position = spawnPoint;
    }

    protected virtual void Spawn()
    {
        if (_pool.NumberOfObjects < _pool.Capacity && _canSpawn)
            CreateObject();
    }

    private IEnumerator GenerateItems()
    {
        var wait = new WaitForSeconds(Delay);

        while (enabled)
        {
            Spawn();

            yield return wait;
        }
    }
}