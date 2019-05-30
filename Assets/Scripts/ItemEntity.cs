using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : AbstractSpriteEntity<ItemData>
{
    public System.Action<ItemEntity> OnDrop = (i) => { };
    public System.Action<ItemEntity> OnPickUp = (i) => { };
    public System.Action<ItemEntity> OnCombine = (i) => { };
    public bool locked { get; private set; }

    List<ItemEntity> itemsHoveringOver = new List<ItemEntity>();

    protected override void Start()
    {
        base.Start();
        if (data != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = data.sprite;
            gameObject.name = "Item: " + data.name;
        }
    }

    void Update()
    {

    }

    public void Lock(bool locked = true)
    {
        this.locked = locked;
        if (locked)
        {
            GetComponent<Collider2D>().isTrigger = true;
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, .666f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void Drop()
    {
        Lock(false);
        OnDrop(this);
        if (!locked && itemsHoveringOver.Count > 0)
        {
            foreach (ItemEntity item in itemsHoveringOver)
            {
                if (item.locked) continue;
                ItemData comboData = CombinationManager.Instance.GetItemCombination(this.data, item.data);
                if (comboData)
                {
                    OnCombine(this);
                    item.OnCombine(item);
                    CombinationManager.Instance.GetItemInstance(comboData, (item.transform.position + transform.position) / 2);
                    Destroy(item.gameObject);
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
    public void PickUp()
    {
        Lock(false);
        OnPickUp(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemEntity item = collision.GetComponent<ItemEntity>();
        if (item && !itemsHoveringOver.Contains(item))
        {
            itemsHoveringOver.Add(item);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemEntity item = other.GetComponent<ItemEntity>();
        if (item)
        {
            itemsHoveringOver.Remove(item);
        }
    }

}
