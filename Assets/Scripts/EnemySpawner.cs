using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // �G�̃v���n�u
    public Transform spawnPoint;    // �G�̃X�|�[���|�C���g

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �n�ʂɐG�ꂽ��
        if (collision.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // �G�𐶐�
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
