using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDataSaveSystem : BinarySaveSystem<LevelData>
{
    private readonly string _saveFileName = "levelData";
    private readonly string _saveDirectoryName = "LevelData";

    public LevelDataSaveSystem()
    {
        SetPath(_saveFileName, _saveDirectoryName);
    }

    public LevelData GetLevelDate(int number)
    {
        LevelData levelData;

        if (ChackExistFile(number) == false)
        {
            levelData = new LevelData();
            Save(levelData, number);
        }
        else
        {
            levelData = Load(number);
        }
        return levelData;
    }

    public LevelData GetIncreaseAttemptIncreaseAttempt(int number)
    {
        LevelData levelData = GetLevelDate(number);
        levelData.IncreaseAttemptNumber();
        Save(levelData, number);
        return levelData;
    }
}

[Serializable]
public class LevelData
{ 
    public LevelData()
    {
        AttemptNumber = 1;
    }
    
    public LevelData(int attemptNumber)
    {
        AttemptNumber = attemptNumber;
    }

    public int AttemptNumber { get; private set; }

    public void IncreaseAttemptNumber()
    {
        AttemptNumber++;
    }
}

