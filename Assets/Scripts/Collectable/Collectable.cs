using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Collectable : MonoBehaviour
{
    protected abstract void Collect(Player player);

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed { get { return _movementSpeed; } }

    Rigidbody _rb;

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

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
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, _movementSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            Collect(player);
            CollectFeedback();
            gameObject.SetActive(false);
        }
    }

    private void CollectFeedback()
    {
        if (_collectParticles != null) Instantiate(_collectParticles, transform.position, Quaternion.identity);
        if (_collectSound != null) AudioHelper.PlayClip2D(_collectSound, 1f);
    }
}
