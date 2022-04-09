using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerShootProcessor : InputProcessor, IInputProcessor
{
    [Header("Shooting Params")]
    [SerializeField] ObjectPooler bulletPool;
    [SerializeField] float shootingSpeed;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] VisualEffect vfx;
    [SerializeField] float aimingRadius = 2f;
    [SerializeField] AudioSource audioSource;


    private float timeSinceLastShotDone=99;

    public static Action OnFire;


    private void Start()
    {
        timeSinceLastShotDone = shootingSpeed;
    }
    public override void ProcessInput()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (InputManager.Instance.FirePressedContinually())
        {
            TryToShoot();
        }
    }
    private void Update()
    {
        if(timeSinceLastShotDone < shootingSpeed)
        {
            timeSinceLastShotDone += Time.deltaTime;
        }
    }

    private void TryToShoot()
    {
        if(timeSinceLastShotDone >= shootingSpeed)
        {
            Fire();
        }
    }

    private void Fire()
    {
        timeSinceLastShotDone = 0;
        GameObject bullet = ObjectPooler.Instance.GetFromPoolAtPosition(PooledObjectType.Bullet, shootingPoint.position,true);
        if (Vector3.Distance(InputManager.Instance.GetMousePositionWorld(), shootingPoint.position) > aimingRadius)
        {
            bullet.transform.rotation = Quaternion.LookRotation(InputManager.Instance.GetMousePositionWorld() - shootingPoint.position);
        }
        else { bullet.transform.rotation = shootingPoint.rotation;
        }

        bullet.GetComponent<Bullet>().SetVelocity(bullet.transform.forward.normalized);
        bullet.SetActive(true);

        OnFire?.Invoke();
        vfx.Play();
        SoundManager.Instance.PlaySFX("Fire");
    }
}
