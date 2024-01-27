using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform focus;
    public float cameraHeight;
    public float wallDistance;

    private void LateUpdate()
    {
        Vector3 cameraOffset = player.position - focus.position;
        Vector3 cameraPosition = player.position + cameraOffset;
        cameraPosition.y = cameraHeight;
        int length = Physics.OverlapSphere(cameraPosition, wallDistance).Length;
        if (length >= 1)
        {
            Camera.main.transform.position = cameraPosition;
        }
        else
        {
            RaycastHit hit;
            Ray ray = new Ray(cameraPosition,  focus.position - cameraPosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CameraSafeArea"), QueryTriggerInteraction.Collide))
            {
                Camera.main.transform.position = hit.point + (hit.point-cameraPosition).normalized*wallDistance;
            }
        }
        Camera.main.transform.LookAt(player.position + (focus.position-player.position)/2);
    }

}
