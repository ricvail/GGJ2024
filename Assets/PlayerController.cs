using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    public float speed;
    public Transform target;
    public Rigidbody throwable;
    public Transform throwPoint;

    public Rigidbody throwablePrefab;
    public LineRenderer lineRenderer;
    
    public float strenght;
    public float angle;
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
        
        
        
        
        if (Input.GetMouseButtonDown(0))
        {
            throwable = Instantiate(throwablePrefab, throwPoint);
            drawTrajectory();
        }
        if (Input.GetMouseButton(0))
        {
            drawTrajectory();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            //Rigidbody throwable = Instantiate(this.throwable, this.throwable.position, Quaternion.identity);
            
            Vector3 dir = getDirection();
            throwable.isKinematic = false;
            throwable.useGravity = true;
            throwable.transform.SetParent(null, true);
            throwable.AddForce(dir * strenght, ForceMode.Impulse);
            lineRenderer.enabled = false;
        }
    }

    private void drawTrajectory()
    {
        Vector3 startVelocity = getDirection() * strenght / throwable.mass;
        lineRenderer.enabled = true;
        int i = 0;
        lineRenderer.positionCount = Mathf.CeilToInt(5 / 0.1f) + 2;
        lineRenderer.SetPosition(i, throwPoint.position);
        for (float t = 0; t < 5; t += 0.1f)
        {
            i++;
            Vector3 point = throwPoint.position + t * startVelocity;
            point.y = throwPoint.position.y + startVelocity.y * t + (Physics.gravity.y / 2f * t * t);
            lineRenderer.SetPosition(i, point);
            Vector3 lastPos = lineRenderer.GetPosition(i - 1);
            
        }
    }
    
    
    private Vector3 getDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2000, LayerMask.GetMask("RaycastCylinder")))
        {
            Vector3 direction = hit.point;
            Debug.DrawLine(Camera.main.transform.position, direction, Color.blue);
            direction -= transform.position;
            direction.Normalize();
            direction.y = Mathf.Cos(angle);
            direction.Normalize();
            return direction;
        }

        return Vector3.zero;
    }
}
