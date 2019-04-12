using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class VRTransform : MonoBehaviour {
	void Update () {
        //transform.localRotation = InputTracking.GetLocalRotation(VRNode.CenterEye) * Quaternion.Euler(45, 0, 0);
        transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye) * Quaternion.Euler(45, 0, 0);

    }
}
