using System.Collections;

public class State<T> {
	
    public State() { }
 
	//Executara quando o estado der entrada
    public virtual void Enter(T owner) { }
 
	//Este e o estado de execuçao do update
    public virtual void Execute(T owner) { }
 
	//Sera executado quando o estado sair.
    public virtual void Exit(T owner) { }
}
