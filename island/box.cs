using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoxManager : MonoBehaviour
{
    private const string BoxTag = "1";
    private const string ItemTag = "2";
    private const int MaxItemsPerBox = 10;

    private Dictionary<GameObject, List<GameObject>> boxToItemsMap = new Dictionary<GameObject, List<GameObject>>();

    void Start()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag(BoxTag);
        GameObject[] items = GameObject.FindGameObjectsWithTag(ItemTag);

        foreach (GameObject box in boxes)
        {
            boxToItemsMap[box] = new List<GameObject>();
        }

        foreach (GameObject item in items)
        {
            foreach (GameObject box in boxes)
            {
                if (boxToItemsMap[box].Count < MaxItemsPerBox)
                {
                    boxToItemsMap[box].Add(item);
                    item.transform.parent = box.transform; // Makes the item a child of the box in the hierarchy
                    break;
                }
            }
        }

        // Add XR Grab Interactable component to items to allow them to be picked up in VR
        foreach (GameObject item in items)
        {
            item.AddComponent<XRGrabInteractable>();
        }
    }
}
