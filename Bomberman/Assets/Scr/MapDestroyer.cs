using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Tile solidBlock;
    [SerializeField]
    private Tile explodableBlock;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject speedPowerUpPrefab;

    public void Explosion(Vector2 worldPos){
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplosionCell(originCell);
        if (ExplosionCell(originCell+ new Vector3Int(1,0,0))){
            ExplosionCell(originCell+ new Vector3Int(2,0,0));
        }
        if (ExplosionCell(originCell+ new Vector3Int(0,1,0))){
            ExplosionCell(originCell+ new Vector3Int(0,2,0));
        }
        if (ExplosionCell(originCell+ new Vector3Int(-1,0,0))){
            ExplosionCell(originCell+ new Vector3Int(-2,0,0));
        }
        if( ExplosionCell(originCell+ new Vector3Int(0,-1,0))){
            ExplosionCell(originCell+ new Vector3Int(0,-2,0));
        }
    }

    public bool ExplosionCell (Vector3Int cell){
        
        Tile tile = tilemap.GetTile<Tile>(cell);
        Debug.Log(tile + " - " + explodableBlock);
        Debug.Log(tile == explodableBlock);
       
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        if (tile == solidBlock){
            Debug.Log("Solid");
            return false;
        }
        if (tile == explodableBlock){
            Debug.Log("No Solid");
            tilemap.SetTile(cell, null);
            createPowerUp(cell);
        }
        else{
            tilemap.SetTile(cell,null);
        }

        Instantiate(explosionPrefab, pos, Quaternion.identity);
        return true;

    }

    private void createPowerUp(Vector3Int pos)
    {
        float random = Random.value;
        if (random <= 0.5)
        {
            Vector3 newPosi = tilemap.GetCellCenterWorld(pos);
            Instantiate(speedPowerUpPrefab, newPosi, Quaternion.identity);
        }
    }
}
