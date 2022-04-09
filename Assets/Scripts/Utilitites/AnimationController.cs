using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    bool canAnimate;
    bool CanAnimate
    {
        get { return canAnimate; }
        set
        {
            animator.SetFloat("Forward", 0);
            animator.SetFloat("Side", 0);
            canAnimate = value;
        }
    }
    private void Awake()
    {
        GameStateMachine.Instance.OnSwitchGameState += ToggleAnimationAbility;
    }

    private void ToggleAnimationAbility(GameState gameState)
    {
        CanAnimate = gameState == GameState.Main;
    }

    private void Update()
    {
        if (canAnimate)
        {
            Vector3 localMovement = transform.InverseTransformDirection(InputManager.Instance.GetPlayerMovement());
            animator.SetFloat("Forward", localMovement.z);
            animator.SetFloat("Side", localMovement.x);
        }
    }
}
