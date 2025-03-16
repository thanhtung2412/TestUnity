using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject panelHome;  
    [SerializeField] private GameObject panelLost;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelTimeAttackMode;
    [SerializeField] private TextMeshProUGUI timeTxt;
    private void Awake()
    {
        instance = this;
    }
    public void ShowPlayGame()
    {
        HidePanelHome();
        GameManager.instance.gameStates = GameStates.gameStarted;
        StageManager.instance.GameStarted();        
    }
    public void GameAutoPlay()
    {
        HidePanelHome();
        GameManager.instance.gameStates = GameStates.gameAutoPlay;
        StageManager.instance.GameStarted();
    }
    public void GameAutoLose()
    {
        HidePanelHome();
        GameManager.instance.gameStates = GameStates.gameAutoLose;
        StageManager.instance.GameStarted();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowPanelWin()
    {
        panelWin.SetActive(true);
    }
    public void ShowPanelLose()
    {
        panelLost.SetActive(true);
    }
    public void HidePanelHome()
    {
        panelHome.SetActive(false);
    }
    public void ShowPanelTimeAttack()
    {
        HidePanelHome();
        panelTimeAttackMode.SetActive(true);
        GameManager.instance.gameStates = GameStates.gameAttackMode;
        StageManager.instance.GameStarted();
    }
    public void UpdateTimerUI(float timer)
    {
        int second = Mathf.FloorToInt(timer);
        timeTxt.text = $"{second}";
    }
}
