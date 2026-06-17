using UnityEngine;

public class PlushyScript : MonoBehaviour
{
    private BallonMovement ballonMovement;
    private LevelLogicScript levelLogic;
    public float speed;
    public int health;
    public int baseDamage;
    public int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballonMovement = new BallonMovement();
        Rigidbody2D ballonRigidBody = gameObject.GetComponent<Rigidbody2D>();
        ballonMovement.initializeMovement(ballonRigidBody, speed);

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            turret.transform.GetChild(0).GetComponent<TurretBasicScript>().AddTarget(gameObject);
        }

        levelLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>();
        levelLogic.IncreaseNumberEnemies(1);
    }

    // Update is called once per frame
    void Update()
    {
        ballonMovement.move();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            levelLogic.DecreaseNumberEnemies(1);
            levelLogic.gainMoney(value);
        }
    }

    public int DoDamageToBase()
    {
        Destroy(gameObject);
        levelLogic.DecreaseNumberEnemies(1);
        return baseDamage;
    }
}
