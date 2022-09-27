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
                Vector3 direction = Camera.main.transform.forward + Vector3.up * 3f;
                _rigidbody.AddForce(direction * power, ForceMode.Impulse);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                PlayGround playGround = other.GetComponent<PlayGround>();
                Debug.Log(playGround.GetSide());
            }
        }
    }
}
