using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;  // トラップのプレハブ
    public Transform spawnPoint;    // トラップのスポーンポイント

    AudioSource audio;
    public AudioClip TrapSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 地面に触れたら
        if (collision.CompareTag("Player"))
        {
            audio.PlayOneShot(TrapSound, 2f);
            
            SpawnTrap();
        }
    }

    void SpawnTrap()
    {
        // トラップを生成
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}