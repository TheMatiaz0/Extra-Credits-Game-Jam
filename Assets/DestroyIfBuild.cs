using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DestroyIfBuild : MonoBehaviour
{
#if !UNITY_EDITOR
	protected void Awake()
	{
		Destroy(this.gameObject);
	}
#endif
}
