using System.Collections.Generic;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;

namespace LudumDare52.Npc.Order
{
    public class OrderDisplay : MonoBehaviour
    {
        [SerializeField]
        private CustomerOrderContainer _container;

        [Header("Ui")]
        [SerializeField]
        private GameObject uiOrderRowPrefab;

        [SerializeField]
        private GameObject uiOrderItemPrefab;

        [SerializeField]
        private Transform uiContainer;

        private void Awake()
        {
            _container.OnNewOrder += OnNewOrder;
        }

        private void OnNewOrder(Order newOrder)
        {
            Debug.Log("Display new Order");
            foreach (KeyValuePair<Crop, int> orderItem in newOrder.OrderList)
            {
                GameObject row = Instantiate(original: uiOrderRowPrefab, parent: uiContainer, worldPositionStays: false);
                for (int i = 0; i < orderItem.Value; i++)
                {
                    GameObject itemContainer = Instantiate(original: uiOrderItemPrefab, parent: row.transform, worldPositionStays: false);
                    itemContainer.GetComponent<UiOrderItem>().SetNewOrderItem(orderItem.Key.displaySpriteUi);
                }
            }
        }
    }
}