using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int level;
    public List<LevelChunk> levelsChunks;
}

[System.Serializable]
public class LevelChunk
{
    public List<float> timeInterval;
    public List<string> enemies;
    public List<string> powerups;
}

