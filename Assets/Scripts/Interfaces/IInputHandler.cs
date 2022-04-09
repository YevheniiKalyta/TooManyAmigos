using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public interface IInputProcessor
{
    public void ProcessInput();
    public bool ShouldProcessInput();
}
