using System;
using UnityEngine;

namespace Assets.Scripts.MatePlayer
{
    public class MatePlayerMovement : MonoBehaviour
    {
        public float Speed = 6f;
        public Vector3 TargetPosition = new Vector3(0, 0, 0);
        public float AxisTolerance = .5f;
        public Quaternion Rotation = new Quaternion(0f, 0f, 0f, 0f);

        private Vector3 _movement;
        private Animator _anim;
        private Rigidbody _playerRigidbody;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var h = TargetPosition.x - transform.position.x;
            var v = TargetPosition.z - transform.position.z;
            Move(h, v);
            Turning();
        }

        private void Move(float h, float v)
        {
            if (Math.Abs(h) < AxisTolerance && Math.Abs(v) < AxisTolerance)
            {
                _anim.SetBool("IsWalking", false);
                return;
            }

            _anim.SetBool("IsWalking", true);
            _movement.Set(h, 0f, v);
            _movement = _movement.normalized * Speed * Time.deltaTime;
            _playerRigidbody.MovePosition(transform.position + _movement);
        }

        private void Turning()
        {
            var newRotation = Quaternion.Lerp(_playerRigidbody.rotation, Rotation, 10f * Time.deltaTime);
            _playerRigidbody.MoveRotation(newRotation);
        }
    }
}
