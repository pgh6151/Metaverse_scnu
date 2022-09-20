using System;
using UnityEngine;

namespace MiniGame3
{
    public class ValleyBall : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField] private float power = 5f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Vector3 direction = (transform.position - collision.collider.ClosestPoint(transform.position)).normalized + Vector3.up;
                _rigidbody.AddForce(direction * power, ForceMode.Impulse);
            }
            
        }
    }
}
