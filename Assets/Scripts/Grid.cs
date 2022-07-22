using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject cross;
    [SerializeField] AudioClip clickSFX;

    void OnMouseDown()
    {
        cross.SetActive(true);
        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
    }
}
