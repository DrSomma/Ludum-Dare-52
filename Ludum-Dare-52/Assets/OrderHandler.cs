using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LudumDare52;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Npc.Order;
using LudumDare52.Storage;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private CustomerOrderContainer orderContainer;
    
    
    void Start()
    {
        interactable.OnLeftClick += OnLeftClick;
    }
    
    private void OnLeftClick()
    {
        Order order = orderContainer.Order;
        foreach (var orderItem in order.OrderList)
        {
            bool hasItem = StorageManager.Instance.TryRemoveFromStorage(orderItem.Key);
            orderContainer.FulfillItemOrder(orderItem.Key);
        }
    }
}
