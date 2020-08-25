using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }

    protected virtual void PlayerImpact(Player player)
    {
        player.DecreaseHealth(_damageAmount);
    }

    public void ImpactFeedback()
    {
        if (_impactParticles != null) Instantiate(_impactParticles, transform.position, Quaternion.identity);
        if (_impactSound != null) AudioHelper.PlayClip2D(_impactSound, 1f);
    }
}
