using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new SpawnerData", menuName = "Scriptables/Spawner Data")]
public class SpawnerData : AbstractSpriteData
{
    public ItemData item;
    public float spawnTime;
}
