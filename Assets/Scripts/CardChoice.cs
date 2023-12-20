using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChoice : MonoBehaviour
{
    public List<GameObject> selectableObjects;

    public GameManager gameManager;

    private GameObject selectedObject1;
    private GameObject selectedObject2;
    private GameObject selectedObject3;

    public Transform cardPos1;
    public Transform cardPos2;
    public Transform cardPos3;

    public GameObject cardUI;
    public AudioSource cardAudio, menuPopAudio;

    public void Update()
    {
        
    }

    public void PickRandomObjects()
    {
        if (selectableObjects.Count == 5)
        {
            selectedObject1 = null;
            selectedObject2 = null;
            selectedObject3 = null;

            // Create a list to store indices of selected objects
            List<int> selectedIndices = new List<int>();

            // Randomly pick three objects without repetition
            for (int i = 0; i < 3; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, selectableObjects.Count);
                } while (selectedIndices.Contains(randomIndex));

                selectedIndices.Add(randomIndex);

                // Assign the selected object to variables based on iteration
                if (i == 0)
                    selectedObject1 = selectableObjects[randomIndex];
                else if (i == 1)
                    selectedObject2 = selectableObjects[randomIndex];
                else if (i == 2)
                    selectedObject3 = selectableObjects[randomIndex];
            }

            Debug.Log("Selected Object 1: " + selectedObject1.name);
            Debug.Log("Selected Object 2: " + selectedObject2.name);
            Debug.Log("Selected Object 3: " + selectedObject3.name);

            cardUI.gameObject.SetActive(true);
            menuPopAudio.Play();
            Time.timeScale = 0f;

            Instantiate(selectedObject1, cardPos1.position, Quaternion.identity, cardUI.transform);
            Instantiate(selectedObject2, cardPos2.position, Quaternion.identity, cardUI.transform);
            Instantiate(selectedObject3, cardPos3.position, Quaternion.identity, cardUI.transform);
        }
        else
        {
            Debug.LogError("Not enough selectable objects in the list. Please configure the list in the editor.");
        }
    }

    public void EndCardChoice()
    {
        Time.timeScale = 1.0f;
        cardUI.gameObject.SetActive(false);
    }

    public void PlayCardChoiceSound()
    {
        cardAudio.Play();
    }
}
