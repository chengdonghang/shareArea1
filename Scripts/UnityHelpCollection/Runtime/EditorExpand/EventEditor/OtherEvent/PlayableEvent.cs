using System.Collections;
using UnityEngine.Playables;
using UnityEngine;

namespace EventEditor
{
    public class PlayableEvent : m_Event
    {
        public PlayableController director;
        public bool ifStart = false;

        public override void PlayEvents(m_Trigger sender, object info)
        {
            director = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayableController>();
            if (ifStart)
            {
                director.Play();
            }
            else
            {
                director.Resume();
                
            }
        }
    }
}
