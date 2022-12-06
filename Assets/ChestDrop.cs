using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDrop : MonoBehaviour
{
    [SerializeField] private GameObject _chestPrefab;
    [SerializeField] private EnemyStats _enemyStats;

    private void OnEnable()
    {
        _enemyStats.OnEnemyDied += DropChest;
    }

    private void OnDisable()
    {
        _enemyStats.OnEnemyDied -= DropChest;
    }

    public void DropChest() // TODO: add a rarity system to the chest drop based on the enemy type. maybe an enum can be used
    {
        Instantiate(_chestPrefab, transform.position + (transform.up * 2), Quaternion.identity);
        Debug.Log($"Chest dropped! from {gameObject.name}");
    }
}
