using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonReader
{
    public static Levels ReadLevelsJson(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<Levels>(jsonFile.text);
    }
}
