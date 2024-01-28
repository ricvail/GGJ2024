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

    private bool isThrowing = false;


    public Animator animator;

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
        Vector3 mov = -Input.GetAxis("Vertical") * movDirection + Input.GetAxis("Horizontal") * movPerp;
        mov.Normalize();
        _characterController.SimpleMove(mov * (speed));
        

        if (mov == Vector3.zero)
        {
            animator.SetBool("AnyKeyDown", false);
        } 
        else
        {
            animator.SetBool("AnyKeyDown", true);
            transform.rotation = Quaternion.LookRotation(mov, Vector3.up);
        }


        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2000, LayerMask.GetMask("Player")))
            {
                isThrowing = true;
                throwable = Instantiate(throwablePrefab, throwPoint);
                drawTrajectory();
            }

        }

        if (Input.GetMouseButton(0) && isThrowing)
        {
            drawTrajectory();
        }

        if (Input.GetMouseButtonUp(0) && isThrowing)
        {
            //Rigidbody throwable = Instantiate(this.throwable, this.throwable.position, Quaternion.identity);

            Vector3 dir = getDirection();
            throwable.isKinematic = false;
            throwable.useGravity = true;
            throwable.transform.SetParent(null, true);
            throwable.AddForce(dir * strenght, ForceMode.Impulse);
            lineRenderer.enabled = false;
            isThrowing = false;
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
            if (Physics.Raycast(lastPos, (point - lastPos).normalized, out RaycastHit hit,
                    (point - lastPos).magnitude, LayerMask.GetMask("Default")) )
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                Debug.Log(hit.transform.gameObject.name);
                return;
            }
        }
    }


    private Vector3 getDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2000, LayerMask.GetMask("RaycastCylinder")))
        {
            Vector3 direction = hit.point;
            //Debug.DrawLine(Camera.main.transform.position, direction, Color.blue);
            direction -= transform.position;
            strenght = direction.magnitude;
            direction.Normalize();
            direction *= -1;
            direction.y = Mathf.Cos(angle);
            direction.Normalize();
            return direction;
        }

        return Vector3.zero;
    }
}