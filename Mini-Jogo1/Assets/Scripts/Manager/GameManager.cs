using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    //Singleton
    private static readonly GameManager InstanceHolder = new GameManager();
    static GameManager() {}
    private GameManager() {}
    public static GameManager Instance => InstanceHolder;

    public bool inGame = true;
}
