using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new CombinationData", menuName = "Scriptables/Combination Data")]
public class CombinationData : ScriptableObject
{
    public ItemData itemA;
    public ItemData itemB;
    public ItemData result;
}
