using UnityEngine;
using Tools;

public abstract class Skills : ScriptableObject
{
    public int playAnim = 1;

    public abstract void ReleaseNow(GameObject hero);
    public abstract void ReleaseAnimEvent1(GameObject hero);
    public abstract void ReleaseAnimEvent2(GameObject hero);
    public abstract void ReleaseAnimEvent3(GameObject hero);
}

public class LineAttack : Skills
{
    public GameObject[] spawnParticles = new GameObject[3];

    public override void ReleaseAnimEvent1(GameObject hero)
    {
        Instantiate(spawnParticles[0], hero.transform.Find("SkillSpawn").position, hero.transform.rotation);
        WaitTimeManager.WaitTime(0.5f, delegate () { Instantiate(spawnParticles[1], hero.transform.Find("SkillSpawn").position, hero.transform.rotation); });
        WaitTimeManager.WaitTime(0.5f, delegate () { Instantiate(spawnParticles[2], hero.transform.Find("SkillSpawn").position, hero.transform.rotation); });
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
