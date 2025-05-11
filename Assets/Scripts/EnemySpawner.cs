using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // 敵のプレハブ
    public Transform spawnPoint;    // 敵のスポーンポイント

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 地面に触れたら
        if (collision.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // 敵を生成
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
