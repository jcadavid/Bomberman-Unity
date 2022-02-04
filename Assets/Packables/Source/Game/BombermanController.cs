using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
 enum gameStatus
{
    menuPrincipal,
    inGame,
    gameOver,
    OnVictory,
    Credits
}
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
    [SerializeField]
    public GameObject _portalPrefab;

    [SerializeField]
    public GameObject _gridPrefab;

    public int seed;
    [SerializeField]

    public int _maxEnemies;
    [SerializeField] 
    public int _maxEnemies2;
    [SerializeField]
    public GameObject _enemy;
    public GameObject _enemy2;
    public int currentEnemies = 0;
    public float _probabilityDestructableWall = 0.4f;
    public float _probabilityWall = 0.03f;
    public float _probabilityEnemy = 0.05f;
    public float _probabilityEnemy2 = 0.02f;
    List<Player> _players = new List<Player>();

    public bool hasPortalSpawn;

    public float _probabilityPowerUp = 0.01f;
    public float _probabilityPortal = 0.01f;

    public int _numberOfDestruyableBlocks;

    private GameObject _currentPlayer;
    private GameObject _currentPortal;

    [SerializeField]
    GameObject _playerPrefab;

    [SerializeField]
    GameObject _powerUpPrefab;
    [SerializeField]
    public GameObject _bombPrefab;
    private bool isFlamePowerUpActive;
     gameStatus status;
    GameObject _grid;

    int score;
    void Start()
    {
        gridGeneration = GetComponent<GridGeneration>();
        BombermanEvent.onPlayerDie += onPlayerDie;
        BombermanEvent.onBlockDestroyed += onBlockDestroyed;
        BombermanEvent.OnGameStartEvent += onGameStart;
        BombermanEvent.onEnemyDeath += onEnemyDeath;
        status = gameStatus.menuPrincipal;
        score = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (status)
            {
                case gameStatus.menuPrincipal:
                    {
                        BombermanEvent.OnGameStartEvent?.Invoke();
                        BombermanEvent.OnScoreUpdatedEvent?.Invoke(score);
                        status = gameStatus.inGame;
                        break;
                    }
                case gameStatus.gameOver:
                    {
                        BombermanEvent.OnGameOverMenuEvent?.Invoke();
                        status = gameStatus.menuPrincipal;
                        break;
                    }
                case gameStatus.OnVictory:
                    {
                        BombermanEvent.OnVictoryMenuEvent?.Invoke();
                        status = gameStatus.menuPrincipal;
                        break;
                    }

            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (status)
            {
                case gameStatus.menuPrincipal:
                    {
                        BombermanEvent.OnExitGameEvent?.Invoke();
                        status = gameStatus.Credits;
                        break;
                    }
                case gameStatus.inGame:
                    {
                        destroyScene();
                        status = gameStatus.menuPrincipal;
                        BombermanEvent.OnExitMenuEvent?.Invoke();
                        break;
                    }

            }
        }
    }

    public void onGameStart()
    {
        _grid = Instantiate(_gridPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        _tileMap = _grid.transform.Find("Blocks").GetComponent<Tilemap>();
        gridGeneration.Init();
        _currentPlayer = Instantiate(_playerPrefab, new Vector3(-8.5f, 6.5f, 0), Quaternion.identity);
    }

    public void onPlayerDie(Player player)
    {        
        status = gameStatus.gameOver;
        BombermanEvent.OnGameOverEvent?.Invoke(score);
        destroyScene();
    }

    public void destroyScene()
    {
        destroyPowerUp();
        destroyEnemies();
        Destroy(_currentPlayer);
        Destroy(_currentPortal);
        Destroy(_grid.gameObject);
        _grid = null;
        _tileMap = null;
        _currentPlayer = null;
        _currentPortal = null;
        hasPortalSpawn = false;
        currentEnemies = 0;
        _radiusExplotion = 2;
        score = 0;
    }

    private void onBlockDestroyed(Vector3Int tilePosition)
    {
        _numberOfDestruyableBlocks -= 1;
        score += 20;
        BombermanEvent.OnScoreUpdatedEvent?.Invoke(score);
        Vector3 pos = _tileMap.GetCellCenterWorld(tilePosition);
        if(!hasPortalSpawn){
            SpawnPortal(pos);
        }        
        else
        {
            float rand = Random.Range(0, 100f);
            rand = rand/100;
            if (rand < _probabilityPowerUp )
            {
                SpawnPowerUp(pos);
            }            
        }

    }

    private void SpawnPowerUp(Vector3 pos)
    {
        Instantiate(_powerUpPrefab, pos, Quaternion.identity);
    }

    public void addRadiusExplotion()
    {
        if (!isFlamePowerUpActive)
        {
            isFlamePowerUpActive = true;
            _radiusExplotion += 1;
        }
    }

    private void SpawnPortal(Vector3 pos)
    {
        if (!hasPortalSpawn)
        {
            hasPortalSpawn = true;
            _currentPortal = Instantiate(_portalPrefab, pos, Quaternion.identity);
        }
    }



    public void onEnemyDeath(int _additionalScore)
    {
        currentEnemies -= 1;
        score += _additionalScore;
        BombermanEvent.OnScoreUpdatedEvent?.Invoke(score);
    }

    public void checkEnemies(Vector3 portalPosition, Collider2D player)
    {
        if (currentEnemies <= 0)
        {
            player.GetComponent<PlayerMovement>().disappearAnimation(portalPosition);
            Invoke("OnWin", 2.5f);

        }
    }

    private void OnWin()
    {        
        status = gameStatus.OnVictory;
        BombermanEvent.OnVictoryEvent?.Invoke(score);
        destroyScene();
    }



    private void destroyPowerUp()
    {
        PowerUp[] PowerUps = FindObjectsOfType<PowerUp>();
        foreach (PowerUp powerUp in PowerUps)
        {
            Destroy(powerUp.gameObject);
        }
    }

    private void destroyEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }




}
