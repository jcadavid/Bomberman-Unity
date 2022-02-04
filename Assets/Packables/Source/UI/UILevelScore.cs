using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevelScore : MonoBehaviour
{
    private const string SCORE_TEXT_TEMPLATE = "Puntaje: {0} pts";
    private const string LEVEL_TEXT_TEMPLATE = "Nivel: {0}";
    private const string LIFE_TEXT_TEMPLATE = "Vidas Restantes: {0}";


    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _lifeText;

    private CanvasGroup _canvasGroup;

    void Start()
    {
        _scoreText = transform.Find("Puntaje").GetComponent<TextMeshProUGUI>();
        _levelText = transform.Find("Nivel").GetComponent<TextMeshProUGUI>();
        _lifeText = transform.Find("Vidas").GetComponent<TextMeshProUGUI>();

        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;

        BombermanEvent.OnScoreUpdatedEvent += OnScoreUpdated;
        BombermanEvent.OnLevelUpdatedEvent += OnLevelUpdated;
        BombermanEvent.OnLifeUpdatedEvent += OnLifeUpdated;
        BombermanEvent.OnGameStartEvent += OnGameStart;
        BombermanEvent.OnGameOverEvent += OnGameOver;
        BombermanEvent.OnExitMenuEvent += OnExitMenu;
    }

    private void OnDestroy()
    {
        BombermanEvent.OnScoreUpdatedEvent -= OnScoreUpdated;
        BombermanEvent.OnLevelUpdatedEvent -= OnLevelUpdated;
        BombermanEvent.OnLifeUpdatedEvent -= OnLifeUpdated;
        BombermanEvent.OnGameStartEvent -= OnGameStart;
        BombermanEvent.OnGameOverEvent -= OnGameOver;
        BombermanEvent.OnExitMenuEvent -= OnExitMenu;
    }

    private void OnScoreUpdated(int score, int totalScore)
    {
        _scoreText.text = string.Format(SCORE_TEXT_TEMPLATE, totalScore);
    }

    private void OnLevelUpdated(int level)
    {
        _levelText.text = string.Format(LEVEL_TEXT_TEMPLATE, level);
    }

    private void OnLifeUpdated(int life)
    {
        _lifeText.text = string.Format(LIFE_TEXT_TEMPLATE, life);
    }

    private void OnGameStart()
    {
        _canvasGroup.alpha = 1;
    }

    private void OnGameOver(int total)
    {
        _canvasGroup.alpha = 0;
    }

    private void OnExitMenu()
    {
        _canvasGroup.alpha = 0;
    }
}
