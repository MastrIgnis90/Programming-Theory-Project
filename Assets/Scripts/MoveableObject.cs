using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    protected Rigidbody thisRb;
    [SerializeField] protected bool moveable = true;

    private float m_maxSpeed = 5f;

    public float MaxSpeed
    {
        get { return m_maxSpeed; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("You cannot set max Speed to a negative number!");
            }
            else
            {
                m_maxSpeed = value;
            }
        }
    }

    protected virtual void Move(Vector3 mvm)
    {
        if (moveable)
        {
            thisRb.AddForce(mvm, ForceMode.Impulse);

            if (thisRb.velocity.y < 0f)
                thisRb.velocity -= Physics.gravity.y * Time.fixedDeltaTime * Vector3.down;

            Vector3 horizontalVelocity = thisRb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > m_maxSpeed * m_maxSpeed)
                thisRb.velocity = horizontalVelocity.normalized * m_maxSpeed + Vector3.up * thisRb.velocity.y;

            LookAt();
        }
    }

    protected virtual void LookAt()
    {
        Vector3 direction = thisRb.velocity;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.1f)
            this.thisRb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            thisRb.angularVelocity = Vector3.zero;
    }
}
