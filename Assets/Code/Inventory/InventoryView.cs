using System.Collections.Generic;
using Code.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Inventory
{
    public class InventoryView: MonoBehaviour, IInventoryView
    {
        [SerializeField] private Image[] placeForItems;

        public void Display(IReadOnlyList<IItem> items)
        {
            foreach (var item in items)
            {
                bool isItemPlaced = false;
                for (int i = 0; i < placeForItems.Length; i++)
                {
                    if (placeForItems[i].sprite == null && !isItemPlaced)
                    {
                        placeForItems[i].sprite = item.Info.Sprite;
                        isItemPlaced = true;
                    }
                }
                Debug.Log($"Id item: {item.Id} title: {item.Info.Title}");
            }
        }
    }
}