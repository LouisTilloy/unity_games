using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class JsonReader
{
    List<TextAsset> levelJsons;

    public Level ReadLevelJson(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<Level>(jsonFile.text);
    }

    public List<Level> ReadAllLevels()
    {
        List<Level> allLevels = new List<Level>();
        foreach (TextAsset jsonFile in levelJsons)
        {
            allLevels.Add(JsonUtility.FromJson<Level>(jsonFile.text));
        }
        return allLevels.OrderBy(x => x.level).ToList();
    }
}
