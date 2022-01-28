

using UnityEngine;

public static class BombermanEvent
{
    public delegate void BlockDestroyed(Vector3Int tilePosition);
    public static BlockDestroyed onBlockDestroyed;
    public delegate void PlayerDie(Player player);
    public static PlayerDie onPlayerDie;
}
