using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    private const string FINAL_SCORE_TEXT_TEMPLATE = "Puntaje Total: {0} pts";

    private CanvasGroup _canvasGroup;

    private TextMeshProUGUI _scoreText;

    void Start()
    {
        _scoreText = transform.Find("PuntajeFinal").GetComponent<TextMeshProUGUI>();

        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;

        BombermanEvent.OnGameOverEvent += OnGameOver;
        BombermanEvent.OnGameOverMenuEvent += OnMainMenu;
    }

    private void OnDestroy()
    {
        BombermanEvent.OnGameOverEvent -= OnGameOver;
        BombermanEvent.OnGameOverMenuEvent -= OnMainMenu;
    }

    private void OnGameOver(int FinalScore)
    {
        _scoreText.text = string.Format(FINAL_SCORE_TEXT_TEMPLATE, FinalScore);
        _canvasGroup.alpha = 1;
    }

    private void OnMainMenu()
    {
        _canvasGroup.alpha = 0;
    }

}
