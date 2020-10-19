using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Settings", menuName = "ScriptableObjects/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    public bool hasEnemy;
    public float timeEnemyMovement;
    public int minVirusSpawn;
    public int maxVirusSpawn;
}
