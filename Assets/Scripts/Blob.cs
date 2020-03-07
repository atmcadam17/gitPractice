using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)
    private GameController controller;  // Cached connection to game controller component
    private bool shrinking;

    void Start()
    {
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();

    }

    void Update()
    {
        currentState.Run(); // Run state update.
    }

    // Change state.
    public void ChangeState(BlobState newState)
    {
        if (currentState != null) currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    // Change blobs to shrinking state when clicked.
    void OnMouseDown()
    {
        if (!shrinking)
        {
            ChangeState(new BlobStateShrinking(this));
            shrinking = true;
        }
    }

    public void Blink()
    {
        if (gameObject.GetComponent<Renderer>().enabled == true)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        } else if (gameObject.GetComponent<Renderer>().enabled == false)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            Debug.Log("unblink");
        }
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        controller.Score += 10;
    }

    // add a point (for when it exits blinking)
    public void AddOne()
    {
        controller.Score += 1;
    }
}
