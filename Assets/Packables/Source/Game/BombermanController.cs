using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombermanController : MonoBehaviour
{

    GridGeneration gridGeneration;
    MapDestroyer mapDestroyer;
    public Tilemap _tileMap;
    public Tile _wallTile;
    public Tile _destructibleTile;
    public GameObject explosionPrefab;
    public int _radiusExplotion;
    public string stringSeed;
    public bool useStringSeed;
    public int seed;
    [SerializeField]
    public int _minEnemies;
    [SerializeField]
    public int _maxEnemies;
    [SerializeField]
    public GameObject _enemy;
    public int currentEnemies = 0;
    public float probabilityDestructableWall = 0.4f;
    public float probabilityEnemy = 0.05f;

    void Start()
    {
        gridGeneration = GetComponent<GridGeneration>();
        gridGeneration.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
