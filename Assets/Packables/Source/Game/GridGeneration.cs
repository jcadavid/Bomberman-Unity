using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGeneration : MonoBehaviour
{
    [SerializeField]
    List<Vector3Int> bannedPositions = new List<Vector3Int>();
    BombermanController bombermanController;

    public void Init()
    {
        bombermanController = GetComponent<BombermanController>();
        if (bombermanController.useStringSeed)
        {
            bombermanController.seed = bombermanController.stringSeed.GetHashCode();
        }
        else
        {
            bombermanController.seed = Random.Range(0, 999999);
        }
        Random.InitState(bombermanController.seed);
        generateGrid();
        generateEnemies();
    }

    private void generateGrid()
    {
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -6; x <= 6; x++)
            {
                float rand = Random.Range(0f, 1f);
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (bombermanController._tileMap.GetTile(pos) != bombermanController._wallTile & !isOnBannedPosition(pos))
                {
                    if (rand < bombermanController.probabilityDestructableWall)
                    {
                        bombermanController._tileMap.SetTile(pos, bombermanController._destructibleTile);
                    }
                }
            }
        }
    }

    void generateEnemies()
    {
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -6; x <= 6; x++)
            {
                float rand = Random.Range(0f, 1f);
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (bombermanController._tileMap.GetTile(pos) != bombermanController._wallTile & bombermanController._tileMap.GetTile(pos) != bombermanController._destructibleTile)
                {
                    if(bombermanController.currentEnemies == bombermanController._maxEnemies){
                        return;
                    }
                    else if (rand < bombermanController.probabilityEnemy)
                    {
                        Instantiate(bombermanController._enemy,bombermanController._tileMap.GetCellCenterWorld(pos),Quaternion.identity);
                        bombermanController.currentEnemies+=1;
                    }
                }
            }
        }
    }

    private bool isOnBannedPosition(Vector3Int pos)
    {
        foreach (Vector3Int banPos in bannedPositions)
        {
            if (banPos == pos)
            {
                return true;
            }
        }
        return false;
    }
}
