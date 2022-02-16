using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorExample : MonoBehaviour
{

    public InputActionReference ColorReference = null;

    private MeshRenderer mesh = null;

    // Start is called before the first frame update
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float value = ColorReference.action.ReadValue<float>();
        updatecolor(value);
    }

    private void updatecolor(float value)
    {
        mesh.material.color = new Color(value, value, value);
    }


}
