using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    void Start()
    {
        BombermanEvent.OnStartGameEvent += OnStartGame;
    }

    private void OnDestroy()
    {
        BombermanEvent.OnStartGameEvent -= OnStartGame;
    }

    private void OnStartGame()
    {
        _canvasGroup.alpha = 1;
        Invoke("start", 5);
    }

    private void start()
    {
        BombermanEvent.OnStartMenuEvent?.Invoke();
    }

}
