using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    [SerializeField] private Color _invincibleColor;
    private Color _startColor;

    Player _player;

    protected override void PowerUpPlayer(Player player)
    {
        _player = player;

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        player.Invincible = true;

        _startColor = player.GetComponent<Renderer>().material.color;
        player.GetComponent<Renderer>().material.color = _invincibleColor;

        StartCoroutine("AwaitPowerUpDuration");
    }

    IEnumerator AwaitPowerUpDuration()
    {
        yield return new WaitForSeconds(powerupDuration);
        PowerDownPlayer(_player);
    }

    protected override void PowerDownPlayer(Player player)
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

        player.Invincible = false;

        player.GetComponent<Renderer>().material.color = _startColor;

        PowerDownFeedback();
    }
}
