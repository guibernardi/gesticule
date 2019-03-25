using System.Collections;

public class Animated: State<CharacterAI>
{
    private static Animated instance;
 
	private Animated() {}
 
    public static Animated getInstance() {
 
        if (instance == null) {
            instance = new Animated();
        }
        return instance;
    }
 
    public override void Enter(CharacterAI owner) { }
 
 
    public override void Execute(CharacterAI owner) {
		
		if (owner.CurrentAnimation != string.Empty) {

			owner.character.animation.CrossFade(owner.CurrentAnimation);
			owner.CurrentAnimation = "";

		} else {

			if (!owner.character.animation.isPlaying) {

				owner.GetFSM().ChangeState(Idle.getInstance());
			}
		}
    }
 
    public override void Exit(CharacterAI owner) { }
}
