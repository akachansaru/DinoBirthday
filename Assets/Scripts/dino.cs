using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dino : MonoBehaviour {
    public float walkspeed;
    public Text score;
    public GameObject blood;
    public GameObject walking;
    public GameObject instructions;
    public GameObject instructionbtn;
    public GameObject scoreimg;
    public GameObject scorebtn;
    public GameObject gameover;
    public GameObject grabit;
    public GameObject conehat;

    private AudioSource dinoAudio;
    private Animator animator;
    private Rigidbody2D rb;
    private bool walkingfw;
    private bool walkingbw;
    private bool jump;
    private bool airborn;
    private float years;
    private bool dead;
    private AudioClip lettergrab;
    private AudioClip landingSound;
    private AudioClip roar1;
    private AudioClip roar2;
    private AudioClip roar3;
    private AudioClip[] roars;

    public void EndGame() {
        Application.Quit();
    }

    public void Restart() {
        SceneManager.LoadScene("Main");
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        walkingfw = false;
        walkingbw = false;
        jump = false;
        airborn = false;
        rb = GetComponent<Rigidbody2D>();
        dinoAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        years = 0F;
        score.text = string.Concat("YEARS:", years.ToString());

        string soundFolder = "Sounds/";
        lettergrab = Resources.Load(soundFolder + "lettergrab") as AudioClip;
        landingSound = Resources.Load(soundFolder + "walking") as AudioClip;
        roar1 = Resources.Load(soundFolder + "roar1") as AudioClip;
        roar2 = Resources.Load(soundFolder + "roar2") as AudioClip;
        roar3 = Resources.Load(soundFolder + "roar3") as AudioClip;
        roars = new AudioClip[] { roar1, roar2, roar3 };
    }

    void Update() {
        if (!dead) {
            if (!airborn
                && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("roarbw")
                && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("roarfw")) {

                if (Input.GetKeyDown(KeyCode.D)) {
                    walkingfw = true;
                    animator.SetTrigger("walk");
                    walking.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.A)) {
                    walkingbw = true;
                    animator.SetTrigger("walkbw");
                    walking.SetActive(true);
                }

                if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("modelfw") &&
                    !this.animator.GetCurrentAnimatorStateInfo(0).IsName("modelbw")) {

                    if (Input.GetKeyUp(KeyCode.D)) {
                        walkingfw = false;
                        animator.SetTrigger("walk");
                        walking.SetActive(false);
                    }
                    if (Input.GetKeyUp(KeyCode.A)) {
                        walkingbw = false;
                        animator.SetTrigger("walkbw");
                        walking.SetActive(false);
                    }
                }
                
                if (Input.GetKeyDown(KeyCode.Space)) {
                    walking.SetActive(false);
                    walkingbw = false;
                    walkingfw = false;
                    jump = true;
                    airborn = true;
                    animator.SetTrigger("jump");
                }
            }
            if ((years == 30F)) {
                grabit.SetActive(true);
                conehat.SetActive(true);
            }
        }
    }

    void FixedUpdate() {
        if (walkingfw) {
            rb.velocity = Vector2.right * walkspeed;
        }
        if (walkingbw) {
            rb.velocity = Vector2.left * walkspeed;
        }
        if (jump) {
            rb.AddForce(Vector2.up * 9f, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("triggerenter");
        if (other.gameObject.CompareTag("floor") && airborn) {
            airborn = false;
            dinoAudio.PlayOneShot(landingSound, 1f);
            animator.SetTrigger("roar");
            dinoAudio.PlayOneShot(roars[Random.Range(0, roars.Length)], 1f);
        }
        if (other.gameObject.CompareTag("letter")) {
            Debug.Log("interacting");
            other.gameObject.SetActive(false);
            dinoAudio.PlayOneShot(lettergrab, 1f);
            years = years + 3f;
            score.text = string.Concat("YEARS:", years.ToString());
        }
        if (other.gameObject.CompareTag("spikes")) {
            grabit.SetActive(false);
            conehat.SetActive(false);
            walking.SetActive(false);
            dead = true;
            blood.SetActive(true);
            walkingfw = false;
            walkingbw = false;
            jump = false;
            airborn = false;
            animator.SetTrigger("die");
            instructions.SetActive(false);
            instructionbtn.SetActive(false);
            scoreimg.SetActive(false);
            scorebtn.SetActive(false);
            gameover.SetActive(true);
        }
        if (other.gameObject.CompareTag("bdayhat")) {
            SceneManager.LoadScene("Victory");
        }
    }
}
