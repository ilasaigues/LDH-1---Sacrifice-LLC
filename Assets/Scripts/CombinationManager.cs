using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : Singleton<CombinationManager>
{
    public ItemEntity ItemEntityPrefab;
    public List<CombinationData> combinations = new List<CombinationData>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ItemData GetItemCombination(ItemData a, ItemData b)
    {
        foreach (CombinationData combo in combinations)
        {
            if ((combo.itemA == a && combo.itemB == b) || (combo.itemA == b && combo.itemB == a))
            {
                return combo.result;
            }
        }
        return null;
    }

    public ItemEntity GetItemInstance(ItemData data, Vector3 pos = default)
    {
        ItemEntity newInstance = Instantiate(ItemEntityPrefab, pos, Quaternion.identity);
        newInstance.SetData(data);
        return newInstance;
    }
}

