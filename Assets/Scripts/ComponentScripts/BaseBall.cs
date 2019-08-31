using System;
using UnityEngine;

public class BaseBall : MonoBehaviour {

    [SerializeField]
    private ParticleSystem HitParticleSystem;

    public Action OnBallCollided;

    private void OnCollisionEnter2D(Collision2D other) {
        OnBallCollided?.Invoke();
        Instantiate(HitParticleSystem, other.contacts[0].point, Quaternion.identity);
    }

}