using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CapsuleMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    public float speed, rotationSpeed, jumpHeight;  
    float cooldown = 5f; 
    bool death = false;
    float timer = 0; 
    // Start is called before the first frame update
    void Start()
    {  
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotational Movement
        float rotation = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        rb.rotation  += rotation; 
        float x = Input.GetAxis("Vertical");    
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up.normalized,1.25f,~LayerMask.GetMask("Ignore Raycast"));
        if(hit){
            timer = 0; 
            rb.velocity = rb.velocity + (x * speed * (Vector2)transform.right.normalized * Time.deltaTime); 
            if(Input.GetKeyDown(KeyCode.Space)) {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight); 
            } 
        }else{
            timer += Time.deltaTime; 
            if(timer >= 4){
                 if(transform.childCount>0){
                    transform.GetChild(0).gameObject.AddComponent<Rigidbody2D>();
                    transform.DetachChildren();
                 }
            }
            if(timer >= 5.5){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("6 seconds");
            }
        }
        if(death){
            cooldown -= Time.deltaTime; 
        }
        if(cooldown<=0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }   
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.parent.name == "Finish"){
            string s = SceneManager.GetActiveScene().name;
           SceneManager.LoadScene("Level" + (s[s.Length-1]-'0' + 1));
        }

        
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.otherCollider.transform.name == this.transform.name){
            if(transform.childCount>0){
                transform.GetChild(0).gameObject.AddComponent<Rigidbody2D>();
                transform.DetachChildren();
            }
            death = true;
        }
    }

}

