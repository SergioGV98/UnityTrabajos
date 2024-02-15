using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Ball ball;
    public Player player;
    int score = 0;
    int brickQuantity = 0;
    int lives = 3;
    void Start()
    {
        Brick[] bricks = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        brickQuantity = bricks.Length;
    }

    public void OnBrickCollided(Brick brick)
    {
        RemoveBrick(brick);
        score++;
        Debug.Log("Score: " + score);
        if(score == brickQuantity)
        {
            Debug.Log("¡Enhorabuena! ¡Has ganado!");
            StartCoroutine(ResetLevel());
        }
    }

    private void RemoveBrick(Brick brick)
    {
        Destroy(brick.transform.gameObject);
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    public void OnDie()
    {
        Debug.Log("Has muerto");
        lives--;
        if(lives == 0)
        {
            Debug.Log("Perdiste todas las vidas, reiniciando partida...");
            StartCoroutine(ResetLevel());
        } else
        {
            StartCoroutine(RespawnBall());
        }
    }
    IEnumerator RespawnBall()
    {
        ball.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        player.GetComponent<FixedJoint2D>().enabled = true;
        yield return player.LaunchBall();
    }
}
