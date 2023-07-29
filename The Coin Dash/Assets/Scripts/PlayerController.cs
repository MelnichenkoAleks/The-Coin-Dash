using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private CapsuleCollider capsuleCollider;
    private Vector3 dir;
    private Score score;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private GameObject timerShield;
    [SerializeField] private GameObject timerStar;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreScript;

    [SerializeField] private Animator anim;

    private bool isRolling;
    private bool isImmortal;

    private int lineToMove = 1;
    public float lineDistance = 4;
    private const float maxSpeed = 110;

    public AudioSource AudioSource;
    public AudioClip coinSound;
    public AudioClip starBonusSound;
    public AudioClip shieldBonusSound;
    public AudioClip gameOverSound;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        score = scoreText.GetComponent<Score>();
        score.scoreMultiplier = 1;
        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("coins");
        coinsText.text = coins.ToString();
        StartCoroutine(SpeedIncrease());

        anim = GetComponent<Animator>();

        isImmortal = false;
    }


    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        characterController.Move(dir * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if (characterController.isGrounded)
                Jump();
        }

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        if (characterController.isGrounded && !isRolling)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            characterController.Move(moveDir);
        else
            characterController.Move(diff);

    }

    private void Jump()
    {
        StartCoroutine(Jumping());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            if (isImmortal)
                Destroy(hit.gameObject);
            else
            {
                AudioSource.PlayOneShot(gameOverSound);
                losePanel.SetActive(true);
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            AudioSource.PlayOneShot(coinSound);
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BonusStar")
        {
            AudioSource.PlayOneShot(starBonusSound);
            StartCoroutine(StarBonus());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BonusShield")
        {
            AudioSource.PlayOneShot(shieldBonusSound);
            StartCoroutine(ShieldBonus());
            Destroy(other.gameObject);
        }
    }
    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if (speed < maxSpeed)
        {
            speed += 2;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Slide()
    {
        capsuleCollider.center = new Vector3(0, 0.5f, 0);
        capsuleCollider.height = 1;
        characterController.center = new Vector3(0, 0.5f, 0);
        characterController.height = 1;
        isRolling = true;
        anim.SetTrigger("isRolling");

        yield return new WaitForSeconds(1);

        capsuleCollider.center = new Vector3(0, 1.5f, 0);
        capsuleCollider.height = 3;
        characterController.center = new Vector3(0, 1.5f, 0);
        characterController.height = 3;
        isRolling = false;
    }
    private IEnumerator StarBonus()
    {
        score.scoreMultiplier = 2;
        timerStar.SetActive(true);

        yield return new WaitForSeconds(5);
        score.scoreMultiplier = 1;
        timerStar.SetActive(false);
    }

    private IEnumerator ShieldBonus()
    {
        isImmortal = true;
        Vector3 originalScale = transform.localScale;
        transform.localScale *= 2;
        timerShield.SetActive(true);

        yield return new WaitForSeconds(5);

        transform.localScale = originalScale;
        isImmortal = false;
        timerShield.SetActive(false);
    }

    private IEnumerator Jumping()
    {
        dir.y = jumpForce;
        anim.SetTrigger("isJumping");
        capsuleCollider.center = new Vector3(0, 2f, 0);
        capsuleCollider.height = 2;
        characterController.center = new Vector3(0, 2f, 0);
        characterController.height = 2;

        yield return new WaitForSeconds(0.75f);

        capsuleCollider.center = new Vector3(0, 1.5f, 0);
        capsuleCollider.height = 3;
        characterController.center = new Vector3(0, 1.5f, 0);
        characterController.height = 3;
    }
}
