using UnityEngine;

public class BasicAimingScript : AbstractAimingScript
{
    public override Vector2 GetTargetLocation(Vector2 targetLocation, Vector2 targetHeading, Vector2 shooterLocation, float bulletSpeed, float enemySpeed)
    {
        return targetLocation;
    }
}
