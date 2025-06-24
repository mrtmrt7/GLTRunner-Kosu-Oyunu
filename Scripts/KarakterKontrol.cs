using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class KarakterKontrol : MonoBehaviour
{
   [SerializeField] Animator animator;
   [SerializeField] Rigidbody rb;
   [SerializeField] float speed;
   [SerializeField] private float lateralSmoothSpeed=10f;
   [SerializeField] private GameObject menuCanvas;
   [SerializeField] TMP_Text scoreText;
   [SerializeField] private AudioSource effectSource,musicSource;
   [SerializeField] AudioClip coinClip,deathClip;
   [SerializeField] TMP_Text CoinText;
   [SerializeField] float jumpForce = 2f;
   [SerializeField] TMP_Text SkorText;

   private float[] xPosition={-0.344f,0.014f,0.375f};
   int currentXpositionIndex=1;
   Vector3 targetPosition;
  public bool isJumping = false;


   public bool isAlive=true;

  private float score;
  public int coinCount = 0;
    void Start()
    {
        targetPosition= transform.position;
        UpdateCoinText();

    }
    void UpdateCoinText()
{
    CoinText.text = ": " + coinCount.ToString();
}

    void Update()
    {
      if(isAlive){

        score+=Time.deltaTime;
        scoreText.text="Skor:"+score.ToString("f1");
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
           Vector3 jumpDirection = (Vector3.up + Vector3.forward * 0.3f).normalized;
rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

            isJumping = true;
        }
        
         if(Input.GetKeyDown(KeyCode.A)&& currentXpositionIndex>0)
      {
        currentXpositionIndex--;
        UpdateLateralPosition();
      } 
      else if(Input.GetKeyDown(KeyCode.D)&& currentXpositionIndex<2)
      {
        currentXpositionIndex++;
        UpdateLateralPosition();
      } 

      }
     
    }
    private void FixedUpdate()
    {
        if(isAlive){
          Vector3 forwardMove=Vector3.forward *speed* Time.fixedDeltaTime;
        Vector3 currentPosition=rb.position;
        Vector3 lateralMove=Vector3.Lerp(currentPosition,targetPosition,Time.fixedDeltaTime*lateralSmoothSpeed);
        Vector3 combineMove=new Vector3(lateralMove.x,transform.position.y,rb.position.z)+ forwardMove;
        rb.MovePosition(combineMove);

        }
        
    }
    void Die()
{
    isAlive = false;
    rb.isKinematic = true;
    animator.avatar = null; 
    animator.SetBool("Die", true);
    musicSource.Stop();
    effectSource.clip = deathClip;
    effectSource.Play(); 
    menuCanvas.SetActive(true); 
    SkorText.text = "Skor: " + score.ToString("F1");

}


    void UpdateLateralPosition(){
        targetPosition=new Vector3(xPosition[currentXpositionIndex],transform.position.y,transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Arabalar")) 
    {
        Die();
    
    }
     else if (collision.gameObject.CompareTag("Ground"))
    {
        isJumping = false; // yere düştükten sonra tekrar zıplayabiliriz
    }
}
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Barikat"))
    {
        
            Die();
            
    
    }
        else if(other.gameObject.CompareTag("Coin"))
        {
          score+=100;
          speed+=0.15f;
          effectSource.clip=coinClip;
          effectSource.Play();
          Destroy(other.gameObject);
          coinCount++;
        UpdateCoinText();

        }
    }

    

}
