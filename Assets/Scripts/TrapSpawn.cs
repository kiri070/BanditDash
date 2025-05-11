using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;  // �g���b�v�̃v���n�u
    public Transform spawnPoint;    // �g���b�v�̃X�|�[���|�C���g

    AudioSource audio;
    public AudioClip TrapSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �n�ʂɐG�ꂽ��
        if (collision.CompareTag("Player"))
        {
            audio.PlayOneShot(TrapSound, 2f);
            
            SpawnTrap();
        }
    }

    void SpawnTrap()
    {
        // �g���b�v�𐶐�
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}