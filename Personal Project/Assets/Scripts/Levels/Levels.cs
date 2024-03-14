using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Levels
{
    public List<Level> levels;
}

[System.Serializable]
public class Level
{
    public int level;
    public List<float> timeInterval;
    public List<string> enemies;
}

