using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemComponent : Element
{
    [SerializeField] private GameObject UI;
    [SerializeField] private TMP_InputField NameField;

    [SerializeField] private string nameUnsaved;

    // Start function
    public override void Start()
    {
        if (NameField != null)
        {
            NameField.text = gameObject.name;
            nameUnsaved = gameObject.name;
        }
    }

    // When User click
    public override void OnMouseDown()
    {
        UI.SetActive(true);
    }

    // Disactive panel
    public void DisactivaveUI()
    {
        UI.SetActive(false);
    }

    // Delete the item
    public void Delete() => Destroy(gameObject);

    // Changing name
    public void ChangeName(string name) => nameUnsaved = name;

    // Save
    public virtual void Save()
    {
        gameObject.name = nameUnsaved;
        NameField.text = gameObject.name;
        DisactivaveUI();
    }

    // Abort
    public virtual void Abort()
    {
        nameUnsaved = gameObject.name;
        NameField.text = nameUnsaved;
        DisactivaveUI();
    }
}
