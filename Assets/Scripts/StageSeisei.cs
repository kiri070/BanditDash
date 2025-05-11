using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSeisei : MonoBehaviour
{
    public GameObject Stage;
    public Transform SpawnPoint;

    AudioSource audio;
    public AudioClip SeiseiSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ínñ Ç…êGÇÍÇΩÇÁ
        if (collision.CompareTag("Player"))
        {
            audio.PlayOneShot(SeiseiSound);
            Instantiate(Stage, SpawnPoint.position, Quaternion.identity);
        }
    }
}
