namespace hinos.dialogue {

    public class DialoguePrompt {
        public string text;
        public List<DialoguePrompt> branchingPromts;
    }

    public class DialogueController {

    }

    public class DialogueBoxGUI : MonoBehavior {
        public TMPro_Text dialogueBoxText;
    }
}