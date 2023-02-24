namespace hinos.dialogue {

    public class Dialogue {
        [SerializeField] private DialogueNode startingNode;
    }

    public abstract class DialogueNode {
        [SerializeField] private string text;

        public string Text => text;
    }

    public class BasicDialogueNode {
        [SerializeField] private DialogueNode nextNode;
    }

    public class BranchingDialogueNode {
        [SerializeField] private BasicDialogueNode[] branches;
    }

    public class DialogueController {

    }

    public interface IDialogueVisitor {
        void VisitNode(BasicDialogueNode node);
        void VisitNode(BranchingDialogueNode node);
    }

    public class DialogueBoxGUI : MonoBehavior, IDialogueVisitor, ICancelHandler {
        public TMPro_Text dialogueBoxText;
        public RectTransform choiceContainer;

        public void VisitNode(BasicDialogueNode node) {
            dialogueBoxText.text = node.text;
        }

        public void VisitNode(BranchingDialogueNode node) {
            dialogueBoxText.text = node.text;

            for(var i = 0; i < node.branches.Length; i++) {

            }

            ProcessShowChoiceContainer();
        }

        public void OnCancel(BasicEventData eventData) {

        }

        private void ProcessShowChoiceContainer() {
            choiceContainer.gameObject.SetActive(true);
        }
    }

    public class DialogueChoiceGUI : MonoBehavior, IPointerClickHandler, ISubmitHandler {
        [SerializeField] private TMPro_Text text;
        
        private BasicDialogueNode choiceNode;

        public BasicDialogueNode ChoiceNode {
            set {
                choiceNode = value;
                ProcessUpdateChoice();
            }
        }


        private void ProcessUpdateChoice() {
            TMPro_Text.text = choiceNode.text;
        }

        public void OnPointerClick(PointerEventData eventData) {

        }

        public void OnSubmit(BasicEventData eventData) {

        }
    }
}