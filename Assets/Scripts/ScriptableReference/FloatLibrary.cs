using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="floatLib",menuName = "Scriptables/Values/Libraries/Float")]
public class FloatLibrary : ScriptableObject
{
    [SerializeField]
    public List<FloatValue> values = new List<FloatValue>();
}
