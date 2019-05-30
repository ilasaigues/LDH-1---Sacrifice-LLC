using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new processorData", menuName = "Scriptables/Processor Data")]

public class ProcessorData : AbstractSpriteData
{
    public List<ProcessRecipe> recipes = new List<ProcessRecipe>();
}
[System.Serializable]
public class ProcessRecipe
{
    public ItemData input;
    public ItemData output;
    public FloatReference time;
}