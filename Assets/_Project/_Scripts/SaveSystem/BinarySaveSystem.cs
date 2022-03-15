using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public abstract class BinarySaveSystem<T> 
{        
    private string _filePath;
    private string _saveDirectory;
    private string _saveFileName;

    public void Save(T data, int fileNumber)
    {
        _filePath = GetPath(fileNumber);

        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, data);
        }
    }

    public T Load(int fileNumber)
    {
        T saveData;
        _filePath = GetPath(fileNumber);

        using (FileStream file = File.Open(_filePath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            saveData = (T)loadedData;
        }

        return saveData;
    }

    public bool ChackExistFile(int fileNumber)
    {
        return File.Exists(GetPath(fileNumber));
    }

    public virtual void SetPath(string saveFileName, string saveDirectoryName)
    {
        _saveFileName = saveFileName;
        _saveDirectory = Application.persistentDataPath + $"/{saveDirectoryName}/";
        Directory.CreateDirectory(_saveDirectory);
    }

    public virtual string GetPath(int fileNumber)
    {
        return _saveDirectory + $"{_saveFileName}_{fileNumber}.dat";
    }

}
