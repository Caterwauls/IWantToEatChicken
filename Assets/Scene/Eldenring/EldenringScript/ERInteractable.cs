using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERInteractable : MonoBehaviour
{
    public virtual bool CanInteract(ERPlayer player)
    {
        return false;
    }

    public virtual string GetInteractText()
    {
        return null;
    }
    
    public virtual void Interact(ERPlayer player)
    {
        
    }
}
