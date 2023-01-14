using System.Collections;
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
        private int MaxCustomers = 3;

        [SerializeField]
        private float SpawnDelayMinInSeconds = 10f;

        [SerializeField]
        private float SpawnDelayMaxInSeconds = 20f;

        private readonly List<GameObject> _npcs = new();
        private bool _canSpawn;
        private Coroutine _coroutine;

        private void Awake()
        {
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
        }

        private void OnStateUpdate(GameState state)
        {
            if (state == GameState.Running)
            {
                StartLoop();
            }

            if (state == GameState.Pause)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
            }

            if (state is GameState.DayEnd or GameState.GameOver)
            {
                _canSpawn = false;
            }
        }

        private void StartLoop()
        {
            if (_coroutine != null)
            {
                return;
            }

            _canSpawn = true;
            _coroutine = StartCoroutine(SpawnLoop());
        }

        private void Spawn()
        {
            if (!_canSpawn)
            {
                return;
            }

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

        private IEnumerator SpawnLoop()
        {
            Debug.Log("Spawnloop start");
            while (_canSpawn)
            {
                if (_npcs.Count() >= MaxCustomers)
                {
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    Spawn();
                    yield return new WaitForSeconds(Random.Range(minInclusive: SpawnDelayMinInSeconds, maxInclusive: SpawnDelayMaxInSeconds));
                }
            }

            Debug.Log("Spawnloop done");
            _coroutine = null;
        }
    }
}