using UnityEngine;

public class AdvancedAimingScript : AbstractAimingScript
{
    public override Vector2 GetTargetLocation(Vector2 targetLocation, Vector2 targetHeading, Vector2 shooterLocation, float bulletSpeed, float enemySpeed)
    {
        Vector2 distance = targetLocation - shooterLocation;
        Vector2 turretHeading = distance;
        turretHeading.Normalize();
        float time = distance.x / (bulletSpeed * turretHeading.x);

        return targetLocation + targetHeading * enemySpeed * time;
    }
}
