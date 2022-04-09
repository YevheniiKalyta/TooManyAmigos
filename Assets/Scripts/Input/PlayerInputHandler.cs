using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private List<InputProcessor> inputProcessors;

    private void Update()
    {
        foreach (var inputProcessor in inputProcessors)
        {
            if(inputProcessor.ShouldProcessInput())
                inputProcessor.ProcessInput();
        }
    }
}
