

using UnityEngine;

public static class BombermanEvent
{
    public delegate void BlockDestroyed(Vector3Int tilePosition);
    public static BlockDestroyed onBlockDestroyed;
    public delegate void PlayerDie(Player player);
    public static PlayerDie onPlayerDie;
    public delegate void BoostPowerUp();
    public static BoostPowerUp onBoostPowerUp;

    public delegate void AdditionalBombPowerUp();
    public static AdditionalBombPowerUp onAdditionalBombPowerUp;
    public delegate void AdditionalFlamePowerUp();
    public static AdditionalFlamePowerUp onAdditionalFlamePowerUp;

    // Game UI Settings Events
    public delegate void ScoreUpdatedAction(int score, int totalScore);
    public static ScoreUpdatedAction OnScoreUpdatedEvent;

    public delegate void LevelUpdatedAction(int level);
    public static LevelUpdatedAction OnLevelUpdatedEvent;

    public delegate void LifeUpdatedAction(int life);
    public static LifeUpdatedAction OnLifeUpdatedEvent;

    // Screen UI Changes
    public delegate void GameStartAction();
    public static GameStartAction OnGameStartEvent;

    public delegate void GameOverAction(int FinalScore);
    public static GameOverAction OnGameOverEvent;

    public delegate void VictoryAction(int FinalScore);
    public static VictoryAction OnVictoryEvent;

    public delegate void GameOverMenuAction();
    public static GameOverMenuAction OnGameOverMenuEvent;

    public delegate void VictoryMenuAction();
    public static VictoryMenuAction OnVictoryMenuEvent;

    public delegate void ExitMenuAction();
    public static ExitMenuAction OnExitMenuEvent;

}
