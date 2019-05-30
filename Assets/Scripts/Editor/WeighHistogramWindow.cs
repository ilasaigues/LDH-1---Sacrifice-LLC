using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WeighHistogramWindow : EditorWindow
{

    [MenuItem("Window/Weight Histogram")]
    public static void OpenWindow()
    {
        GetWindow<WeighHistogramWindow>();
    }

    public Dictionary<ItemData, int> availableItems = new Dictionary<ItemData, int>();


    private void OnEnable()
    {
        List<SpawnerData> allSpawnerData = new List<SpawnerData>();
        List<ProcessorData> allProcessData = new List<ProcessorData>();
        List<CombinationData> allCombinationData = new List<CombinationData>();

        string[] allSpawnerPaths = AssetDatabase.FindAssets("t:SpawnerData");
        string[] allProcessPaths = AssetDatabase.FindAssets("t:ProcessorData");
        string[] allComboPaths = AssetDatabase.FindAssets("t:CombinationData");

        foreach (string guid in allSpawnerPaths)
        {
            allSpawnerData.Add(AssetDatabase.LoadAssetAtPath<SpawnerData>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        foreach (string guid in allProcessPaths)
        {
            allProcessData.Add(AssetDatabase.LoadAssetAtPath<ProcessorData>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        foreach (string guid in allComboPaths)
        {
            allCombinationData.Add(AssetDatabase.LoadAssetAtPath<CombinationData>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        foreach (SpawnerData spawnData in allSpawnerData)
        {
            availableItems[spawnData.item] = 1;
        }

        foreach (ProcessorData processData in allProcessData)
        {
            foreach (ProcessRecipe recipe in processData.recipes)
            {
                if (!availableItems.ContainsKey(recipe.output) && availableItems.ContainsKey(recipe.input))
                {
                    availableItems[recipe.output] = 1 + availableItems[recipe.input];
                }
            }
        }

        foreach (CombinationData combo in allCombinationData)
        {
            if (!availableItems.ContainsKey(combo.result) && availableItems.ContainsKey(combo.itemA) && availableItems.ContainsKey(combo.itemB))
            {
                availableItems[combo.result] = 1 + availableItems[combo.itemA] + availableItems[combo.itemB];
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh"))
        {
            OnEnable();
        }

        List<List<ItemData>> costs = new List<List<ItemData>>();
        int highestCost = 0;
        foreach (var kvp in availableItems)
        {
            if (highestCost < kvp.Value) highestCost = kvp.Value;
            while (costs.Count < kvp.Value)
            {
                costs.Add(new List<ItemData>());
            }
            costs[kvp.Value - 1].Add(kvp.Key);
        }
        EditorGUILayout.BeginHorizontal();

        for (int i = 0; i < costs.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(15));
            EditorGUILayout.LabelField((i + 1).ToString(), GUILayout.MaxWidth(15));
            for (int j = 0; j < costs[i].Count; j++)
            {
                EditorGUILayout.LabelField("[" + costs[i][j].name + "]", GUILayout.Width(100));
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

    }
}
