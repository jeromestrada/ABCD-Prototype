using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private int _maxTerrainCount = 30;
    public int _currentTerrainCount;
    
    public bool HasRoom => _currentTerrainCount < _maxTerrainCount;

    private void Start()
    {
        _currentTerrainCount = 0;
    }
    private void OnEnable()
    {
        TerrainSpawner.OnTerrainSpawned += IncrementCount;
    }

    private void OnDisable()
    {
        TerrainSpawner.OnTerrainSpawned -= IncrementCount;
    }

    public void IncrementCount()
    {
        if( _currentTerrainCount < _maxTerrainCount ) _currentTerrainCount++;
    }
}
