using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBombSpawner : MonoBehaviour
{

    public int _bombQuantity = 0;
    public int _maxBombs = 1;
    BombermanController _bombermanController;

    // Start is called before the first frame update
    void Start()
    {
        _bombermanController = FindObjectOfType<BombermanController>();

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
        if (_bombermanController._bombPrefab == null)
        {
            Debug.LogError("BombPrefab Null");
            return;
        }

        Vector3Int cell = _bombermanController._tileMap.WorldToCell(transform.position);
        Vector3 cellCenterPos = _bombermanController._tileMap.GetCellCenterWorld(cell);
        GameObject newBomb = Instantiate(_bombermanController._bombPrefab, cellCenterPos, Quaternion.identity);
        newBomb.GetComponent<Bomb>().Init(this);
        _bombQuantity += 1;
    }
}
