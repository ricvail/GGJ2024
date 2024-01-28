using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDemandSpawner : MonoBehaviour
{
    private Canvas canvas;
    public float timer;
    public Rigidbody throwablePrefab;
    public float range;
    private float cooldown;

    public bool destroyParentOnPickup;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        if (cooldown <= 0 && PlayerController.Instance.throwable == null)
        {
            float playerDistance = (PlayerController.Instance.transform.position - transform.position).magnitude;
            if (Input.GetKeyDown("e") && playerDistance <= range)
            {
                cooldown = timer;
                AudioManager.Instance.PlaySFX("Grab");
                PlayerController.Instance.throwable =
                    Instantiate(throwablePrefab, PlayerController.Instance.throwPoint);
                if (destroyParentOnPickup)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
            else
            {
                cooldown = 0;
                canvas.enabled = true;
            }
        }
        else
        {
            canvas.enabled = false;
            cooldown -= Time.deltaTime;
        }
    }
}
