using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour {

	public Tilemap _tileMap;

	public Tile _wallTile;
	public Tile _destructibleTile;

	public GameObject explosionPrefab;
    public int _radiusExplotion = 3;

	
	

	public void Explode(Vector2 worldPos)
	{
		Vector3Int originCell = _tileMap.WorldToCell(worldPos);
		bool continueYPositive = true;
		bool continueYNegative = true;
		bool continueXPositive = true;
		bool continueXNegative = true;
		
		ExplodeCell(originCell);
		for (int i = 1; i < _radiusExplotion; i++)
        {
            if(continueYPositive){
				continueYPositive = ExplodeCell(originCell + new Vector3Int(0,1,0));
			}
			if(continueYNegative){
				continueYPositive = ExplodeCell(originCell + new Vector3Int(0,-1,0));
			}
			if(continueXPositive){
				continueYPositive = ExplodeCell(originCell + new Vector3Int(1,0,0));
			}
			if(continueXNegative){
				continueYPositive = ExplodeCell(originCell + new Vector3Int(-1,0,0));
			}
        }

	}

	bool ExplodeCell (Vector3Int cell)
	{
		Tile tile = _tileMap.GetTile<Tile>(cell);

		if (tile == _wallTile)
		{
			return false;
		}

		if (tile == _destructibleTile)
		{
			_tileMap.SetTile(cell, null);
		}

		Vector3 pos = _tileMap.GetCellCenterWorld(cell);
		Instantiate(explosionPrefab, pos, Quaternion.identity);

		return true;
	}

}

