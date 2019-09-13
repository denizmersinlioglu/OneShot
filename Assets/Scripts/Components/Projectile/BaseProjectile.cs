using System;
using UnityEngine;
using LevelSystem;

public class BaseProjectile : MonoBehaviour {

    [SerializeField]
    private GameObject CreateOnHit;

    [SerializeField]
    private float CreateOnHitLifeTime = 2f;

    public Action OnBallCollided;

    public void Setup(Level level) {
        GetComponent<Rigidbody2D>().gravityScale = level.GravityScale;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        OnBallCollided?.Invoke();
        var hitParticle = Instantiate(CreateOnHit, other.contacts[0].point, Quaternion.identity);
        Destroy(hitParticle.gameObject, CreateOnHitLifeTime);
    }
}