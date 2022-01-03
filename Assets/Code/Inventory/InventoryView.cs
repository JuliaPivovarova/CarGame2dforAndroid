using System.Collections.Generic;
using Code.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Inventory
{
    public class InventoryView: MonoBehaviour, IInventoryView
    {
        [SerializeField] private Image[] placeForItems;
        [SerializeField] private TextMeshProUGUI text;
        
        private float _distance;
        private Vector3 _offset;
        
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

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Moved)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        _offset = hit.transform.position - hit.point;
                        _distance = hit.distance;
                    }

                    text.transform.position = ray.origin + ray.direction * _distance + _offset;
                }
            }

            for (int i = 0; i < placeForItems.Length; i++)
            {
                if (text.transform.position.x <= placeForItems[i].transform.position.x + 20f && text.transform.position.x >= placeForItems[i].transform.position.x - 20f)
                {
                    if (text.transform.position.y <= placeForItems[i].transform.position.y + 20f && text.transform.position.y >= placeForItems[i].transform.position.y - 20f)
                    {
                        text.gameObject.SetActive(true);
                        foreach (var item in items)
                        {
                            if (placeForItems[i].sprite == item.Info.Sprite)
                            {
                                text.text = item.Info.Title;
                            }
                        }
                    }
                    else if(text.gameObject.activeInHierarchy)
                    {
                        text.gameObject.SetActive(false);
                    }
                    
                    if(text.gameObject.activeInHierarchy)
                    {
                        text.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}