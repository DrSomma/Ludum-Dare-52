using System.Collections.Generic;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;
using UnityEngine.UI;

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
        
        void Awake()
        {
            _container.OnNewOrder += OnNewOrder;
        }

        private void OnNewOrder(Order newOrder)
        {
            Debug.Log("Display new Order");
            foreach (KeyValuePair<Crop, int> orderItem in newOrder.OrderList)
            {
                var row = Instantiate(uiOrderRowPrefab, uiContainer, false);
                for (int i = 0; i < orderItem.Value; i++)
                {
                    var itemContainer = Instantiate(uiOrderItemPrefab, row.transform, false);
                    itemContainer.GetComponent<UiOrderItem>().SetNewOrderItem(orderItem.Key.displaySpriteUi);
                }
            }           
        }
    }
}
