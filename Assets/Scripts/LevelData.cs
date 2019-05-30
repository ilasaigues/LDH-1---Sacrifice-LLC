using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelData", menuName = "Scriptables/Level Data")]
public class LevelData : ScriptableObject
{
    public List<SpawnerData> availableSpawners;
    public List<ProcessorData> availableProcessors;
    [Range(1, 3)]
    public float difficulty;
    [Range(1, 100)]
    public int requestCount = 10;
    public int[] starThresholds = { 100, 200, 300 };

}
