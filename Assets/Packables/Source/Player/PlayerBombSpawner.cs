using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBombSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject _bombPrefab;


    [SerializeField]
    Tilemap _tileMap;

    public int _bombQuantity = 0;
    public int _maxBombs = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _bombQuantity < _maxBombs)
        {
            CreateBomb();            
        }

    }

    private void CreateBomb()
    {
        if (_bombPrefab == null)
        {
            Debug.LogError("BombPrefab Null");
            return;
        }

        Vector3Int cell = _tileMap.WorldToCell(transform.position);
        Vector3 cellCenterPos = _tileMap.GetCellCenterWorld(cell);
        GameObject newBomb = Instantiate(_bombPrefab, cellCenterPos, Quaternion.identity);
        newBomb.GetComponent<Bomb>().Init(this);
        _bombQuantity += 1;
    }
}
