using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public Action<float, float> OnMovement;
    public Action OnShoot;

    [SerializeField] private float horizontal = 0;
    [SerializeField] private float vertical = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            OnMovement?.Invoke(horizontal, vertical);

            Debug.Log("movement");
        }

        if(Input.GetButtonDown("Jump"))
        {
            OnShoot?.Invoke();
        }
    }
}
