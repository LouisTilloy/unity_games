using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstanceJsonReader : MonoBehaviour
{
    [SerializeField] bool debugMode;
    [SerializeField] List<TextAsset> levelJsons;

    public Level ReadLevelJson(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<Level>(jsonFile.text);
    }

    public List<Level> ReadAllLevels()
    {
        List<Level> allLevels = new List<Level>();
        
        if (debugMode)
        {
            levelJsons = new List<TextAsset>(levelJsons.Take(1));
        }

        foreach (TextAsset jsonFile in levelJsons)
        {
            allLevels.Add(JsonUtility.FromJson<Level>(jsonFile.text));
        }
        return allLevels.OrderBy(x => x.level).ToList();
    }
}
