using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactable
{
    public int id;
    public Sprite icon;
    public AudioClip pickupClip;
    public AudioClip dropClip;
    public AudioClip fullClip;

    public virtual bool Grab(Inventory inventory) 
    {
        if (inventory.IsFull())
        {
            return false;
        }
        inventory.Add(this);
        gameObject.SetActive(false);
        return true;
    }

    public virtual bool Drop(Inventory inventory, Vector3 dropPosition)
    {
        inventory.Remove(this);
        transform.position = dropPosition;
        gameObject.SetActive(true);
        GameManager.instance.soundFx.Play(dropClip);
        return true;
    }

    public virtual void Use() 
    {
        Debug.Log("Use Item");
    }

    public void Interact()
    {
        bool success = Grab(GameManager.instance.player.inventory);
        if (success)
        {
            GameManager.instance.soundFx.Play(pickupClip);
        }
        else
        {
            GameManager.instance.soundFx.Play(fullClip);
        }
    }
}
