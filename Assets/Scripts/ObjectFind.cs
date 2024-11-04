using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectFind : MonoBehaviour
{
    public TeaCeremony teaCeremonyScript;  // Reference to the TeaCeremony script
    private string _objectName;  // Store the object's name for easy access
    private Animator _animator;
    private Collider2D _collider;
    private void Start()
    {
        // Get the object's display name from the "ObjectName" child
       _animator = GetComponentInParent<Animator>();
        _collider = GetComponent<Collider2D>();

    }

    public void OnMouseDown()
    {
        // Call the FoundObject method in TeaCeremony with this item's name
        //teaCeremonyScript.FoundObject(objectName);
        _animator.SetTrigger("Found");
        _collider.enabled = false;


        Debug.Log("yeah");

    }

}
