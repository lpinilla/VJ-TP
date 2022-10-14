using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour {

	[SerializeField] private PlayableDirector timeline;

	void Start(){
		EventsManager.instance.StartIntroCutscene += PlayIntroCutscene;
		timeline.stopped += FinishIntroCutscene;
	}

	void PlayIntroCutscene(){
			timeline.Play();
	}


	void FinishIntroCutscene(PlayableDirector director) => EventsManager.instance.StopIntroCutscene();


}
