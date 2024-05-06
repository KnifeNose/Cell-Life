using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject cell;

    [Header("Settings")]
    [SerializeField] private bool randomize;
    [SerializeField] private bool resetPosition;

    [Range(-1f, 1f)]
    [SerializeField] private float rule1;
    [Range(-1f, 1f)]
    [SerializeField] private float rule2;
    [Range(-1f, 1f)]
    [SerializeField] private float rule3;
    [Range(-1f, 1f)]
    [SerializeField] private float rule4;
    [Range(-1f, 1f)]
    [SerializeField] private float rule5;
    [Range(-1f, 1f)]
    [SerializeField] private float rule6;
    [Range(-1f, 1f)]
    [SerializeField] private float rule7;
    [Range(-1f, 1f)]
    [SerializeField] private float rule8;
    [Range(-1f, 1f)]
    [SerializeField] private float rule9;
    [Range(-1f, 1f)]
    [SerializeField] private float rule10;
    [Range(-1f, 1f)]
    [SerializeField] private float rule11;
    [Range(-1f, 1f)]
    [SerializeField] private float rule12;
    [Range(-1f, 1f)]
    [SerializeField] private float rule13;
    [Range(-1f, 1f)]
    [SerializeField] private float rule14;
    [Range(-1f, 1f)]
    [SerializeField] private float rule15;
    [Range(-1f, 1f)]
    [SerializeField] private float rule16;

    private Cell[] redCell;
    private GameObject redCellParent;

    private Cell[] yellowCell;
    private GameObject yellowCellParent;

    private Cell[] greenCell;
    private GameObject greenCellParent;

    private Cell[] blueCell;
    private GameObject blueCellParent;

    private Vector3 tempPosition;
    private Vector3 tempVelocity;

    private int numberOfCells = 300;


    // Start is called before the first frame update
    void Start()
    {
        randomize = false;

        tempPosition = Vector3.zero;
        tempVelocity = Vector3.zero;

        RandomGravity();
        SetupParents();
        SetupArrays(numberOfCells);
        SpawnCells(numberOfCells, Color.red, redCell, redCellParent);
        SpawnCells(numberOfCells, Color.yellow, yellowCell, yellowCellParent);
        SpawnCells(numberOfCells, Color.green, greenCell, greenCellParent);
        SpawnCells(numberOfCells, Color.blue, blueCell, blueCellParent);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomize == true)
        {
            RandomGravity();
            randomize = false;
        }

        if (resetPosition == true) 
        {
            ResetLocation(blueCell);
            ResetLocation(greenCell);
            ResetLocation(redCell);
            ResetLocation(yellowCell);
            resetPosition = false;
        }

        // Green rules
        Rule(greenCell, greenCell, rule1);
        Rule(greenCell, redCell, rule2);
        Rule(greenCell, yellowCell, rule3);
        Rule(greenCell, blueCell, rule4);
        // Red rules
        Rule(redCell, redCell, rule5);
        Rule(redCell, greenCell, rule6);
        Rule(redCell, yellowCell, rule7);
        Rule(redCell, blueCell, rule8);
        // yellow rules
        Rule(yellowCell, yellowCell, rule9);
        Rule(yellowCell, greenCell, rule10);
        Rule(yellowCell, redCell, rule11);
        Rule(yellowCell, blueCell, rule12);
        // Blue rules
        Rule(blueCell, greenCell, rule13);
        Rule(blueCell, redCell, rule14);
        Rule(blueCell, yellowCell, rule15);
        Rule(blueCell, blueCell, rule16);
    }

    private void SpawnCells(int amount, Color color, Cell[] array, GameObject parent)
    {
        for (int i = 0; i < amount; i++) 
        {
            tempPosition.x = Random.Range(-50f, 50f);
            tempPosition.y = Random.Range(-50f, 50f);

            array[i] = new Cell(cell, color, tempPosition, tempVelocity, parent);
        }
    }

    private void SetupArrays(int amount)
    {
        redCell = new Cell[amount];
        yellowCell = new Cell[amount];
        greenCell = new Cell[amount];
        blueCell = new Cell[amount];
    }

    private void SetupParents()
    {
        redCellParent = new GameObject("Red Cell Parent");
        yellowCellParent = new GameObject("Yellow Cell Parent");
        greenCellParent = new GameObject("Green Cell Parent");
        blueCellParent = new GameObject("Blue Cell Parent");
    }

    private void Rule(Cell[] particle1, Cell[] particle2, float g)
    {

        for (int i = 0; i < particle1.Length; i++)
        {
            float forceX = 0f;
            float forceY = 0f;

            Cell a = particle1[i];
            
            for (int j = 0; j < particle2.Length; j++)
            {
                Cell b = particle2[j];

                if (a != b)
                {
                    float dX = a.position.x - b.position.x;
                    float dY = a.position.y - b.position.y;

                    float d = Mathf.Sqrt(dX * dX + dY * dY);

                    if (d > 0f && d < 20f)
                    {
                        float F = g * 1 / d;

                        forceX += (F * dX);
                        forceY += (F * dY);
                    }
                }
            }

            a.velocity.x = (a.velocity.x + forceX) * 0.5f;
            a.velocity.y = (a.velocity.y + forceY) * 0.5f;

            tempPosition = a.position + a.velocity * Time.deltaTime;

            a.SetPosition(tempPosition);

            const float boundary = 50f;
            if (a.position.x <= -boundary || a.position.x >= boundary)
            {
                // Reverse the velocity component
                a.velocity.x *= -1f;

                // Clamp the position to ensure it stays within the boundary
                a.position.x = Mathf.Clamp(a.position.x, -boundary, boundary);
            }
            if (a.position.y <= -boundary || a.position.y >= boundary)
            {
                // Reverse the velocity component
                a.velocity.y *= -1f;

                // Clamp the position to ensure it stays within the boundary
                a.position.y = Mathf.Clamp(a.position.y, -boundary, boundary);
            }
        }
    }

    private void RandomGravity()
    {
        rule1 = Random.Range(-1f, 1f);
        rule2 = Random.Range(-1f, 1f);
        rule3 = Random.Range(-1f, 1f);
        rule4 = Random.Range(-1f, 1f);
        rule5 = Random.Range(-1f, 1f);
        rule6 = Random.Range(-1f, 1f);
        rule7 = Random.Range(-1f, 1f);
        rule8 = Random.Range(-1f, 1f);
        rule9 = Random.Range(-1f, 1f);
        rule10 = Random.Range(-1f, 1f);
        rule11 = Random.Range(-1f, 1f);
        rule12 = Random.Range(-1f, 1f);
        rule13 = Random.Range(-1f, 1f);
        rule14 = Random.Range(-1f, 1f);
        rule15 = Random.Range(-1f, 1f);
        rule16 = Random.Range(-1f, 1f);

        //Debug.Log("Rule 1 = " + rule1);
        //Debug.Log("Rule 2 = " + rule2);
        //Debug.Log("Rule 3 = " + rule3);
        //Debug.Log("Rule 4 = " + rule4);
        //Debug.Log("Rule 5 = " + rule5);
        //Debug.Log("Rule 6 = " + rule6);
        //Debug.Log("Rule 7 = " + rule7);
        //Debug.Log("Rule 8 = " + rule8);
        //Debug.Log("Rule 9 = " + rule9);
        //Debug.Log("Rule 10 = " + rule10);
        //Debug.Log("Rule 11 = " + rule11);
        //Debug.Log("Rule 12 = " + rule12);
        //Debug.Log("Rule 13 = " + rule13);
        //Debug.Log("Rule 14 = " + rule14);
        //Debug.Log("Rule 15 = " + rule15);
        //Debug.Log("Rule 16 = " + rule16);
    }

    private void ResetLocation(Cell[] array)
    {
        for(int i = 0; i < array.Length; i++) 
        {
            tempPosition.x = Random.Range(-50f, 50f);
            tempPosition.y = Random.Range(-50f, 50f);

            array[i].SetPosition(tempPosition);
            array[i].velocity = Vector3.zero;
        }
    }
}

