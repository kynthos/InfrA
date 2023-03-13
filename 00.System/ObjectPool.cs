using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : IPoolable
{
    private GameObject prefab;
    private LinkedList<T> instants = new LinkedList<T>();

    private ObjectPool() {}
    public ObjectPool(GameObject _prefab)
    {
        try
        {
            if (_prefab == null)
                throw new System.Exception("Prefab is null");

            prefab = _prefab;
        }
        catch(System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private T Create()
    {
        GameObject inst = GameObject.Instantiate(prefab);

        T scriptInst = inst.GetComponent<T>();

        return scriptInst;
    }

    public T GetInst()
    {
        if (instants.Count > 0)
        {
            T ret = instants.Last.Value;

            instants.RemoveLast();

            ret.OnGetFromPool();

            return ret;
        }

        return Create();
    }

    public void ReturnInst(T inst)
    {
        if (inst == null) return;

        inst.OnReturnPool();
        instants.AddLast(inst);
    }
}
