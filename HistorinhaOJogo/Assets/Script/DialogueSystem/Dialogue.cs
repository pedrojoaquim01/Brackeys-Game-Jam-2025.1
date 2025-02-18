using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject {
        [field:SerializeField] public string Name { get; private set; } = string.Empty;
        [field:SerializeField][field:Multiline] public string Text { get; private set; } = string.Empty;
        [field:SerializeField] public float SpeechSpeed { get; private set; } = .02f;
        [field:SerializeField] public float AutoAvanceTime { get; private set; } = -1f;
        public bool IsAutoAdvance => AutoAvanceTime > 0f;
    }
}