using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] int damage = 20;

    private void Start()
    {
        StartCoroutine(DisActive());
    }

    private void OnEnable()
    {
        StartCoroutine(DisActive());
    }

    IEnumerator DisActive()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Health health = collision.gameObject.GetComponent<Health>();
        if(health!= null )
        {
            health.TakeDamage(damage);
        }
    }
}
