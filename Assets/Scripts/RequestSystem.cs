using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestSystem : Singleton<RequestSystem>
{
    [System.Serializable]
    public class Request
    {
        public List<ItemData> items = new List<ItemData>();
        public int weight;
        public float startTime;
        public float remainingTime;
        public bool satisfied;

        public Request(List<ItemData> items, float startTime, int weight)
        {
            this.items = items;
            this.remainingTime = this.startTime = startTime;
            this.weight = weight;
        }
        public bool SatisfiedBy(List<ItemEntity> queryItems)
        {
            List<ItemData> dataList = new List<ItemData>();
            foreach (ItemEntity entity in queryItems) dataList.Add(entity.data);
            return SatisfiedBy(dataList);
        }
        public bool SatisfiedBy(List<ItemData> queryItems)
        {
            List<ItemData> tempQueryItems = new List<ItemData>(queryItems);
            List<ItemData> tempRequestItems = new List<ItemData>(items);

            for (int i = tempQueryItems.Count - 1; i >= 0; i--)
            {
                if (tempRequestItems.Count == 0) return false;
                var qi = tempQueryItems[i];
                if (tempRequestItems.Contains(qi))
                {
                    tempQueryItems.RemoveAt(i);
                    tempRequestItems.Remove(qi);
                }
            }

            return tempRequestItems.Count <= 0;
        }
    }

    public FloatReference baseCooldown;
    public FloatReference baseRequestTime;
    public FloatReference requestTimeOffsetByWeight;

    public Dictionary<ItemData, int> allAvailableItems = new Dictionary<ItemData, int>();

    public System.Action<Request> OnNewRequest = (req) => { };
    public System.Action<int> OnFinishedRequestsCountUpdate = (count) => { };

    int finishedRequests;
    float timeSinceLastRequest = 5;
    int lastRequestWeight = 0;

    bool gameOver = false;

    public List<Request> requestQueue = new List<Request>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (SpawnerData spawnData in LevelEntity.Instance.levelData.availableSpawners)
        {
            allAvailableItems[spawnData.item] = 1;
        }

        foreach (ProcessorData processData in LevelEntity.Instance.levelData.availableProcessors)
        {
            foreach (ProcessRecipe recipe in processData.recipes)
            {
                if (!allAvailableItems.ContainsKey(recipe.output) && allAvailableItems.ContainsKey(recipe.input))
                {
                    allAvailableItems[recipe.output] = 1 + allAvailableItems[recipe.input];
                }
            }
        }

        foreach (CombinationData combo in CombinationManager.Instance.combinations)
        {
            if (!allAvailableItems.ContainsKey(combo.result) && allAvailableItems.ContainsKey(combo.itemA) && allAvailableItems.ContainsKey(combo.itemB))
            {
                allAvailableItems[combo.result] = 1 + allAvailableItems[combo.itemA] + allAvailableItems[combo.itemB];
            }
        }

        foreach (var pair in allAvailableItems)
        {
            print(pair.Key.name);
        }

        LevelEntity.Instance.OnDefeat += OnGameOver;
        LevelEntity.Instance.OnSuccess += OnGameOver;

    }

    void OnGameOver()
    {
        foreach (Request req in requestQueue)
        {
            req.satisfied = true;
        }
        requestQueue.Clear();
        gameOver = true;
    }
    void OnGameOver(int score)
    {
        OnGameOver();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        timeSinceLastRequest += TimeManager.deltaTime;
        if (timeSinceLastRequest >= baseCooldown.Value + lastRequestWeight * Random.Range(.5f, 1.5f))
        {
            Request newRequest = NewRequest(2 * LevelEntity.Instance.levelData.difficulty);
            OnNewRequest(newRequest);
            requestQueue.Add(newRequest);
            timeSinceLastRequest = 0;
        }

        for (int i = requestQueue.Count - 1; i >= 0; i--)
        {
            if (requestQueue.Count == 0) break;
            Request req = requestQueue[i];
            if (req.satisfied || req.remainingTime <= 0)
            {
                requestQueue.RemoveAt(i);
                finishedRequests++;
                ScoreManager.Instance.RequestEnd(req, req.satisfied);
                OnFinishedRequestsCountUpdate(finishedRequests);
            }
            else
            {
                req.remainingTime -= TimeManager.deltaTime;
            }
        }

    }
    public Request NewRequest(float maxWeight)
    {
        return NewRequest(Mathf.FloorToInt(maxWeight));
    }

    public Request NewRequest(int maxWeight)
    {
        List<ItemData> result = new List<ItemData>();

        int targetWeight = Random.Range(1, maxWeight + 1);
        if (targetWeight == lastRequestWeight)
        {
            if (lastRequestWeight == 1) targetWeight = lastRequestWeight + 1;
            else targetWeight = lastRequestWeight - 1;
        }
        lastRequestWeight = targetWeight;
        List<ItemData> usableItems = ItemsAffordedWith(targetWeight);
        while (targetWeight > 0)
        {
            ItemData requestItem = usableItems[Random.Range(0, usableItems.Count)];
            targetWeight -= allAvailableItems[requestItem];
            result.Add(requestItem);
            usableItems = ItemsAffordedWith(targetWeight);
        }

        return new Request(result, GetRequestTime(lastRequestWeight), lastRequestWeight);
    }

    public float GetRequestTime(int weight)
    {
        return baseRequestTime.Value + weight * requestTimeOffsetByWeight.Value;
    }

    public List<ItemData> ItemsAffordedWith(int cost)
    {
        List<ItemData> result = new List<ItemData>();
        foreach (var kvp in allAvailableItems)
        {
            if (kvp.Value <= cost) result.Add(kvp.Key);
        }
        return result;
    }
}

