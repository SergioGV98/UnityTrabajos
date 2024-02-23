using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LevelController : MonoBehaviour
{
    public Ball ball;
    public Player player;
    int score = 0;
    int brickQuantity = 0;
    public int lives = 3;
    public float timeLimit = 180f; 
    private float currentTime;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI messageFinal;

    private Vector3 initialPlayerPosition;

    void Start()
    {
        initialPlayerPosition = player.transform.position;
        currentTime = timeLimit;
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            timer.text =  "Tiempo restante: " + currentTime.ToString("F0");
            Debug.Log("Tiempo restante: " + currentTime.ToString("F0") + " segundos");
        }

        Debug.Log("Se ha alcanzado el límite de tiempo. Reiniciando partida...");
        StartCoroutine(ResetLevel());
    }

    public void SetBrickQuantity(int quantity)
    {
        brickQuantity = quantity;
    }

    public void OnBrickCollided(GameObject brick)
    {
        RemoveBrick(brick);
        score++;
        brickQuantity--;
        Debug.Log("Score: " + score);
        Debug.Log("Bricks restantes " + brickQuantity);
        if (brickQuantity == 0)
        {
            messageFinal.text = "¡Enhorabuena! ¡Has ganado!";
            messageFinal.gameObject.SetActive(true);
            Debug.Log("¡Enhorabuena! ¡Has ganado!");
            StartCoroutine(ResetLevel());
        }
    }

    private void RemoveBrick(GameObject brick)
    {
        brick.GetComponent<Brick>().DestroyBrick();
        Destroy(brick.transform.gameObject);
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2f);
        messageFinal.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnDie()
    {
        Debug.Log("Has muerto");
        lives--;
        Debug.Log("Vidas restantes: " + lives);
        if (lives == 0)
        {
            Debug.Log("Perdiste todas las vidas, reiniciando partida...");
            messageFinal.text = "Perdiste todas las vidas, reiniciando partida...";
            messageFinal.gameObject.SetActive(true);
            StartCoroutine(ResetLevel());
        }
        else
        {
            StartCoroutine(RespawnBall());
        }
    }

    IEnumerator RespawnBall()
    {
 
        player.transform.position = initialPlayerPosition;

        Vector3 respawnPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.45f, player.transform.position.z);

        if (Mathf.Abs(ball.transform.position.y - respawnPosition.y) < 0.1f)
        {
            respawnPosition.y += 0.45f; 
        }

        ball.transform.position = respawnPosition;

        player.GetComponent<FixedJoint2D>().enabled = true;

        yield return player.LaunchBall();
    }

}
