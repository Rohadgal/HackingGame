using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    private float throwForce = 500f; 
    private float pickUpRange = 1f; 
    private GameObject heldObj; 
    private Rigidbody heldObjRb; 
    private bool canDrop = true; 
    private int LayerNumber; 
    private bool messageVisible = false;
    public Text messageText;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        SelectObject();
        CheckObjectInRange();
    }

    private void SelectObject()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldObj == null) 
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.CompareTag("Cube"))
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if(canDrop == true)
                {
                    StopClipping(); 
                    DropObject();
                }
            }
        }

        if (heldObj != null) 
        {
            MoveObject(); 
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) 
            {
                StopClipping();
                ThrowObject();
            }

        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) 
        {
            heldObj = pickUpObj; 
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); 
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; 
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; 
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; 
        heldObj = null;
    }
    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }
    
    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    void StopClipping() 
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); 
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

    void CheckObjectInRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
        {
            if (hit.transform.CompareTag("Cube"))
            {
                ShowMessage();
            }
        }
        else
        {
            HideMessage();
        }
    }

    void ShowMessage()
    {
        if(messageText != null && !messageVisible)
        {
            messageText.gameObject.SetActive(true);
            messageVisible = true;
        }
    }

    void HideMessage()
    {
        if (messageText != null && messageVisible)
        {
            messageText.gameObject.SetActive(false);
            messageVisible = false;
        }
    }
    
}
