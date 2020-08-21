using Cyberultimate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LockValue
{
	public enum Action
	{
		Add,
		Take
	}
	public enum Element
	{
		Value,
		Max
	}
	public interface ILockArgs
	{
		Action Action { get; }
		LockValue LockedValue { get; }
		uint ValueBeforeChange { get; }
		uint ValueAfterChange { get; }
	}
	public class AnyHpValueChangedArgs : EventArgs, ILockArgs
	{
		public uint Last { get; }
		public uint Actual { get; }
		public Action Action { get; }
		public Element Element { get; }
		public string From { get; }
		public LockValue LockedValue { get; }
		uint ILockArgs.ValueAfterChange => Actual;
		uint ILockArgs.ValueBeforeChange => Last;
		public AnyHpValueChangedArgs(uint last, uint actual, Action action, Element element, string who, LockValue hp)
		{
			Last = last;
			Actual = actual;
			Action = action;
			Element = element;
			From = who ?? throw new ArgumentNullException(nameof(who));
			LockedValue = hp;
		}


	}
	public class CanChangeArgs : BoolResolverArgs, ILockArgs
	{
		public string From { get; }
		public LockValue LockedValue { get; }
		public int Change { get; }
		public uint TryTo { get; }
		public Action Action { get; }
		public uint ValueBeforeChange { get; }
		uint ILockArgs.ValueAfterChange => TryTo;
		public CanChangeArgs(LockValue hp, int change, uint tryTo, Action action, string from)
		{
			LockedValue = hp ?? throw new ArgumentNullException(nameof(hp));
			TryTo = tryTo;
			Change = change;
			Action = action;
			From = from;
			ValueBeforeChange = hp.Value;

		}


	}
	public ReadOnlyCollection<string> Effectors => effectors.AsReadOnly();
	public uint Value
	{
		get => _Value;
		private set
		{
			if (value == _Value)
				return;
			_Value = MathHelper.Clamp((uint)value, Min, Max);


		}
	}

	public uint Min { get; }
	public readonly uint MaxOfMax;

	public event EventHandler<AnyHpValueChangedArgs> OnValueChanged = delegate { };
	public event EventHandler<AnyHpValueChangedArgs> OnHpTaken = delegate { };
	public event EventHandler<AnyHpValueChangedArgs> OnHpGiven = delegate { };
	public event EventHandler<AnyHpValueChangedArgs> OnValueChangeToMin = delegate { };
	public event EventHandler<AnyHpValueChangedArgs> OnMaxValueChanged = delegate { };
	public event EventHandler<CanChangeArgs> CanGetSmaller = delegate { };

	private Cint _Value = 0;
	private uint _Max;
	private readonly List<string> effectors = new List<string>();
	public LockValue(uint max, uint min, uint value, uint? maxOfMax = null)
	{
		_Max = max;
		Min = min;
		Value = value;
		MaxOfMax = maxOfMax ?? max;
	}
	public void SetValue(Cint val, string from)
	{
		if (val == Value)
			return;
		Action action = (val > Value) ? Action.Add : Action.Take;
		CanChangeArgs args = null;
		if (action == Action.Take)
		{
			args = new CanChangeArgs(this, ((int)(uint)val) - ((int)(uint)Value), val, action, from);
			CanGetSmaller(this, args);
		}

		if (action == Action.Add || args.ResolveValue)
		{
			effectors.Add(from);
			Cint last = Value;
			Value = val;
			AnyHpValueChangedArgs ev = new AnyHpValueChangedArgs(last, Value, action, Element.Value, from, this);
			OnValueChanged(this, ev);
			if (Value == Min)
				OnValueChangeToMin(this, ev);
			switch (action)
			{
				case Action.Add:
					OnHpGiven(this, ev); break;
				case Action.Take:
					OnHpTaken(this, ev); break;
			}
		}

	}
	public void SetMax(Cint val, string who)
	{
		uint last = _Max;
		if (_Max == val)
			return;
		val = (uint)Mathf.Clamp(val, 0, MaxOfMax);
		_Max = val;
		if (_Value > _Max)
		{
			SetValue(_Max, "Max");
		}
		OnMaxValueChanged.Invoke(this, new AnyHpValueChangedArgs(last, val, (last > val) ? Action.Take : Action.Add, Element.Max, who, this));

	}
	public string GetLastEffector() => (Effectors.Count >= 1) ? Effectors[Effectors.Count - 1] : null;
	public void Take(uint value, string from)
	{
		SetValue(Value - value, from);
	}

	public void Give(uint value, string from)
	{

		SetValue(Value + value, from);
	}
	public bool IsInRange(float val)
		=> Max >= val && Min <= val;
	public uint Max
	{
		get => _Max;
	}
	public Cint GetFragmental(float numerator, float denominator)
	{
		return (Cint)(this.Value * (numerator / denominator));
	}

	public override string ToString()
	{
		return $"{Value}/{Max}";
	}



}