using UnityEngine;

public class Enemy : MoveableObject
{
    private GameObject player;

    private void Awake()
    {
        thisRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 steering = player.transform.position - transform.position - thisRb.velocity;
        steering.y = 0;

        Move(steering.normalized);
    }
}
