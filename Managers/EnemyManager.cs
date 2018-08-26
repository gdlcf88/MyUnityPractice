using System.Linq;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject Enemy;
        public float SpawnTime = 3f;
        public Transform[] SpawnPoints;

        private GameObject[] _playersHealth;

        private void Start ()
        {
            _playersHealth = GameObject.FindGameObjectsWithTag("Player");
            InvokeRepeating ("Spawn", SpawnTime, SpawnTime);
        }


        private void Spawn ()
        {
            return;
            if (_playersHealth.Any(player => player.GetComponent<IPlayerHealth>().CurrentHealth <= 0f))
            {
                return;
            }

            var spawnPointIndex = Random.Range (0, SpawnPoints.Length);

            Instantiate (Enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
        }
    }
}
