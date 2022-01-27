using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGeneration : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap _tileMap;
    public string stringSeed;
    public Tile _destructibleTile;
    public Tile _wallTile;
    public bool useStringSeed;
    public int seed;
    [SerializeField]
    public int _minEnemies;
    [SerializeField]
    public int _maxEnemies;
    [SerializeField]
    GameObject _enemy;

    [SerializeField]
    List<Vector3Int> bannedPositions = new List<Vector3Int>();
    float probabilityDestructableWall = 0.4f;
    float probabilityEnemy = 0.05f;

    void Start()
    {
        if (useStringSeed)
        {
            seed = stringSeed.GetHashCode();
        }
        else
        {
            seed = Random.Range(0, 999999);
        }
        Random.InitState(seed);
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
                if (_tileMap.GetTile(pos) != _wallTile & !isOnBannedPosition(pos))
                {
                    if (rand < probabilityDestructableWall)
                    {
                        _tileMap.SetTile(pos, _destructibleTile);
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
                if (_tileMap.GetTile(pos) != _wallTile & _tileMap.GetTile(pos) != _destructibleTile)
                {
                    if (rand < probabilityEnemy)
                    {
                        Instantiate(_enemy,_tileMap.GetCellCenterWorld(pos),Quaternion.identity);
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
