using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float powerupDuration = 5f;

    protected abstract void PowerUpPlayer(Player player);
    protected abstract void PowerDownPlayer(Player player);

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed { get { return _movementSpeed; } }

    Rigidbody _rb;

    [SerializeField] ParticleSystem _powerupParticles;
    [SerializeField] AudioClip _powerupSound;
    [SerializeField] ParticleSystem _powerdownParticles;
    [SerializeField] AudioClip _powerdownSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, 0, _movementSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUpPlayer(player);
            PowerUpFeedback();
        }
    }

    protected void PowerUpFeedback()
    {
        if (_powerupParticles != null) Instantiate(_powerupParticles, transform.position, Quaternion.identity);
        if (_powerupSound != null) AudioHelper.PlayClip2D(_powerupSound, 1f);
    }

    protected void PowerDownFeedback()
    {
        if (_powerdownParticles != null) Instantiate(_powerdownParticles, transform.position, Quaternion.identity);
        if (_powerdownSound != null) AudioHelper.PlayClip2D(_powerdownSound, 1f);
    }
}
