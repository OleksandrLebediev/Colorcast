using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelsItemSaveSystem: BinarySaveSystem<Level>
{
    private readonly string _saveFileName = "levelItem";
    private readonly string _saveDirectoryName = "LevelItems";

    public LevelsItemSaveSystem()
    {
        SetPath(_saveFileName, _saveDirectoryName);        
    }

    public Level GetLevelDate(int number)
    {
        Level level;
        bool isSelect = false;
        if (ChackExistFile(number) == false)
        {         
            if(number == 0)
            {
                isSelect = true;
            }

            level = new Level(number, 0, isSelect);
            Save(level, number);
        }
        else
        {
            level = Load(number);
        }

        return level;
    }
}
