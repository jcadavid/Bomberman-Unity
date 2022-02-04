using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreditos : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    void Start()
    {
        BombermanEvent.OnExitGameEvent += OnExitGame;
    }

    private void OnDestroy()
    {
        BombermanEvent.OnExitGameEvent -= OnExitGame;
    }

    private void OnExitGame()
    {
        _canvasGroup.alpha = 1;
        Invoke("quit", 5);
    }

    private void quit()
    {
        Application.Quit();
    }
}
