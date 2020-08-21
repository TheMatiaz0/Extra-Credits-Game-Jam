using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
public class BoolResolverArgs : EventArgs
{
	public bool ResolveValue { get; private set; } = true;
	public void SendFalse(bool when=true)
	{
		if (when == false)
			return;
		ResolveValue = false;
	}
	public BoolResolverArgs()
	{

	}
}