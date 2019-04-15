using UnityEngine;
using Tools;

public abstract class Skills : ScriptableObject
{
    public string id = "-1";
    public int playAnim = 1;
    public float coldTime = 2.0f;

    public abstract void ReleaseNow(GameObject hero);
    public abstract void ReleaseAnimEvent1(GameObject hero);
    public abstract void ReleaseAnimEvent2(GameObject hero);
    public abstract void ReleaseAnimEvent3(GameObject hero);
}

public class LineAttack : Skills
{
    public GameObject[] spawnParticles = new GameObject[1];

    public override void ReleaseAnimEvent1(GameObject hero)
    {
        var obj = hero.transform.Find("SpawnLow");
        Instantiate(spawnParticles[0], obj.position, obj.rotation);
    }

    public override void ReleaseAnimEvent2(GameObject hero)
    {
        throw new System.NotImplementedException();
    }

    public override void ReleaseAnimEvent3(GameObject hero)
    {
        throw new System.NotImplementedException();
    }

    public override void ReleaseNow(GameObject hero)
    {
        
    }
}
