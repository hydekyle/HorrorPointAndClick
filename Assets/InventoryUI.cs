using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using System.Linq;
using RPGSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Localization.Plugins.XLIFF.V12;

public class InventoryUI : MonoBehaviour
{
    public AudioClip openBagSFX;
    public static bool isOpen = false;
    public GameObject bag;

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (isOpen) CloseInventory();
            else OpenInventory();
        }
    }

    void OpenInventory()
    {
        AudioManager.Instance.PlaySound(openBagSFX);
        for (var x = 0; x < RPGManager.Instance.gameState.inventory.Keys.Count; x++)
        {
            bag.transform.GetChild(x).GetComponent<Image>().sprite = GetItemByIndex(x).sprite;
        }
        bag.SetActive(true);
        isOpen = true;
    }

    ScriptableItem GetItemByIndex(int index)
    {
        return RPGManager.Instance.gameState.inventory.Keys.ToList()[index];
    }

    void CloseInventory()
    {
        bag.SetActive(false);
        isOpen = false;
    }

    public void UseItem(int index)
    {
        CloseInventory();
        var sprite = GetItemByIndex(index).sprite;
        Cursor.SetCursor(sprite.texture, new Vector2(sprite.texture.width / 2, sprite.texture.height / 2), CursorMode.ForceSoftware);
    }
}
