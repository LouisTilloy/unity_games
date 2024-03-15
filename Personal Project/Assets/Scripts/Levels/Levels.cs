using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Levels
{
    public List<LevelChunk> levelsChunks;
}

[System.Serializable]
public class LevelChunk
{
    public int level; // Which level the chunk belongs to
    public List<float> timeInterval;
    public List<string> enemies;
    public List<string> powerups;
}

