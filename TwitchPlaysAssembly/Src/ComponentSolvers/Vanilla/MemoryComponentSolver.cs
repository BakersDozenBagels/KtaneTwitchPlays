using System.Collections;

public class MemoryComponentSolver : ComponentSolver
{
    public MemoryComponentSolver(BombCommander bombCommander, MemoryComponent bombComponent) :
        base(bombCommander, bombComponent)
	{
		_buttons = bombComponent.Buttons;
        modInfo = ComponentSolverFactory.GetModuleInfo("MemoryComponentSolver", "!{0} position 2, !{0} pos 2, !{0} p 2 [2nd position] | !{0} label 3, !{0} lab 3, !{0} l 3 [label 3]");
    }

    protected override IEnumerator RespondToCommandInternal(string inputCommand)
    {
        string[] commandParts = inputCommand.ToLowerInvariant().Split(' ');

        if (commandParts.Length != 2)
        {
            yield break;
        }

        if (!int.TryParse(commandParts[1], out int buttonNumber))
        {
            yield break;
        }

        if (buttonNumber >= 1 && buttonNumber <= 4)
        {
            if (commandParts[0].EqualsAny("position", "pos", "p"))
            {
                yield return "position";
				
                yield return DoInteractionClick(_buttons[buttonNumber - 1]);
            }
            else if (commandParts[0].EqualsAny("label", "lab", "l"))
            {
                foreach(KeypadButton button in _buttons)
                {
                    if (button.GetText().Equals(buttonNumber.ToString()))
                    {
                        yield return "label";
                        yield return DoInteractionClick(button);
                        break;
                    }
                }
            }
        }
    }

    private KeypadButton[] _buttons = null;
}
