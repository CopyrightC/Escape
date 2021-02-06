using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocketmovment : MonoBehaviour
{
    [SerializeField] float rscthrs = 70f;
    [SerializeField] float speedthr = 100f;
    [SerializeField] AudioClip ThrustSnd;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelcross;
    [SerializeField] ParticleSystem Rockpart;
    [SerializeField] ParticleSystem Lvlpart;
    [SerializeField] ParticleSystem Deathpart;
    enum gamestate {Alive,Dead,Changing};
    gamestate state = gamestate.Alive;
    Rigidbody rockrigi;
    AudioSource audiosrc;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        rockrigi = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (state == gamestate.Alive){
            HandleThrust();
            checkRotation();
        }
        
    }
    void OnCollisionEnter(Collision collision){
        if (state!=gamestate.Alive){return;}
        switch(collision.gameObject.tag){
            case "Friendly":
                Debug.Log("OK");
                break;
            case "Finish":
                state = gamestate.Changing;
                audiosrc.Stop();
                audiosrc.PlayOneShot(levelcross);
                Lvlpart.Play();
                Invoke("LoadnxtScene",1f);
                break;
            case "Death":
                state = gamestate.Dead;
                audiosrc.Stop();
                audiosrc.PlayOneShot(deathSound);
                Deathpart.Play();
                Invoke("previousscene",1f);
                break;
            default: 
                break;
            
        }
    }
    void LoadnxtScene() {
        int lvlindex = SceneManager.GetActiveScene().buildIndex;
        int nxtindex = lvlindex+1;
        if (nxtindex == 5){
            nxtindex = 0;
        }
        SceneManager.LoadScene(nxtindex);
    }
    void previousscene(){
        SceneManager.LoadScene(0);
    }
    private void HandleThrust(){
        if (Input.GetKey(KeyCode.Space)){
            rockrigi.AddForce(Vector3.up * speedthr);
            if (!audiosrc.isPlaying){
                audiosrc.PlayOneShot(ThrustSnd);
            }
            Rockpart.Play();
        }
        else{
            audiosrc.Stop();
            Rockpart.Stop();
        }
        
    }
    private void checkRotation(){
        
        rockrigi.freezeRotation = true;
        float speed = rscthrs*Time.deltaTime;
        if (Input.GetKey(KeyCode.A)){
          
            rockrigi.AddForce(Vector3.left*rscthrs);
        }
        else if (Input.GetKey(KeyCode.D)){
          
            rockrigi.AddForce(Vector3.right*rscthrs);
        }
        rockrigi.freezeRotation = false;
    }
}