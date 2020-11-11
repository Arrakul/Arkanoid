using System.Collections;
using System.Collections.Generic;
using Arkanoid.Utils;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public List<GameObject> GetObjects(GameObject prefab, int count, Transform position)
    {
        if (prefab == null) return null;
        
        var listObj = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab, position);
            obj.name = prefab.name + i;
            obj.SetActive(false);
            
            listObj.Add(obj);
        }

        return listObj;
    }
    
    public GameObject GetObject(GameObject prefab, Transform position)
    {
        if (prefab == null) return null;
        
        var obj = Instantiate(prefab, position);
        obj.name = prefab.name;
        obj.SetActive(false);

        return obj;
    }
}
