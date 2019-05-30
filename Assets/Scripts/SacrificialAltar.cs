using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialAltar : MonoBehaviour
{
    public float sacrificeTime = 3;

    public ProgressBar progressBar;
    public SpriteRenderer spriteRenderer;

    List<ItemEntity> currentItems = new List<ItemEntity>();
    float timeSinceLastDrop = 0;
    private Vector3 targetDirection;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentItems.Count > 0)
        {
            progressBar.Visible(true);
            timeSinceLastDrop += TimeManager.deltaTime;

            targetDirection = Quaternion.AngleAxis(timeSinceLastDrop * timeSinceLastDrop * 360, Vector3.forward) * Vector3.up;

            progressBar.SetProgress(timeSinceLastDrop / sacrificeTime);
            if (timeSinceLastDrop >= sacrificeTime)
            {
                bool satisfiesSomething = false;
                foreach (RequestSystem.Request request in RequestSystem.Instance.requestQueue)
                {
                    if (request.SatisfiedBy(currentItems))
                    {
                        satisfiesSomething = true;
                        request.satisfied = true;//TODO: THIS IS WHERE YOU ADD SCORES AND STUFF
                        break;
                    }
                }

                if (!satisfiesSomething)
                {
                    foreach (RequestSystem.Request request in RequestSystem.Instance.requestQueue)
                    {
                        request.remainingTime -= request.startTime * .1f;
                    }
                }

                foreach (ItemEntity item in currentItems)
                {
                    Destroy(item.gameObject);
                }
                currentItems.Clear();
                progressBar.Visible(false);
                timeSinceLastDrop = 0;
                targetDirection = Vector3.up;
            }
            else
            {
                for (int i = 0; i < currentItems.Count; i++)
                {
                    ItemEntity current = currentItems[i];
                    float targetAngle = i * 360f / currentItems.Count;
                    var newPos = transform.position + (Quaternion.AngleAxis(targetAngle + Time.time * 90, Vector3.forward) * Vector3.up) * .75f;
                    current.transform.position = Vector3.Lerp(current.transform.position, newPos, .1f);
                }
            }

        }
        spriteRenderer.transform.up = Vector3.Slerp(spriteRenderer.transform.up, targetDirection, .33333333f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        ItemEntity item = collision.GetComponent<ItemEntity>();
        if (item) item.OnDrop += OnItemDrop;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        ItemEntity item = collision.GetComponent<ItemEntity>();
        if (item != null) item.OnDrop -= OnItemDrop;
    }

    void OnItemDrop(ItemEntity item)
    {
        item.Lock();
        item.OnDrop -= OnItemDrop;
        item.OnPickUp += OnItemPickUp;
        currentItems.Add(item);
        timeSinceLastDrop = 0;
    }

    void OnItemPickUp(ItemEntity item)
    {
        item.OnPickUp -= OnItemPickUp;
        currentItems.Remove(item);
        if (currentItems.Count <= 0)
        {
            progressBar.Visible(false);
        }
    }
}
