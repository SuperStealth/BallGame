using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuUI;
    [SerializeField] private CanvasGroup gameplayUI;
    [SerializeField] private Button startGameButton;

    [SerializeField] private GameController gameController;
    private void Awake()
    {
        InitMenu();
        startGameButton.onClick.AddListener(StartGame);
        gameController.OnGameEnd += InitMenu;
    }

    private void StartGame()
    {
        menuUI.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(true);
        gameController.StartGame();
    }

    private void InitMenu()
    {
        menuUI.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveListener(StartGame);
        gameController.OnGameEnd -= InitMenu;
    }
}
