using UnityEngine;
using System.Collections;

public class CharacterAI : MonoBehaviour {
	
#region "Declaraçoes"
	
	StateMachine<CharacterAI> m_pStateMachine;
	private string m_pCurrentAnimation = string.Empty;
	private string m_pPreviousAnimation = string.Empty;
	
	private delegate void GUIMethod();
    private GUIMethod currentGUIMethod;
	private string _SelectedLetter = string.Empty;
	public string[] words;
	public string[] dictionary;
	
	public GameObject character;
	private Vector2 scrollPosition;
	
#endregion
	
#region "Propriedades"
	
	public StateMachine<CharacterAI> GetFSM(){ 
		return m_pStateMachine; 
	}
	
	public string SelectedLetter {
		get { return _SelectedLetter; }
		set { _SelectedLetter = value; }
	}
	
	public string CurrentAnimation {
		get { return m_pCurrentAnimation; }
		set { m_pCurrentAnimation = value; }
	}
	
	public string PreviousAnimation {
		get { return m_pPreviousAnimation; }
		set { m_pPreviousAnimation = value; }
	}
	
#endregion
	
#region "Metodos"
	
	void Start() {
		
		this.currentGUIMethod = MainMenu;
		
		character = GameObject.FindGameObjectWithTag("Player");
		
		m_pStateMachine = new StateMachine<CharacterAI>(this);
  		m_pStateMachine.SetCurrentState(Idle.getInstance());
  		StartCoroutine(UpdateFSM());
	}
	
	void Update() {
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (this.currentGUIMethod == MainMenu) {
   				Application.Quit();
			} else if((this.currentGUIMethod == ListMenu) && (!this.SelectedLetter.Equals(string.Empty))){
				this.SelectedLetter = string.Empty;
			} else {
				this.currentGUIMethod = MainMenu;
			}
		}
	}
 
 	IEnumerator UpdateFSM () {
  		while(true){
   			m_pStateMachine.Update();
   			yield return new WaitForSeconds(0.1f);
  		}
 	}	
	
	private void ListMenu() {
		
		string wFirstLetter = string.Empty;
		
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));

		foreach (Touch touch in Input.touches) {
        	if (touch.phase == TouchPhase.Moved) {
				scrollPosition.y += touch.deltaPosition.y;
        	}
		}
		
        for (int i=0; i < dictionary.Length; i++) {
			
			if (this.SelectedLetter.Equals(string.Empty)) {
			
				if (GUILayout.Button(dictionary[i], GUILayout.Width(Screen.width - (Screen.width/8)), GUILayout.Height(Screen.height / 8))) {
					
					this.SelectedLetter = dictionary[i];
				}
				
			} else {
				
				if (GUILayout.Button(dictionary[i], GUILayout.Width(Screen.width - (Screen.width/8)), GUILayout.Height(Screen.height / 8))) {
					
					if (string.Equals(dictionary[i], this.SelectedLetter, System.StringComparison.Ordinal)) {
						this.SelectedLetter = string.Empty;
					} else {
						this.SelectedLetter = dictionary[i];
					}
				}
					
				for (int x=0; x < words.Length; x++) {
						
					if (string.Equals(dictionary[i], this.SelectedLetter, System.StringComparison.Ordinal)) {
						
						wFirstLetter = words[x];
						
						if (string.Equals(wFirstLetter.Substring(0,1), this.SelectedLetter, System.StringComparison.Ordinal)) {
								
							GUI.backgroundColor = Color.blue;
							if (GUILayout.Button(words[x], GUILayout.Width(Screen.width - (Screen.width/8)), GUILayout.Height(Screen.height / 12))) {
								this.CurrentAnimation = words[x];
								this.PreviousAnimation = words[x];
								this.SelectedLetter = string.Empty;
								this.currentGUIMethod = MainMenu;	
							}
							GUI.backgroundColor = Color.gray;
						}
					}
				}
			}
        }
		GUILayout.EndScrollView();
	}
	
	private void AboutMenu() {
		
		GUILayout.BeginArea(new Rect(Screen.width / 2 - (Screen.width / 4), Screen.height / 2 - (Screen.height / 4), Screen.width / 2, Screen.height / 2),"", "Box");
		
		GUILayout.Label("Gesticule v1.0 BETA");
		GUILayout.Label("Projeto desenvolvido para a disciplina de Trabalho de Conclusao de Curso no Instituto Municipal de Ensino Superior de Catanduva - IMES Fafica");
		GUILayout.Label("Sugestoes, criticas ou duvidas:");
		GUILayout.Label("gui.bernardi09@gmail.com");
		GUILayout.Label("Guilherme Bernardi - 2013");
		
		if (GUILayout.Button("OK")) {
			this.currentGUIMethod = MainMenu;
		}
		
		GUILayout.EndArea();
	}
	
	private void MainMenu() {
		
		if (this.PreviousAnimation != string.Empty) {
			
			if (GUI.Button(new Rect(0, 0, Screen.width / 4, Screen.height / 8), "Replay")) {
				this.CurrentAnimation = this.PreviousAnimation;
			}
		}
		
		if (GUI.Button(new Rect(Screen.width - (Screen.width / 4), 0, Screen.width / 4, Screen.height / 8), "Sobre")) {
			this.currentGUIMethod = AboutMenu;
		} else if (GUI.Button(new Rect(0, Screen.height - (Screen.height / 8), Screen.width/2, Screen.height / 8), "Dicionario")) {
			this.currentGUIMethod = ListMenu;
		} else if (GUI.Button(new Rect(Screen.width/2, Screen.height - (Screen.height / 8), Screen.width/2, Screen.height / 8), "Sair")) {
			Application.Quit();
		}
	}
	
#endregion

#region "Construtores"
	
    public void OnGUI() {
		this.currentGUIMethod();
    }
	
#endregion
	
}