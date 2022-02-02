using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour {

	BombermanController bombermanController;

	private void Start() {
		bombermanController = GetComponent<BombermanController>();
	}
	

	public void Explode(Vector2 worldPos)
	{
		Vector3Int originCell = bombermanController._tileMap.WorldToCell(worldPos);
		bool continueYPositive = true;
		bool continueYNegative = true;
		bool continueXPositive = true;
		bool continueXNegative = true;
		
		ExplodeCell(originCell);
		for (int i = 1; i < bombermanController._radiusExplotion; i++)
        {
            if(continueYPositive){
				continueYPositive = ExplodeCell(originCell + new Vector3Int(0,i,0));
			}
			if(continueYNegative){
				continueYNegative = ExplodeCell(originCell + new Vector3Int(0,-i,0));
			}
			if(continueXPositive){
				continueXPositive = ExplodeCell(originCell + new Vector3Int(i,0,0));
			}
			if(continueXNegative){
				continueXNegative = ExplodeCell(originCell + new Vector3Int(-i,0,0));
			}
        }

	}

	bool ExplodeCell (Vector3Int cell)
	{
		Tile tile = bombermanController._tileMap.GetTile<Tile>(cell);
	
		if (tile == bombermanController._wallTile)
		{
			return false;
		}

		if (tile == bombermanController._destructibleTile)
		{
			bombermanController._tileMap.SetTile(cell, null);
			BombermanEvent.onBlockDestroyed?.Invoke(cell);
			return false;
			
		}
		else{
			Vector3 pos = bombermanController._tileMap.GetCellCenterWorld(cell);
		Instantiate(bombermanController.explosionPrefab, pos, Quaternion.identity);

		return true;
		}	
		
	}

}

