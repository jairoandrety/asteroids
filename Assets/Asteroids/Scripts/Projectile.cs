using System;
using System.Collections;
using UnityEngine;

public enum ProjectileType
{
    Normal = 0,
    Guide = 1,
    TargetPoint = 2
}

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private ProjectileSetup setup;

    [SerializeField] private GameObject graphic;

    private IEnumerator moveCoroutine = null;

    private bool isMoving = false;
    private float timeLife = 0;
    private float timeCurve = 0; 

    #region Unity Behavior

    private void Update()
    {
        if (timeCurve >= 1)
            timeCurve = 0;

        timeCurve += Time.deltaTime;
        graphic.transform.localPosition = new Vector3(setup.curve.Evaluate(timeCurve), 0, 0);

        CalculateBounds();
    }
    #endregion

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

    public void SetProjectile(ProjectileSetup setup)
    {
        this.setup = setup;
    }

    public void Active(Vector3 position, Quaternion rotation)
    {
        timeLife = 0;
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    public void Impact()
    {
        Destroy();
    }

    public void Move()
    {
        isMoving = true;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = Moving();
        StartCoroutine(moveCoroutine);
    }

    public IEnumerator Moving()
    {
        while (isMoving)
        {
            transform.Translate(Vector3.up * (setup.speed) * Time.deltaTime);

            if (timeLife >= 5f)
                Destroy();

            timeLife += Time.deltaTime;
            yield return null;
        }
    }
}