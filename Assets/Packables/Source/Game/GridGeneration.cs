using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGeneration : MonoBehaviour
{
    [SerializeField]
    List<Vector3Int> bannedPositions = new List<Vector3Int>();
    List<Vector3Int> enemiesPosition = new List<Vector3Int>();
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
        generateAdditionalSolidBlocks();
        bombermanController.currentEnemies = generateEnemies() + generateEnemies2();        
    }

    private void generateGrid()
    {
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -9; x <= 9; x++)
            {
                float rand = Random.Range(0f, 1f);
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (bombermanController._tileMap.GetTile(pos) != bombermanController._wallTile & !isOnBannedPosition(pos))
                {
                    if (rand < bombermanController._probabilityDestructableWall)
                    {
                        bombermanController._tileMap.SetTile(pos, bombermanController._destructibleTile);
                        bombermanController._numberOfDestruyableBlocks +=1;
                    }
                }
            }
        }
    }

    private void generateAdditionalSolidBlocks()
    {
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -9; x <= 9; x++)
            {
                float rand = Random.Range(0f, 1f);
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (bombermanController._tileMap.GetTile(pos) != bombermanController._wallTile & !isOnBannedPosition(pos))
                {
                    if (rand < bombermanController._probabilityWall)
                    {
                        bombermanController._tileMap.SetTile(pos, bombermanController._wallTile);
                    }
                }
            }
        }
    }

    int generateEnemies()
    {
        int countEnemy =0;
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -9; x <= 9; x++)
            {
                float rand = Random.Range(0f, 1f);
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (bombermanController._tileMap.GetTile(pos) != bombermanController._wallTile & bombermanController._tileMap.GetTile(pos) != bombermanController._destructibleTile)
                {
                    if(countEnemy == bombermanController._maxEnemies){
                        return countEnemy;
                    }
                    else if (rand < bombermanController._probabilityEnemy & !isOtherEnemy(pos) & !isOnBannedPosition(pos))
                    {
                        enemiesPosition.Add(pos);
                        Instantiate(bombermanController._enemy,bombermanController._tileMap.GetCellCenterWorld(pos),Quaternion.identity);
                        countEnemy++;
                    }
                }
            }
        }
        return countEnemy;
    }
    int generateEnemies2(){
        int countEnemy2 =0;
        for (int y = -5; y <= 5; y++)
        {
            for (int x = -9; x <= 9; x++)
            {
                float rand = Random.Range(0f, 1f);
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (bombermanController._tileMap.GetTile(pos) != bombermanController._wallTile & bombermanController._tileMap.GetTile(pos) != bombermanController._destructibleTile)
                {
                    if(countEnemy2  == bombermanController._maxEnemies2){
                        return countEnemy2;
                    }
                    else if (rand < bombermanController._probabilityEnemy & !isOtherEnemy(pos) & !isOnBannedPosition(pos))
                    {
                        enemiesPosition.Add(pos);
                        Instantiate(bombermanController._enemy2,bombermanController._tileMap.GetCellCenterWorld(pos),Quaternion.identity);
                        countEnemy2++;
                    }
                }
            }
        }
        return countEnemy2;
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

    private bool isOtherEnemy(Vector3Int pos){
        foreach (Vector3Int enemyPos in enemiesPosition)
        {
            if (enemyPos == pos)
            {
                return true;
            }
        }
        return false;
    }

    
}
