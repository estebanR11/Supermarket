using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera playerCamera;
    private float interactDistance = 1.0f;

    [SerializeField] GameObject panel;
    [SerializeField]AdvancedWalkerController advancedWalkerController;
    void Start()
    {
        GetComponent<AdvancedWalkerController>();   
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Comprueba si el jugador presiona la tecla E.
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance))
            {
           
                if (hit.collider.CompareTag("Store"))
                {
              
                    InterestPoints interestPoint = hit.collider.GetComponent<InterestPoints>();

                    if (interestPoint != null)
                    {
                        interestPoint.PlayerInteract();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel.gameObject.activeInHierarchy)
            {
                advancedWalkerController.enabled = false; 
                panel.SetActive(true);
            }
            else
            {
                advancedWalkerController.enabled = true;
                panel.SetActive(false); 
            }
        }
    }
}
