using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour, Interactable
{
    public GameObject formula;
    public Inventory ingredient;
    public Inventory endProduct;

    public GameObject dialog;
    public ItemCookbook itemCookbook;
    public Vector3 dropPosition = Vector3.right * 2 + Vector3.up;

    public ItemList itemList;
    private Character character;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = dialog.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        //TODO: Need to fixed ItemUI not updated error
        Load();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item _item = collision.GetComponent<Item>();
        if(_item != null)
        {
            if(character != null)
            {
                _item.Grab(ingredient);
                GetItemDialog();
            }
        }
        Character _character = collision.GetComponent<Character>();
        if(_character != null)
        {
            character = _character;
            CharacterEnterDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character _character = collision.GetComponent<Character>();
        if (_character != null)
        {
            character = null;
            HideDialog();
        }
    }

    public void Interact()
    {
        if(character == null)
        {
            Debug.Log("Character is null.");
            return;
        }
        if (endProduct.IsFull())
        {
            ShowDialog("Here you go, \n Have a nice day!");
            DropEndProduct();
        }

        else if (itemCookbook.GetEndProduct(ingredient.items) != null)
        {
            ShowDialog("I merged the items \n for you! \n Press X to get item...");
            MergeIngredient();
        }
        else if (itemCookbook.GetEndProduct(ingredient.items) == null)
        {
            ShowDialog("Sorry, \n I can only merge hearts that has the same color \n");
            DropIngredient();
        }
        else if(!ingredient.IsEmpty())
        {
            ShowDialog("Here's your item...");
            DropIngredient();
        }
        else
        {
            ShowDialog("Hello!");
        }
    }
    private void DropEndProduct()
    {
        endProduct.GetItem(0).Drop(endProduct, transform.position + dropPosition);
    }
    private void DropIngredient()
    {
        for (int i = 0; i < ingredient.space; i++)
        {
            Item item = ingredient.GetItem(i);
            if (item != null)
            {
                item.Drop(ingredient, transform.position + dropPosition);
            }
        }
    }
    private void MergeIngredient()
    {
        Item item = itemCookbook.GetEndProduct(ingredient.items);
        if(item != null)
        {
            GameObject finalProd = Instantiate(item.gameObject, transform.position, Quaternion.identity);
            finalProd.GetComponent<Item>().Grab(endProduct);
            ingredient.Clear();
        }
    }


    public void HideDialog()
    {
        textMesh.text = "";
        formula.SetActive(false);
        dialog.SetActive(false);
    }

    public void ShowDialog(string sentence, bool showFormula = true)
    {
        dialog.SetActive(true);
        textMesh.text = sentence;
        formula.SetActive(showFormula);
    }

    public void GetItemDialog()
    {
        if (ingredient.IsFull())
        {
            ShowDialog("Press X to merge items");
        }
        else
        {
            ShowDialog("That's a nice item.");
        }
    }

    public void CharacterEnterDialog()
    {
        if (character.inventory.IsEmpty())
        {
            ShowDialog("You can Find Items \n by roaming around  \n the map.");
        }
        else
        {
            ShowDialog("Drag and Drop \n Items in front of me.");
        }

        if (ingredient.IsFull())
        {
            ShowDialog("Press X to merge items...");
        }

        if (endProduct.IsFull())
        {
            ShowDialog("Press X to get Item...");
        }
    }

    public void Save()
    {
        SaveSystem.SaveNPC(this);
    }

    public void Load()
    {
        NPCData data = SaveSystem.LoadNPC();
        if(data != null)
        {
            ShowDialog(""); //Prevents UI Error
            for (int i = 0; i < data.ingredientKeys.Length; i++)
            {
                int key = data.ingredientKeys[i];
                Item prefab = itemList.items[key];
                if (prefab != null)
                {
                    Item item = Instantiate(prefab);
                    item.Grab(ingredient);
                }
            }

            for (int i = 0; i < data.endProductKeys.Length; i++)
            {
                int key = data.endProductKeys[i];
                Item prefab = itemList.items[key];
                if (prefab != null)
                {
                    Item item = Instantiate(prefab);
                    item.Grab(endProduct);
                }
            }
            HideDialog();
        }
    }
}
