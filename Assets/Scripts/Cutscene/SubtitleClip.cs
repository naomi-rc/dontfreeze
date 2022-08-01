using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Custscene
{
    public class SubtitleClip : PlayableAsset
    {
        public string subtitleText;
        public Color textColor;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);
            SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
            subtitleBehaviour.subtitleText = subtitleText;
            subtitleBehaviour.textColor = textColor;
            return playable;
        }
    }
}

