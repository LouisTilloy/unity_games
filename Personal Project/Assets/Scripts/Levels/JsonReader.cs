using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class JsonReader
{
    public static Level ReadLevelJson(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<Level>(jsonFile.text);
    }

    public static List<Level> ReadAllLevels(string directoryPath)
    {
        List<Level> allLevels = new List<Level>();
        DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
        foreach (FileInfo file in dirInfo.GetFiles("*.json"))
        {
            string filePath = "levels/" + Path.GetFileNameWithoutExtension(file.Name);
            TextAsset jsonFile = Resources.Load<TextAsset>(filePath);
            allLevels.Add(JsonUtility.FromJson<Level>(jsonFile.text));
        }
        return allLevels.OrderBy(x => x.level).ToList();
    }
}
