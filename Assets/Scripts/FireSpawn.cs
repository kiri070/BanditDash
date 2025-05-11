using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    public GameObject FirePrefab;
    public Transform FirePoint;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーが触れたらオブジェクトをスポーン
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(FirePrefab, FirePoint.position, Quaternion.identity);
        }

    }
}
