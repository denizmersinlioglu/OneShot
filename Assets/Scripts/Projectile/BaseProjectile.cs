using System;
using System.Reflection;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

    [SerializeField]
    private GameObject CreateOnHit;

    [SerializeField]
    private float CreateOnHitLifeTime = 2f;

    public Action OnBallCollided;

    private void OnCollisionEnter2D(Collision2D other) {
        OnBallCollided?.Invoke();
        var hitParticle = Instantiate(CreateOnHit, other.contacts[0].point, Quaternion.identity);
        Destroy(hitParticle.gameObject, CreateOnHitLifeTime);
    }
}