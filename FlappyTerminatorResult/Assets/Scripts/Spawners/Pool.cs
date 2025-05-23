using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<TItem> : IPool<TItem> where TItem : MonoBehaviour, IItem<TItem>
{
    private TItem _prefab;
    private int _capacity;
    private int _maxSize;
    private int _numberOfObjects;

    private ObjectPool<TItem> _pool;

    public event Action<Vector3> Released;

    public int NumberOfObjects => _numberOfObjects;
    public int Capacity => _capacity;

    public void Initialize(TItem prefab, int capacity, int maxSize)
    {
        _capacity = capacity;
        _maxSize = maxSize;
        _prefab = prefab;

        _pool = new ObjectPool<TItem>(
            createFunc: () => MonoBehaviour.Instantiate(_prefab),
            actionOnGet: (item) => ActionOnGet(item),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => MonoBehaviour.Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: _capacity,
            maxSize: _maxSize);
    }

    public TItem GetObject()
    {
        _numberOfObjects++;

        return _pool.Get();
    }

    public void Reset()
    {
        _pool.Clear();

        var activeObject = UnityEngine.Object.FindObjectsOfType(typeof(TItem)) as TItem[];

        foreach (var item in activeObject)
        {
            if (item != null && item.gameObject.activeInHierarchy)
            {
                MonoBehaviour.Destroy(item.gameObject);
            }
        }

        _numberOfObjects = 0;
    }

    private void ActionOnGet(TItem item)
    {
        item.Deactivated += ReleaseItem;
    }

    private void ReleaseItem(TItem item)
    {
        _numberOfObjects--;

        Released?.Invoke(item.transform.position);

        item.Deactivated -= ReleaseItem;
        _pool.Release(item);
    }
}