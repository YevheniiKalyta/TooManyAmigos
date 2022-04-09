using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<TagPoolObjectPair> tagPoolObjectPairs = new List<TagPoolObjectPair>();
    private Rigidbody rb;
    [Serializable]
    struct TagPoolObjectPair
    {
        public string tag;
        public PooledObjectType pooledObjectType;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        GetComponent<TrailRenderer>().Clear();
    }
    public void SetVelocity(Vector3 velocity)
    {
        velocity.y = 0;
        rb.velocity = velocity * speed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VisibilityArea"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<IKillable>().TakeDamage(35, collision.GetContact(0));
            SoundManager.Instance.PlaySFX(collision.collider.tag);
        }

        PooledObjectType pooledObjectType = FindTagInPairs(collision.collider.tag);
        if (pooledObjectType != PooledObjectType.None)
        {
            var particles = ObjectPooler.Instance.GetFromPoolAtPosition(pooledObjectType, collision.contacts[0].point);
            particles.transform.forward = collision.contacts[0].normal;
            if (pooledObjectType != PooledObjectType.VegetationChips)
            {
                SoundManager.Instance.PlaySFX(collision.collider.tag);
            }
             
        }
        gameObject.SetActive(false);
        gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    private PooledObjectType FindTagInPairs(string tag)
    {
        foreach (var item in tagPoolObjectPairs)
        {
            if(item.tag == tag)
            {
                return item.pooledObjectType;
            }
        }
        return PooledObjectType.None;
    }
}
