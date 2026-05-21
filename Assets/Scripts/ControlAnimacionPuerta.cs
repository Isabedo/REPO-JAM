using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimacionPuerta : MonoBehaviour
{

    public Material[] doorStates;
    private int currentState = 0;

    private Renderer doorRenderer;

    // Start is called before the first frame update
    void Start()
    {
        doorRenderer = GetComponent<Renderer>();
        UpdateDoorVisual();
    }

    public void AdvanceState()
    {
        if (currentState < doorStates.Length - 1)
        {
            currentState++;
            UpdateDoorVisual();
        }
    }

    void UpdateDoorVisual()
    {
        doorRenderer.material = doorStates[currentState];
    }
}
