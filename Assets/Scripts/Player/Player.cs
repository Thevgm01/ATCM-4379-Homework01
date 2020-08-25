using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;

    BallMotor _ballMotor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
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

    public void IncreaseHealth(int amount) { ChangeHealth(amount); }
    public void DecreaseHealth(int amount) { ChangeHealth(-amount); }
    public void ChangeHealth(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth <= 0) Kill();
        else if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
