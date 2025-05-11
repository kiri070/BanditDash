using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float fixedY;  // �J�������Œ肳���y���̒l
    public float xOffset;

    void Update()
    {
        if (player != null)
        {
            float playerX = player.position.x;

            // �J������y���W���Œ肵�A�v���C���[��x���W�ɒǏ]
            transform.position = new Vector3(playerX - xOffset, fixedY, transform.position.z);
        }
    }
}


