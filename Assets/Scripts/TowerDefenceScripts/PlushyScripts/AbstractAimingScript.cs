using UnityEngine;

public abstract class AbstractAimingScript
{
    public abstract Vector2 GetTargetLocation(Vector2 targetLocation, Vector2 targetHeading, Vector2 shooterLocation, float bulletSpeed, float enemySpeed);
}
