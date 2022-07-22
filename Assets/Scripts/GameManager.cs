using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip winSFX;
    [SerializeField] ParticleSystem winVFX;
    [SerializeField] TextMeshProUGUI matchCount;
    [SerializeField] float winScreenDelay;

    int _score;
    int _matchCount;

    void Update()
    {
        //Checking cross score to determine if game is finished
        if (_score >= 4)
        {
            _score = 0;
            StartCoroutine(nameof(ProcessWin));
        }
    }

    public void IncrementScore()
    {
        _score++;
    }

    IEnumerator ProcessWin()
    {
        //Freeze mouse input while processing win scenario
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //Calculate match score and then display
        _matchCount++;
        matchCount.text = $"Match \n Count: {_matchCount}";


        //Play sound and visual effects
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        winVFX.Play();


        //Wait for desired time
        yield return new WaitForSeconds(winScreenDelay);


        //Unfreeze mouse input
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        //Find and clear all existing grid tiles for new scene
        Grid[] grids = FindObjectsOfType<Grid>();

        foreach (Grid grid in grids)
        {
            Destroy(grid.gameObject);
        }
    }

}
