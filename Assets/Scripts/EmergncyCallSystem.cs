using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmergencyCallout : MonoBehaviour
{
    public List<GameObject> callObjects; // List of possible callouts
    public AudioSource bongAudio;
    public AudioSource frontIncomingCall;
    public AudioSource rearIncomingCall;
    public TMP_Text statusText;
    public GameObject icText;
    private bool availableForCalls = true; 
    private bool callActive = false;
    private GameObject activeCall;
    public GameObject orangePointLightTower;

    void Start()
    {
        UpdateStatusText();
        StartCoroutine(CalloutRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            availableForCalls = true;
            UpdateStatusText();
            StartCoroutine(CalloutRoutine());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            availableForCalls = false;
            UpdateStatusText();
            StopCoroutine(CalloutRoutine());
        }
        else if (callActive && Input.GetKeyDown(KeyCode.Y))
        {
            EndCall();
        }
        else if (availableForCalls && Input.GetKeyDown(KeyCode.U))
        {
            TriggerCall();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            frontIncomingCall.Stop();
            rearIncomingCall.Stop();
            icText.SetActive(false);
        }
    }

    IEnumerator CalloutRoutine()
    {
        while (availableForCalls)
        {
            float waitTime = Random.Range(5f, 100f); // Random wait time before call
            Debug.Log(waitTime);
            yield return new WaitForSeconds(waitTime);

            if (availableForCalls)
            {
                TriggerCall();
            }
        }
    }

    void TriggerCall()
    {
        availableForCalls = false;
        callActive = true;
        UpdateStatusText();
        int randomIndex = Random.Range(0, callObjects.Count);
        activeCall = callObjects[randomIndex];
        activeCall.SetActive(true);
        bongAudio.Play();
        frontIncomingCall.Play();
        rearIncomingCall.Play();
        icText.SetActive(true);
        orangePointLightTower.SetActive(true);
        StartCoroutine(OrangeLightTower());
    }

    void EndCall()
    {
        if (activeCall != null)
        {
            activeCall.SetActive(false);
        }
        callActive = false;
        availableForCalls = true;
        UpdateStatusText();
        StartCoroutine(CalloutRoutine()); // Restart call routine
    }

    void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = availableForCalls ? "Status: Available" : "Status: Unavailable";
        }
    }

    IEnumerator OrangeLightTower()
    {
        yield return new WaitForSeconds(60);
        orangePointLightTower.SetActive(false);
    }
}
