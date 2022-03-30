using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodFightGame : MonoBehaviour
{
    public BoxCollider spawnArea;
    public List<Target> targetPrefabs = new List<Target>();
    public TMP_Text scoreText;
    public TMP_Text remainingTimeText;

    [SerializeField]
    private float gameDuration;
    private bool isGameOver => gameDuration <= 0;
    private int currentScore = 0;
    
    void Start()
    {
        SpawnTarget();
    }

    
    void Update()
    {
        SetRemaingTime(gameDuration - Time.deltaTime);
    }

    public void SetRemaingTime(float value)
    {
        gameDuration = value;

        if (isGameOver)
        {
            gameDuration = 0;

            // show gameover screen
        }

        remainingTimeText.text = "Remaing Time: " + gameDuration.ToString("0.0");
    }

    public void AddToScore(int value)
    {
        currentScore = currentScore + value;
        scoreText.text = "Score: " + currentScore;
    }

    public void OnTargetHit(Target target)
    {
        // spawn the target
        SpawnTarget();

        // add to the score
        AddToScore(target.points);
    }

    private void SpawnTarget()
    {
        int n = Random.Range(0, targetPrefabs.Count);
        var newTarget = Instantiate(targetPrefabs[n], GetRandomPosition(), targetPrefabs[n].transform.rotation);
        newTarget.game = this;
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x,y,z);
    }

}
