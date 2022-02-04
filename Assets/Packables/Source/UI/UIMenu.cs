using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 1;

        BombermanEvent.OnGameStartEvent += OnGameStart;
        BombermanEvent.OnGameOverMenuEvent += OnMainMenu;
        BombermanEvent.OnVictoryMenuEvent += OnMainMenu;
        BombermanEvent.OnExitMenuEvent += OnMainMenu;
    }

    private void OnDestroy()
    {
        BombermanEvent.OnGameStartEvent -= OnGameStart;
        BombermanEvent.OnGameOverMenuEvent -= OnMainMenu;
        BombermanEvent.OnVictoryMenuEvent -= OnMainMenu;
        BombermanEvent.OnExitMenuEvent -= OnMainMenu;
    }

    private void OnGameStart()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnMainMenu()
    {
        _canvasGroup.alpha = 1;
    }
}
