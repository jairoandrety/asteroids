using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Action OnDestroy;

    [SerializeField] private LayerMask layer;
    [SerializeField] private float speed = 0;
    [SerializeField] private float size = 0;

    [SerializeField] private GameObject graphic;
    [SerializeField] private Collider2D collider;

    private IEnumerator moveCoroutine = null;
    private bool isMoving = false;

    #region Unity Behavior
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log((layer.value & (1 << collision.gameObject.layer)) > 0);
        if ((layer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.SetActive(false);
            ActiveAsteroid(false);
            OnDestroy?.Invoke();
        }
    }
    #endregion

    public void CreateAsteroid(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        speed = 5 * UnityEngine.Random.value;
        size = 3 * UnityEngine.Random.value;
        transform.localScale = Vector3.one * size;

        ActiveAsteroid(true);
    }

    public void ActiveAsteroid(bool value)
    {
        gameObject.SetActive(value);
        graphic.SetActive(value);
        collider.enabled = value;
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
            transform.Translate(Vector3.up * (speed) * Time.deltaTime);
            yield return null;
        }
    }
}