using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool inGame = true;
    public Action OnSceneUnload; //Used to unsubscribe every action when scene is unloaded
}
