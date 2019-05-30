using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessorEntity : AbstractSpriteEntity<ProcessorData>
{
    public ProgressBar progressBar;

    private ProcessRecipe _currentRecipe = null;
    private float _progress = 0;
    protected override void Start()
    {
        base.Start();
        if (data != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = data.sprite;
        }
    }

    private void Update()
    {
        if (_currentRecipe != null)
        {
            if (_progress > _currentRecipe.time.Value)
            {
                ItemEntity newItemEntity = CombinationManager.Instance.GetItemInstance(_currentRecipe.output, transform.position + Vector3.back);
                _currentRecipe = null;
                progressBar.Visible(false);
            }
            else
            {
                _progress += TimeManager.deltaTime;
                progressBar.SetProgress(_progress / _currentRecipe.time.Value);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        ItemEntity item = collision.GetComponent<ItemEntity>();
        if (item) item.OnDrop += OnItemDrop;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        ItemEntity item = collision.GetComponent<ItemEntity>();
        if (item) item.OnDrop -= OnItemDrop;
    }

    void OnItemDrop(ItemEntity item)
    {
        if (_currentRecipe != null) return;
        foreach (ProcessRecipe recipe in data.recipes)
        {
            if (recipe.input == item.data)
            {
                _currentRecipe = recipe;
                _progress = 0;
                Destroy(item.gameObject);
                progressBar.Visible(true);

            }
        }
    }
}
