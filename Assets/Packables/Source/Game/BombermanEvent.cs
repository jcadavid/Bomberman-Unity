

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
}
