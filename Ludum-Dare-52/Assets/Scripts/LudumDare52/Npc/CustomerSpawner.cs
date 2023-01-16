using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities;
using Amazeit.Utilities.Random;
using LudumDare52.Npc.Order;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Npc
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] npcPrefab;

        [SerializeField]
        private OrderManager orderManager;

        [SerializeField]
        private int maxCustomers = 3;

        [SerializeField]
        private float spawnDelayMinInSeconds = 5f;

        [SerializeField]
        private float spawnDelayMaxInSeconds = 10f;

        private readonly List<GameObject> _npcs = new();
        private bool _canSpawn;

        private float _nextSpawnCooldownInSeconds;

        private void Awake()
        {
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
            GameManager.Instance.OnStartDay += OnStartDay;
        }

        private void Update()
        {
            if (!_canSpawn)
            {
                return;
            }

            if (_nextSpawnCooldownInSeconds > 0)
            {
                _nextSpawnCooldownInSeconds -= Time.deltaTime;
                return;
            }

            if (_npcs.Count() >= maxCustomers)
            {
                return;
            }

            Spawn();
            _nextSpawnCooldownInSeconds = Random.Range(minInclusive: spawnDelayMinInSeconds, maxInclusive: spawnDelayMaxInSeconds);
        }

        private void OnStartDay(int obj)
        {
            StartLoop();
            _nextSpawnCooldownInSeconds = 0;
        }

        private void OnStateUpdate(GameState state)
        {
            if (state == GameState.Running)
            {
                StartLoop();
            }

            if (state == GameState.Pause)
            {
                StopLoop();
            }

            if (state is GameState.DayEnd or GameState.GameOver)
            {
                StopLoop();
            }
        }

        private void StartLoop()
        {
            _canSpawn = true;
        }

        private void StopLoop()
        {
            _canSpawn = false;
        }

        private void Spawn()
        {
            GameObject npc = Instantiate(npcPrefab.Random());
            npc.transform.position = transform.position;
            npc.GetComponent<CustomerOrderContainer>().SetOrder(OrderManager.GetNewOrder());
            npc.GetComponent<OnObjectDestroyed>().OnDestroyCallback += OnNpcDestroyed;
            _npcs.Add(npc);
        }

        private void OnNpcDestroyed(GameObject npc)
        {
            _npcs.Remove(npc);
        }
    }
}