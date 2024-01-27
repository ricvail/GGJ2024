using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    public float speed;
    public Transform target;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movDirection = transform.position - target.position;
        movDirection.y = 0;
        movDirection.Normalize();
        Vector3 movPerp = Vector3.Cross(movDirection, Vector3.up);
        Vector3 mov = - Input.GetAxis("Vertical") * movDirection + Input.GetAxis("Horizontal") * movPerp;
        mov.Normalize();
        _characterController.Move(mov * (speed * Time.deltaTime));
    }
}
