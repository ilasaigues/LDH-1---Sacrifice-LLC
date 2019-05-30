using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntity : Singleton<LevelEntity>
{
    public LevelData levelData;

    public List<Transform> spawnLocations;
    public List<Transform> processorLocations;
    public List<Transform> altarLocations;

    public SpawnerEntity spawnerPrefab;
    public ProcessorEntity processorPrefab;
    public SacrificialAltar altarPrefab;

    public System.Action OnDefeat = () => { };
    public System.Action<int> OnSuccess = (score) => { };


    void Start()
    {
        ErrorCheck();

        foreach (SpawnerData spawnData in levelData.availableSpawners)
        {
            Transform target = spawnLocations[Random.Range(0, spawnLocations.Count)];
            spawnLocations.Remove(target);
            SpawnerEntity newSpawner = Instantiate(spawnerPrefab, target.position, Quaternion.identity);
            newSpawner.SetData(spawnData);
        }

        foreach (ProcessorData processorData in levelData.availableProcessors)
        {
            Transform target = processorLocations[Random.Range(0, processorLocations.Count)];
            processorLocations.Remove(target);
            ProcessorEntity newProcessor = Instantiate(processorPrefab, target.position, Quaternion.identity);
            newProcessor.SetData(processorData);
        }

        Instantiate(altarPrefab, altarLocations[Random.Range(0, altarLocations.Count)].position, Quaternion.identity);
        RequestSystem.Instance.OnFinishedRequestsCountUpdate += FinishedRequestsCountUpdated;
        ScoreManager.Instance.OnReputationUpdate += ReputationUpdated;
    }

    void ErrorCheck()
    {
        bool error = false;
        string msg = "";
        if (spawnLocations.Count < levelData.availableSpawners.Count)
        {
            error = true;
            msg += string.Format("Spawn locations count mismatch | Required: {0}, Provided: {1}" + System.Environment.NewLine, levelData.availableSpawners.Count, spawnLocations.Count);
        }
        if (processorLocations.Count < levelData.availableProcessors.Count)
        {
            error = true;
            msg += string.Format("Processor locations count mismatch | Required: {0}, Provided: {1}" + System.Environment.NewLine, levelData.availableProcessors.Count, processorLocations.Count);
        }
        if (altarLocations.Count < 1)
        {
            error = true;
            msg += "No altar locations found";

        }
        if (error)
        {
            throw new System.Exception(msg);
        }
    }

    void FinishedRequestsCountUpdated(int requests)
    {
        Debug.Log("Completed requests:" + requests);
        if (requests >= levelData.requestCount)
        {
            OnSuccess(ScoreManager.Instance.score);
            HighScoreManager.SaveNewHighScore(levelData, ScoreManager.Instance.score);
        }
    }

    void ReputationUpdated(float reputation)
    {
        if (reputation <= 0)
        {
            OnDefeat();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
