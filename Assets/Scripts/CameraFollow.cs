using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float fixedY;  // カメラが固定されるy軸の値
    public float xOffset;

    void Update()
    {
        if (player != null)
        {
            float playerX = player.position.x;

            // カメラのy座標を固定し、プレイヤーのx座標に追従
            transform.position = new Vector3(playerX - xOffset, fixedY, transform.position.z);
        }
    }
}


