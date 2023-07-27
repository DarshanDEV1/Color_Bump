using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }

    [SerializeField] private float slowMotionFactor = 0.1f; // Time scale factor for slow-motion mode
    [SerializeField] private Transform startTransform; // Starting position of the ball
    [SerializeField] private Transform goalTransform; // Ending position of the ball
    [SerializeField] private BallController ball; // Reference to the ball's controller
    [SerializeField] Transform levelParentTransform; // Reference to the parent of the level
    [SerializeField] GameObject[] level; // Levels prefab array
    [SerializeField] int currentLevel;

    public float EntireDistance { get; private set; }
    public float DistanceLeft { get; private set; }
    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("Level");

        Transform prefabTransform = Instantiate(level[currentLevel]).transform;
        prefabTransform.SetParent(levelParentTransform);

        startTransform = GameObject.Find("StartPosition").transform;
        goalTransform = GameObject.Find("EndPosition").transform;


        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Start()
    {
        EntireDistance = goalTransform.position.z - startTransform.position.z;

        GameObject.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("StartScene");
        });
    }

    public void StartGame()
    {
        GameStarted = true;
        Debug.Log("Game started.");
    }

    public void EndGame(bool win)
    {
        GameEnded = true;
        Debug.Log("Game Ended.");

        if (!win)
        {
            // Restart the game after 2 seconds.
            Invoke("RestartGame", 2 * slowMotionFactor);

            // Set the time scale for slow-motion mode.
            Time.timeScale = slowMotionFactor;

            // Set the time interval for physics and frame updates.
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            // If the player wins, restart the game with slow-motion.
            Invoke("RestartGame", 2 * slowMotionFactor);
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        DistanceLeft = Vector3.Distance(ball.transform.position, goalTransform.position);

        if (DistanceLeft > EntireDistance)
        {
            // The starting line has not been crossed yet.
            DistanceLeft = EntireDistance;
        }

        // The ball has crossed the finish line.
        if (ball.transform.position.z > goalTransform.transform.position.z)
        {
            DistanceLeft = 0;
        }

        //Debug.Log("Distance Traveled: " + (EntireDistance - DistanceLeft) + "/" + EntireDistance);
    }
}
