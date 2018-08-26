using System;
using System.Linq;
using Assets.Scripts.Interface;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        public float TimeBetweenAttacks = 0.5f;
        public int AttackDamage = 10;

        private Animator _anim;
        private GameObject _player;
        private GameObject[] _players;
        private IPlayerHealth _playerHealth;
        private EnemyHealth _enemyHealth;
        private bool _playerInRange;
        private float _timer;


        private void Awake ()
        {
            _players = GameObject.FindGameObjectsWithTag ("Player");
            _enemyHealth = GetComponent<EnemyHealth>();
            _anim = GetComponent <Animator> ();
        }


        private void OnTriggerEnter ([NotNull] Collider other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            var mPlayer = IsPlayer(other);
            if (!mPlayer) return;
            _playerInRange = true;
            _player = mPlayer;
            _playerHealth = _player.GetComponent<IPlayerHealth>();
        }


        private void OnTriggerExit (Component other)
        {
            var mPlayer = IsPlayer(other);
            if (!mPlayer) return;
            _playerInRange = false;
            _player = null;
            _playerHealth = null;
        }

        private GameObject IsPlayer (Component other)
        {
            return _players.FirstOrDefault(mPlayer => other.gameObject == mPlayer);
        }

        private bool IsEverybodyDead()
        {
            return _players.All(mPlayer => mPlayer.GetComponent<IPlayerHealth>().CurrentHealth <= 0);
        }


        private void Update ()
        {
            _timer += Time.deltaTime;


            if (_players.Any(player => player.GetComponent<IPlayerHealth>().CurrentHealth <= 0f))
            {
                return;
            }

            if (_timer >= TimeBetweenAttacks && _playerInRange && _enemyHealth.CurrentHealth > 0)
            {
                Attack ();
            }

            if (IsEverybodyDead())
                _anim.SetTrigger("PlayerDead");

        }


        private void Attack ()
        {
            _timer = 0f;

            if(_playerHealth.CurrentHealth > 0)
            {
                _playerHealth.TakeDamage (AttackDamage);
            }
        }
    }
}
