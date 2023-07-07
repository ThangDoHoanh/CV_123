using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling _instant;
    public static ObjectPooling instant => _instant;

    Dictionary<GameObject, List<GameObject>> _poolObjects2 = new Dictionary<GameObject, List<GameObject>>();
    private void Start()
    {
        if (_instant == null)
        {
            _instant = this;
            DontDestroyOnLoad(this);// them dong nay
            return;
        }
        if (_instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject GetObject(GameObject key)
{
    List<GameObject> _itemPool = new List<GameObject>();
    if (!_poolObjects2.ContainsKey(key))
    {
        _poolObjects2.Add(key, _itemPool);
    }
    else
    {
        _itemPool = _poolObjects2[key];
    }

    // Remove any null objects from the pool
    _itemPool.RemoveAll(item => item == null); // them dong nay

    foreach (GameObject g in _itemPool)
    {
        if (g.gameObject.activeSelf)
            continue;
        return g;
    }

    GameObject g2 = Instantiate(key, this.transform.position, Quaternion.identity);
    _poolObjects2[key].Add(g2);
    return g2;
}

}
