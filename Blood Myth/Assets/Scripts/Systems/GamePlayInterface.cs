﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

///This has to be in the same order as the scenes that are loaded in the
///scene build settings, only doing this to avoid memorizing numbers.
public enum SceneId
{
    // Based on scene ordering in build index
    MainMenu = 0,
    Loading,

    Intro,
    Spring,
    Summer,
    Fall,
    Winter,
    Outro,

    Settings,
    Credits,
    Pause,
    SelectLevel,
}

public class GamePlayInterface : MonoBehaviour
{
    //TODO(CHRIS) Add whatever logic you want to run when unity is done
    //loading a scene. you can use IOSystem for getting saved data.

    #region [Sceneloading Function]

    public void MainMenuOnLoad(LoadSceneMode mode) { GameManager.Instance.ChangeGameState(GameStateId.MainMenu); }
    public void LoadingOnLoad(LoadSceneMode mode) { GameManager.Instance.ChangeGameState(GameStateId.Loading); }
    public void GameOnLoad(LoadSceneMode mode) { GameManager.Instance.ChangeGameState(GameStateId.Gameplay); }
    
    #endregion
    
}