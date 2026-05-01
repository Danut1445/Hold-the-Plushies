using UnityEngine;

public class OurBaseScript : MonoBehaviour
{
    public LevelLogicScript levelLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyPlushy")
        {
            levelLogic.takeDamage(collision.gameObject.GetComponent<PlushyScript>().doDamageToBase());
        }
    }
}