public class Cell
{
    private GameObject cell;
    private GameObject cellParent;
    private Color color;
    public Vector3 position;
    public Vector3 velocity;

    public Cell(GameObject cellObject, Color color, Vector3 position, Vector3 velocity, GameObject parent)
    {
        this.cell = GameObject.Instantiate(cellObject);
        this.cellParent = parent;
        this.color = color;
        this.position = position;
        this.velocity = velocity;
        SetupCell();
    }

    private void SetupCell()
    {
        Renderer renderer = cell.GetComponent<Renderer>();
        renderer.material.color = color;
        cell.transform.position = position;
        cell.transform.parent = cellParent.transform;
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
        cell.transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}

// Single moving creature

//Rule(greenCell, greenCell, -1f);
//Rule(greenCell, redCell, -1f);
//Rule(greenCell, yellowCell, 1f);
//Rule(redCell, redCell, -1f);
//Rule(redCell, greenCell, -1f);
//Rule(yellowCell, yellowCell, 1f);
//Rule(yellowCell, greenCell, -1f);















































//[SerializeField] private GameObject cell;

//private GameObject[] firstCell;
//private GameObject firstCellParent;

//private Vector3 tempVec;

//// Start is called before the first frame update
//void Start()
//{
//    tempVec = Vector3.zero;

//    SetupParents();
//    SpawnCells(100, ref firstCell, firstCellParent);
//}

//// Update is called once per frame
//void Update()
//{

//}

//private void SpawnCells(int spawnAmount, ref GameObject[] cellArray, GameObject cellParent)
//{
//    firstCell = new GameObject[spawnAmount];

//    for (int i = 0; i < spawnAmount; i++)
//    {
//        tempVec.x = Random.Range(-50f, 50f);
//        tempVec.y = Random.Range(-50f, 50f);

//        cellArray[i] = Instantiate(cell);

//        cellArray[i].transform.position = tempVec;
//        cellArray[i].transform.parent = cellParent.transform;
//    }
//}

//private void SetupParents()
//{
//    firstCellParent = new GameObject("First Cell Parent");
//}