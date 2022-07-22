using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] TMP_InputField input;
    [SerializeField] float bottomUIScreenHeight;

    Vector3 _originalPos;
    float _gridSize;

    void Start()
    {
        _originalPos = transform.position;
    }

    public void GenerateGridMap()
    {
        //Find and clear existing grid tiles before creating new ones
        Grid[] grids = FindObjectsOfType<Grid>();

        foreach (Grid grid in grids)
        {
            Destroy(grid.gameObject);
        }

        ProcessGridMap();
    }


    void ProcessGridMap()
    {
        //Reset positon to first position
        transform.position = _originalPos;


        //Get grid size from user input
        _gridSize = float.Parse(input.text);


        //Calculate reference top left corner of the screen
        Vector2 topLeftCorner = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        float posX = topLeftCorner.x;
        float posY = topLeftCorner.y;


        //Calculate resize ratio of grid tiles according to screen / grid size
        float newScaleX = (Screen.width / _gridSize) / 128f;
        float newScaleY = ((Screen.height - bottomUIScreenHeight) / _gridSize) / 128f;


        //Initialize new gameObject for reference
        GameObject createdTile = new GameObject();


        //Nested 'for' loops for creating the grid map
        for (int row = 0; row < _gridSize; row++)
        {
            for (int col = 0; col < _gridSize; col++)
            {
                GameObject gridTile = Instantiate(grid, transform);
                createdTile = gridTile;

                gridTile.transform.localScale = new Vector2(gridTile.transform.localScale.x * newScaleX,
                                                            gridTile.transform.localScale.y * newScaleY);

                posY = posY - gridTile.transform.localScale.y;

                gridTile.transform.position = new Vector2(posX, posY);


                //On the last loop, moving to next column and re-adjusting row start position to top of screen
                if (col == _gridSize - 1)
                {
                    posX = posX + gridTile.transform.localScale.x;
                    posY = topLeftCorner.y;
                }
            }
        }


        //Move object to adjust whole grid tiles according to screen
        transform.position = new Vector3(createdTile.transform.localScale.x / 2,
                                         createdTile.transform.localScale.y / 2,
                                         transform.position.z);
    }
}
