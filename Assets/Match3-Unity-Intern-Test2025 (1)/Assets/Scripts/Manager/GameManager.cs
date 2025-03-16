using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates gameStates;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameStates = GameStates.gameHome;
    }
}
