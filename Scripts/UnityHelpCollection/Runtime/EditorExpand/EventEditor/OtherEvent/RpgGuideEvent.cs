using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    public class RpgGuideEvent : m_Event
    {
        public int index;

        public override void PlayEvents(m_Trigger sender, object info)
        {
            var c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayableController>();
            c.ChangeGuideText(index);
        }
    }
}
