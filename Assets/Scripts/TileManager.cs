using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<FallingTile> tiles;
    [SerializeField] private float interval = 2f;

    void Start()
    {
        StartCoroutine(DropTilesRoutine());
    }

    private IEnumerator DropTilesRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(interval);

            FallingTile tileToDrop = GetRandomAvailableTile();
            interval = Mathf.Max(0.8f, interval - 0.05f);
            if (tileToDrop != null)
            {
                tileToDrop.TriggerFall();
            }
        }
    }

    private FallingTile GetRandomAvailableTile()
    {
        List<FallingTile> availableTiles = new List<FallingTile>();

        foreach (FallingTile tile in tiles)
        {
            if (tile != null && !tile.IsFalling())
            {
                availableTiles.Add(tile);
            }
        }

        if (availableTiles.Count == 0) return null;

        int randomIndex = Random.Range(0, availableTiles.Count);
        return availableTiles[randomIndex];
    }
}