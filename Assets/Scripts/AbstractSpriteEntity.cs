using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpriteEntity<T> : MonoBehaviour where T : AbstractSpriteData
{
    public T data { get; protected set; }
    public SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        if (data != null) spriteRenderer.sprite = data.sprite;
    }

    public void SetData(T newData)
    {
        data = newData;
        if (data != null) spriteRenderer.sprite = data.sprite;
    }
}
