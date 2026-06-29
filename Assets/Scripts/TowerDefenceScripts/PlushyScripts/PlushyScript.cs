using UnityEngine;

public class PlushyScript : MonoBehaviour
{
    private BallonMovement ballonMovement;
    private LevelLogicScript levelLogic;
    private AbstractAimingScript aimingScript;
    private int divisionID;
    public float speed;
    public int health;
    public int baseDamage;
    public int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballonMovement = new BallonMovement();
        Rigidbody2D ballonRigidBody = gameObject.GetComponent<Rigidbody2D>();
        ballonMovement.initializeMovement(ballonRigidBody, speed, divisionID);

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            turret.transform.GetChild(0).GetComponent<TurretBasicScript>().AddTarget(gameObject);
        }

        levelLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>();
        levelLogic.IncreaseNumberEnemies(1);
        if (PlayerStats.GetPolicy(3).GetChoiceMade() == 0)
        {
            aimingScript = new BasicAimingScript();
        } else
        {
            aimingScript = new AdvancedAimingScript();
        }
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

    public Vector2 GetLocationToAim(Vector3 turretLocation, float bulletSpeed)
    {
        Vector2 currentLocation = new Vector2(transform.position.x, transform.position.y);
        Vector2 heading = ballonMovement.GetNextCheckpoint() - currentLocation;
        heading.Normalize();
        return aimingScript.GetTargetLocation(currentLocation, heading, new Vector2(turretLocation.x, turretLocation.y), bulletSpeed, speed);
    }

    public int DoDamageToBase()
    {
        Destroy(gameObject);
        levelLogic.DecreaseNumberEnemies(1);
        return baseDamage;
    }

    public void setDivisionID(int ID)
    {
        divisionID = ID;
    }
}
