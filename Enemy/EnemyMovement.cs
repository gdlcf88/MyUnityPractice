using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private static readonly System.Random Rand = new System.Random();

        private GameObject[] _players;
        private GameObject _player;
        private IPlayerHealth _playerHealth;
        private EnemyHealth _enemyHealth;
        private UnityEngine.AI.NavMeshAgent _nav;

        private void Awake ()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            var randNumber = Rand.Next(_players.Length);
            _player = _players[randNumber];
            _playerHealth = _player.GetComponent<IPlayerHealth> ();
            _enemyHealth = GetComponent<EnemyHealth> ();
            _nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        }


        private void Update ()
        {
            if(_enemyHealth.CurrentHealth > 0 && _playerHealth.CurrentHealth > 0)
            {
                _nav.SetDestination (_player.transform.position);
            }
            else
            {
                _nav.enabled = false;
            }
        }
    }
}
