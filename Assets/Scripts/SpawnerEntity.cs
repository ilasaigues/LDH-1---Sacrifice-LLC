using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEntity : AbstractSpriteEntity<SpawnerData>
{
    public ProgressBar progressBar;

    private ItemEntity spawnedItem = null;
    private float spawnTime = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (data != null)
            spriteRenderer.sprite = data.sprite;
    }

    public void SetData()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedItem == null)
        {
            if (spawnTime <= 0)
            {
                spawnedItem = CombinationManager.Instance.GetItemInstance(data.item, transform.position + Vector3.back);
                spawnedItem.OnPickUp += SpawnedItemPickUp;
                spawnedItem.OnCombine += SpawnedItemCombine;
                progressBar.Visible(false);
            }
            else
            {
                spawnTime -= TimeManager.deltaTime;
                progressBar.SetProgress((data.spawnTime - spawnTime) / data.spawnTime);
            }

        }
    }

    private void SpawnedItemCombine(ItemEntity entity)
    {
        Debug.Log("Combined item " + entity.name);
        SpawnedItemPickUp(entity);
    }

    private void SpawnedItemPickUp(ItemEntity entity)
    {
        if (spawnedItem == entity)
        {
            spawnTime = data.spawnTime;
            progressBar.Visible(true);
            spawnedItem.OnCombine -= SpawnedItemCombine;
            spawnedItem.OnPickUp -= SpawnedItemPickUp;
            spawnedItem = null;
        }
    }
}
