using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehavior : MonoBehaviour
{
    // Store the welcome page prefab
    [Header("Welcome page")]
    public GameObject welcomePagePrefab;

    // Store the first page prefab
    [Header("First page")]
    public GameObject firstPage;

    // Store based on the location of which object the page will show up
    [Header("First page holder")]
    public GameObject firstPageHolder;

    // Store the one-hand grabable object prefab
    [Header("One hand holding object")]
    public GameObject oneHandObjectPrefab;

    // Store the second page prefab
    [Header("Second page")]
    public GameObject secondPage;

    // Store based on the location of which object the page will show up
    [Header("Second page holder")]
    public GameObject secondPageHolder;

    // Store the two-hand grabable object prefab
    [Header("Two hand holding object")]
    public GameObject twoHandObjectPrefab;

    // Store the third page prefab
    [Header("Third page")]
    public GameObject thirdPage;

    // Store the ending page prefab
    [Header("End page")]
    public GameObject endPage;

    private GameObject page;
    private int currentStep = 0;
    private ObjectAnchor oneHandGrabObject;
    private TwoHandAnchor twoHandObject;
    private MainPlayerController mainPlayerController;

    // Initialization
    void Start()
    {
        page = Instantiate(welcomePagePrefab, transform.position + transform.forward + new Vector3(0, 0.5f, 0), Quaternion.identity * Quaternion.Euler(0, calculateAngleToFaceTheObject(), 0), transform);
        twoHandObject = twoHandObjectPrefab.GetComponent<TwoHandAnchor>();
        oneHandGrabObject = oneHandObjectPrefab.GetComponent<ObjectAnchor>();
        mainPlayerController = GetComponent<MainPlayerController>();
    }

    void Update()
    {
        switch (currentStep)
        {
            case 0:
                if (isButtonPressed())
                {
                    // Destroy the welcome page
                    Destroy(page);

                    // Load the first page
                    page = Instantiate(firstPage, firstPageHolder.transform.position + new Vector3(0, 1.8f, 0), Quaternion.identity * Quaternion.Euler(0, 90, 0), null);

                    currentStep += 1;
                }
                break;
            case 1:
                if (hasGrabbedAndReleased())
                {
                    // Change the text on the first page
                    changePageTextAndClearNext(page, "Completed! \n Turn to the back to continue with the next step");

                    // Load the second page
                    page = Instantiate(secondPage, secondPageHolder.transform.position + new Vector3(0, -0.5f, -0.5f), Quaternion.identity * Quaternion.Euler(0, -90, 0), null);

                    currentStep += 1;
                }
                break;
            case 2:
                if (twoHandObject.getHasHoldAndReleased())
                {
                    // Change the text on the second page
                    changePageTextAndClearNext(page, "Completed!");

                    // Load the third page
                    page = Instantiate(thirdPage, transform.position + transform.forward * 1.2f + new Vector3(0, 0.5f, 0), Quaternion.identity * Quaternion.Euler(0, -90, 0), transform);

                    currentStep += 1;
                }
                break;
            case 3:
                if (mainPlayerController.getHasSprint())
                {
                    // Add a delay before continue with the third step
                    Invoke("step3", 2);

                    currentStep += 1;
                }
                break;
            case 4:
                if (isButtonPressed())
                {
                    // Destroy the ending page
                    Destroy(page);

                    currentStep += 1;
                }
                break;
        }
    }

    // Check if user has pressed any button
    private bool isButtonPressed()
    {
        return OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two)
            || OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four);
    }

    // Check if user has grabbed something and released it
    private bool hasGrabbedAndReleased()
    {
        return oneHandGrabObject.getHasHoldAndReleased();
    }

    // Change the text of the first page
    private void changePageTextAndClearNext(GameObject page, string text) {
        TextMeshProUGUI content = page.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        content.text = text;
        TextMeshProUGUI next = page.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        next.text = "";
    }

    // Calculate the angle to rotate the page so that the page can face the user
    private float calculateAngleToFaceTheObject()
    {
        return Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up) -90;
    }

    private IEnumerator WaitForFunction(int time)
    {
        yield return new WaitForSeconds(time);
    }

    // Present the logic for the third step in the tutorial
    private void step3()
    {
        // Add delay to let user sprint
        StartCoroutine(WaitForFunction(2));

        // Destroy the third page
        Destroy(page);

        // Load the ending page
        page = Instantiate(endPage, transform.position + transform.forward * 1.2f + new Vector3(0, 0.5f, 0), Quaternion.identity * Quaternion.Euler(0, calculateAngleToFaceTheObject(), 0), transform);

    }
}

