using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public float HorizontalInput { get; private set; }
    public bool IsBraking { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        IsBraking = Input.GetKey(KeyCode.Space);
    }
}
