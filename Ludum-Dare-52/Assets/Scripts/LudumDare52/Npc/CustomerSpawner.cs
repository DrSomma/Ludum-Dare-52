using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities;
using Amazeit.Utilities.Random;
using LudumDare52.Npc.Order;
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
        private int MaxCustomers = 4;

        [SerializeField]
        private float SpawnDelayMinInSeconds = 10f;

        [SerializeField]
        private float SpawnDelayMaxInSeconds = 20f;

        private readonly List<GameObject> _npcs = new();

        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private void Spawn()
        {
            GameObject npc = Instantiate(npcPrefab.Random());
            npc.transform.position = transform.position;
            npc.GetComponent<NpcOrderContainer>().Order = orderManager.GetNewOrder();
            npc.GetComponent<OnObjectDestroyed>().OnDestroyCallback += OnNpcDestroyed;
            _npcs.Add(npc);
        }

        private void OnNpcDestroyed(GameObject npc)
        {
            _npcs.Remove(npc);
        }

        private IEnumerator SpawnLoop()
        {
            //TODO: Pause oder Tag vorbei
            while (true)
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
        }
    }
}