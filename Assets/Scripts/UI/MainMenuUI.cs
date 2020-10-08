using System;
using UnityEngine;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject PvPContur;
    
    [SerializeField] private GameObject PvEContur;
    
    [SerializeField] private GameObject MainmenuPanel;

    private void Start()
    {
        var PvP = GameConfiguration.Instance.GameType == GameType.PvP;
        PvPContur.SetActive(PvP);
        PvEContur.SetActive(!PvP);

        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleStartGame()
    {
        MainmenuPanel.gameObject.SetActive(false);
    }
    
    private void HandleEndGame()
    {
        MainmenuPanel.gameObject.SetActive(true);
    }

    public void HandleStartGameButtonClick()
    {
        GameInitializer.Instance.StartGame();
    }
    
    public void HandlePvPGameButtonClick()
    {
        GameConfiguration.Instance.UpdateGameType(GameType.PvP);
        PvPContur.SetActive(true);
        PvEContur.SetActive(false);
    }
    
    public void HandlePvEGameButtonClick()
    {
        GameConfiguration.Instance.UpdateGameType(GameType.PvE);
        PvPContur.SetActive(false);
        PvEContur.SetActive(true);
    }
}
