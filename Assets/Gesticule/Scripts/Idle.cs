using System.Collections;

public class Idle: State<CharacterAI>
{
    private static Idle instance;
 
	private Idle() {}
 
    public static Idle getInstance() {
 
        if (instance == null) {
            instance = new Idle();
        }
        return instance;
    }
 
    public override void Enter(CharacterAI owner) { }
 
 
    public override void Execute(CharacterAI owner) {
		
		if (owner.CurrentAnimation != string.Empty)
			owner.GetFSM().ChangeState(Animated.getInstance());
		else {
			owner.character.animation.CrossFade("Idle");
		}
    }
 
    public override void Exit(CharacterAI owner) { }
}
