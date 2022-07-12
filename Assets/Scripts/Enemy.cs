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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }

    protected override void LookAt()
    {
        var look = player.transform.position - transform.position;
        look.y = 0;
        thisRb.rotation = Quaternion.LookRotation(look, Vector3.up);
    }
}
