using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool reappearOffscreen = false;
    [SerializeField] private float speed = 0;

    [SerializeField] private float horizontal = 0;
    [SerializeField] private float vertical = 0;
    [SerializeField] private Vector3 direction = Vector3.zero;

    [SerializeField] private float targetAngle = 0;
    [SerializeField] private float turnSmoothTime = 0;
    private float turnSmoothVelocity = 0;

    private void Update()
    {
        if (!canMove)
            return;

        direction = new Vector3(horizontal, vertical, 0f);
        bool isTurn = direction.magnitude >= 0.025f;
        if (direction.magnitude >= 0.025)
        {          
            targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime); 
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (reappearOffscreen)
            CalculateBounds();
        else
            StopBounds();
    }

    public void ChangeBoundsBehaviour()
    {
        reappearOffscreen = !reappearOffscreen;
    }

    private void CalculateBounds()
    {
        if (transform.position.y >= (Bounds.Instance.height / 2))
        {
            transform.position = new Vector3(transform.position.x, -Bounds.Instance.height / 2, transform.position.z);
        }
        else if (transform.position.y <= -(Bounds.Instance.height / 2))
        {
            transform.position = new Vector3(transform.position.x, Bounds.Instance.height / 2, transform.position.z);
        }
        else if (transform.position.x >= (Bounds.Instance.width / 2))
        {
            transform.position = new Vector3(-(Bounds.Instance.width / 2), transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -(Bounds.Instance.width / 2))
        {
            transform.position = new Vector3(Bounds.Instance.width / 2, transform.position.y, transform.position.z);
        }
    }

    private void StopBounds()
    {
        if (transform.position.y >= (Bounds.Instance.height / 2))
        {
            transform.position = new Vector3(transform.position.x, Bounds.Instance.height / 2, transform.position.z);
        }
        else if (transform.position.y <= -(Bounds.Instance.height / 2))
        {
            transform.position = new Vector3(transform.position.x, -(Bounds.Instance.height / 2), transform.position.z);
        }
        else if (transform.position.x >= (Bounds.Instance.width / 2))
        {
            transform.position = new Vector3((Bounds.Instance.width / 2), transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -(Bounds.Instance.width / 2))
        {
            transform.position = new Vector3(-(Bounds.Instance.width / 2), transform.position.y, transform.position.z);
        }
    }

    public void ActiveMovement(bool value)
    {
        canMove = value;
        SetDirection(0, 0);
    }

    public void SetDirection(float horizontal, float vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
    }
}
