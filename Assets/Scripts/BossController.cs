using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //public Camera mainCamera;
    //public float padding = 1.0f; // ボスが画面端からどれ

    //private void Update()
    //{
    //    if (mainCamera == null)
    //    {
    //        Debug.LogError("Main Cameraがアサインされていません。");
    //        return;
    //    }

    //    Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);
    //    Vector3 newPosition = transform.position;

    //    if (screenPoint.x < padding)
    //    {
    //        newPosition.x = mainCamera.ScreenToWorldPoint(new Vector3(padding, 0, 0)).x;
    //    }
    //    else if (screenPoint.x > Screen.width - padding)
    //    {
    //        newPosition.x = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - padding, 0, 0)).x;
    //    }

    //    if (screenPoint.y < padding)
    //    {
    //        newPosition.y = mainCamera.ScreenToWorldPoint(new Vector3(0, padding, 0)).y;
    //    }
    //    else if (screenPoint.y > Screen.height - padding)
    //    {
    //        newPosition.y = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height - padding, 0)).y;
    //    }

    //    transform.position = newPosition;
    //}

    public float padding = 2.0f; // ボスが画面端からどれだけ離れるか

    private void Update()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Cameraが見つかりません。");
            return;
        }

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 newPosition = transform.position;

        if (screenPoint.x < padding)
        {
            newPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(padding, 0, 0)).x;
        }
        else if (screenPoint.x > Screen.width - padding)
        {
            newPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - padding, 0, 0)).x;
        }

        if (screenPoint.y < padding)
        {
            newPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(0, padding, 0)).y;
        }
        else if (screenPoint.y > Screen.height - padding)
        {
            newPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - padding, 0)).y;
        }

        transform.position = newPosition;
    }

}
