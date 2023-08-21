using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float lifeTime;

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
    }
}
