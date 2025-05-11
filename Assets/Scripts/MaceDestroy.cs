using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceDestroy : MonoBehaviour
{
    AudioSource audio;
    public AudioClip DestroySound;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy", 5f);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2d(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hakai"))
        {
            audio.PlayOneShot(DestroySound);
        }

    }
}
