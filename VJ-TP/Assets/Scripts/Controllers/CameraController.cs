using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

	private CinemachineVirtualCamera _virCam;

	[SerializeField] private Transform characterHeadTransform;
	[SerializeField] private Transform gunScopeTransform;

	private bool isScoped;

	private CinemachinePOV aim;

	void Start(){
			_virCam = GetComponent<CinemachineVirtualCamera>();
			isScoped = false;
			EventsManager.instance.onToggleScope += ToggleScope;
			aim = _virCam.GetCinemachineComponent<CinemachinePOV>();
	}

	void ToggleScope(){
		if(isScoped){
			//put follow transform on gun
			_virCam.Follow = gunScopeTransform;
			//Adjust FoV
			_virCam.m_Lens.FieldOfView = 40;
			//reduce camera angles
			//Debug.Log(aim.m_VerticalAxis.GetType().GetProperties());
			aim.m_VerticalAxis = new AxisState(-30, 30, false, true, 300, 0.1f, 0.1f, "Mouse Y", true);
		}else{
			_virCam.Follow = characterHeadTransform;
			_virCam.m_Lens.FieldOfView = 60;
			aim.m_VerticalAxis = new AxisState(-50, 12, false, true, 300, 0.1f, 0.1f, "Mouse Y", true);
		}
		isScoped = !isScoped;
	}

}
