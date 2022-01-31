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
    public float _probabilityDestructableWall = 0.4f;
    public float _probabilityWall = 0.03f;
    public float _probabilityEnemy = 0.05f;
    List<Player> _players = new List<Player>();

    public bool hasPortalSpawn;

    public float _probabilityPowerUp = 0.01f;
    public float _probabilityPortal = 0.01f;

    public int _numberOfDestruyableBlocks;
    [SerializeField]
    GameObject _powerUpPrefab;
    [SerializeField]
    public GameObject _bombPrefab;
    void Start()
    {
        gridGeneration = GetComponent<GridGeneration>();
        gridGeneration.Init();
        BombermanEvent.onPlayerDie += onPlayerDie;
        BombermanEvent.onBlockDestroyed += onBlockDestroyed;

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void onPlayerDie(Player player)
    {
        
            Debug.Log("GAMEOVER");
        
    }

    private void onBlockDestroyed(Vector3Int tilePosition)
    {
        _numberOfDestruyableBlocks -= 1;
        Vector3 pos = _tileMap.GetCellCenterWorld(tilePosition);
        if (_numberOfDestruyableBlocks == 0 & !hasPortalSpawn)
        {
            hasPortalSpawn = true;
        }
        else
        {
            float rand = Random.Range(0, 1);
            if (rand < _probabilityPortal & !hasPortalSpawn)
            {
                SpawnPowerUp(pos);
            }
            else if(rand < _probabilityPowerUp)
            {
                SpawnPowerUp(pos);
            }
            _probabilityPortal += _probabilityPortal;
        }

    }

    private void SpawnPowerUp(Vector3 pos)
    {
        Instantiate(_powerUpPrefab,pos,Quaternion.identity);
    }

    public void addRadiusExplotion()
    {
        _radiusExplotion +=1;
    }

    private void SpawnPortal(Vector3 pos)
    {

    }
}
