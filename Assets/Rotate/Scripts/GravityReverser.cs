﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReverser : MonoBehaviour
{
    public float scaleDownSpeed = 4f;

    private Player player;
    private bool ignorePlayer = false;

    private Quaternion initialRotation;
    private Vector2 initialScale;

	void Start()
	{
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        if (!ignorePlayer && other.CompareTag(Tags.Player))
        {
            player = other.GetComponent<Player>();

            player.onPlayerDeath += Show;
            player.rigidbody2d.gravityScale *= -1;

            StartCoroutine(ScaleDown());

            ignorePlayer = true;
        }
    }

    private void Show()
    {
        transform.localScale = initialScale;
        transform.localRotation = initialRotation;
        gameObject.SetActive(true);

        player.onPlayerDeath -= Show;

        ignorePlayer = false;
    }

    private IEnumerator ScaleDown()
    {
        while (transform.localScale.y > 0f)
        {
            transform.Rotate(Vector3.forward * 180f * Time.deltaTime);
            transform.localScale -= Vector3.one * scaleDownSpeed * Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
