using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public const float CamaRayLength = 100f;
        public float AxisTolerance = .5f;
        public float Speed = 6f;

        private Vector3 _movement;
        private Animator _anim;
        private Rigidbody _playerRigidbody;
        private int _floorMask;

        private void Awake()
        {
            _floorMask = LayerMask.GetMask("Floor");
            _anim = GetComponent<Animator>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            Move(h, v);
            Turning();
        }

        private void Move (float h, float v)
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
            var camRay = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (!Physics.Raycast(camRay, out floorHit, CamaRayLength, _floorMask)) return;
            var playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            var newRotation = Quaternion.LookRotation(playerToMouse);
            _playerRigidbody.MoveRotation(newRotation);
        }
    }
}
