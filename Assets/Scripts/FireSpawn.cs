using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    public GameObject FirePrefab;
    public Transform FirePoint;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�v���C���[���G�ꂽ��I�u�W�F�N�g���X�|�[��
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(FirePrefab, FirePoint.position, Quaternion.identity);
        }

    }
}
