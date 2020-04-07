using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;


    public float speed = 70f;
    public float explosionRadius = 0f;
    public int damage = 50;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Moves the bullet to the target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        // Rotate the bullet to the target direction.
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                float multiplier = 1 / Mathf.Abs(Vector3.Distance(transform.position, collider.transform.position));
                multiplier = Mathf.Clamp01(multiplier);
                Damage(collider.transform, multiplier);
            }
        }
    }

    void Damage(Transform enemy, float multiplier = 1)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
            e.TakeDamage(damage * multiplier);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
