using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(FloatLibrary))]
public class FloatLibraryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        foreach (var value in (target as FloatLibrary).values)
        {
            if (value == null)
            {
                GUILayout.Space(10);
            }
            else
            {
                value.value = EditorGUILayout.FloatField(value.name, value.value);
            }
        }
    }
}
