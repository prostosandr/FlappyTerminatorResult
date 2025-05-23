using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action SpacePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpacePressed?.Invoke();
    }
}
