using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth <= 0) Kill();
            else if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;

            _healthText.text = "HP: " + _currentHealth;
        }
    }

    [SerializeField] int _treasure = 0;
    public int Treasure
    {
        get => _treasure;
        set
        {
            _treasure = value;
            _treasureText.text = "Treasure: " + _treasure;
        }
    }

    [SerializeField] private bool _invincible = false;
    public bool Invincible
    {
        get => _invincible;
        set => _invincible = value;
    }

    BallMotor _ballMotor;
    Rigidbody _rb;

    [SerializeField] UnityEngine.UI.Text _healthText;
    [SerializeField] UnityEngine.UI.Text _treasureText;

    private void Awake()
    {
        startPosition = transform.position;

        _ballMotor = GetComponent<BallMotor>();
        _rb = GetComponent<Rigidbody>();
        CurrentHealth = _maxHealth;
        Treasure = 0;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        ProcessMovement();  
    }

    private void ProcessMovement()
    {
        //TODO move into Input script
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        _ballMotor.Move(movement);
    }

    public void IncreaseHealth(int amount) { CurrentHealth += amount; }
    public void DecreaseHealth(int amount) { if(!_invincible) CurrentHealth -= amount; }

    public void Kill()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;

        Treasure = 0;

        StartCoroutine("WaitToRespawn");
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(1f);
        Respawn();
    }

    public void Respawn()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        _rb.isKinematic = false;

        CurrentHealth = _maxHealth;

        transform.position = startPosition;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
