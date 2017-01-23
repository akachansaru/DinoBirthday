using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dino : MonoBehaviour {

	public float walkspeed;
	public Rigidbody2D rb;
	public Text score;
	public AudioClip lettergrab;
	public AudioClip boom;
	public AudioClip roar1;
	public AudioClip roar2;
	public AudioClip roar3;
	public GameObject blood;
	public GameObject walking;
	public GameObject instructions;
	public GameObject instructionbtn;
	public GameObject scoreimg;
	public GameObject scorebtn;
	public GameObject gameover;
	public GameObject grabit;
	public GameObject conehat;
	AudioSource audio;
	Animator animator;

	private bool walkingfw;
	private bool walkingbw;
	private bool jump;
	private bool airborn;
	private float years;
	private bool dead;
	private AudioClip[] roars;
	private int start;
	private int end;

	public void endgame() {
		Application.Quit();
	}
	public void restart() {
		Application.LoadLevel ("Main");
	}

	// Use this for initialization
	void Start () {
		walkingfw = false; walkingbw = false; jump = false; airborn = false;
		rb = GetComponent <Rigidbody2D> ();
		audio = GetComponent <AudioSource>();
		animator = GetComponent <Animator> ();
		years = 0F;
		score.text = string.Concat("YEARS:",years.ToString());
		roars = new AudioClip[] {roar1,roar2,roar3}; start = 0; end = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead == false) {
			if ((Input.GetKeyDown (".")) && (airborn == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarfw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarbw") == false)) {
				walkingfw = true;
				animator.SetTrigger ("walk");
				walking.SetActive(true);
			}
			if (Input.GetKeyUp (".") && (airborn == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("modelfw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("modelbw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarfw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarbw") == false)) {
				walkingfw = false;
				animator.SetTrigger ("walk");
				walking.SetActive(false);
			}

			if ((Input.GetKeyDown (",")) && (airborn == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarbw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarfw") == false)) {
				walkingbw = true;
				animator.SetTrigger ("walkbw");
				walking.SetActive(true);
			}
			if (Input.GetKeyUp (",") && (airborn == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("modelbw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("modelfw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarbw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarfw") == false)) {
				walkingbw = false;
				animator.SetTrigger ("walkbw");
				walking.SetActive(false);
			}
			if ((Input.GetKeyDown ("space")) && (airborn == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarbw") == false) && (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("roarfw") == false)) {
				walking.SetActive(false);
				walkingbw = false;
				walkingfw = false;
				jump = true;
				airborn = true;
				animator.SetTrigger ("jump");
			}
		}
		if ((years == 30F) && (dead == false)) {
			grabit.SetActive(true);
			conehat.SetActive(true);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (walkingfw) {
			rb.velocity = Vector2.right * walkspeed;
		}
		if (walkingbw) {
			rb.velocity = Vector2.left * walkspeed;
		}
		if (jump) {
			rb.AddForce(Vector2.up * 9F, ForceMode2D.Impulse);
			jump = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("triggerenter");
		if (other.gameObject.CompareTag ("floor") && airborn) {
			airborn = false;
			audio.PlayOneShot(boom,1F);
			animator.SetTrigger("roar");
			audio.PlayOneShot(roars[Random.Range(start,end)],1F);//roars[Random.Range(1,3)]
		}
		if (other.gameObject.CompareTag ("letter")) {
			Debug.Log ("interacting");
			other.gameObject.SetActive(false);
			audio.PlayOneShot(lettergrab,1F);
			years = years + 3F;
			score.text = string.Concat("YEARS:",years.ToString());
		}
		if (other.gameObject.CompareTag ("spikes")) {
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
		if (other.gameObject.CompareTag ("bdayhat")) {
			Application.LoadLevel ("Victory");
		}
	}
}
