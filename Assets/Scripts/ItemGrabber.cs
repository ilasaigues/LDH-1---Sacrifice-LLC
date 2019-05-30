using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    private List<ItemEntity> _gameEntities = new List<ItemEntity>();
    private ItemEntity _grabbedEntity = null;

    private const int LAYER_ITEMS = 10;
    private const int LAYER_GRABBED = 11;

    public ItemEntity Closest
    {
        get
        {
            ItemEntity closest = null;
            foreach (ItemEntity coll in _gameEntities)
            {
                if (closest == null || Vector3.Distance(closest.transform.position, transform.position) > Vector3.Distance(coll.transform.position, transform.position))
                {
                    closest = coll;
                }
            }
            return closest;
        }
    }

    public void FixedUpdate()
    {
        if (_grabbedEntity != null) _grabbedEntity.transform.up = Vector3.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemEntity ent = collision.GetComponent<ItemEntity>();
        if (ent && !_gameEntities.Contains(ent))
            _gameEntities.Add(ent);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemEntity ent = collision.GetComponent<ItemEntity>();
        if (ent && _gameEntities.Contains(ent))
            _gameEntities.Remove(ent);
    }

    public void Toggle()
    {
        if (_grabbedEntity) //drop
        {
            _grabbedEntity.transform.parent = null;
            _grabbedEntity.gameObject.layer = LAYER_ITEMS;
            _grabbedEntity.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _grabbedEntity.GetComponent<Collider2D>().isTrigger = false;
            _grabbedEntity.transform.up = Vector3.up;
            _grabbedEntity.Drop();
            _grabbedEntity = null;
        }
        else if (Closest != null) //grab
        {
            _grabbedEntity = Closest;
            if (_grabbedEntity == null) return;
            _grabbedEntity.gameObject.layer = LAYER_GRABBED;
            _grabbedEntity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _grabbedEntity.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            _grabbedEntity.GetComponent<Collider2D>().isTrigger = true;
            _grabbedEntity.transform.parent = transform;
            _grabbedEntity.transform.localPosition = Vector3.zero;
            _grabbedEntity.PickUp();
        }
    }

}
