using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public static Bounds Instance;

    public float screenHeight = 0;
    public float screenWidth = 0;
    public float aspect = 0;

    public float height = 0;
    public float width = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetBounds();
    }

    public void SetBounds()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        aspect = Camera.main.aspect;

        height = Camera.main.orthographicSize * 2;
        width = height * aspect;
        transform.localScale = new Vector3(width, 0, height);
    }
}
