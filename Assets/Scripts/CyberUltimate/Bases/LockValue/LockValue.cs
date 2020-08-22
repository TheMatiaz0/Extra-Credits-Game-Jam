using Cyberultimate;
using Cyberultimate.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;


namespace Cyberultimate
{
	public class LockValue<T>
	where T : struct, IComparable<T>
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
			LockValue<T> Hp { get; }
			T ValueBeforeChange { get; }
			T ValueAfterChange { get; }
		}
		public class AnyValueChangedArgs : EventArgs, ILockArgs
		{
			public T Last { get; }
			public T Actual { get; }
			public Action Action { get; }
			public Element Element { get; }
			public string From { get; }
			public LockValue<T> Hp { get; }
			T ILockArgs.ValueAfterChange => Actual;
			T ILockArgs.ValueBeforeChange => Last;
			public AnyValueChangedArgs(T last, T actual, Action action, Element element, string who, LockValue<T> hp)
			{
				Last = last;
				Actual = actual;
				Action = action;
				Element = element;
				From = who ?? throw new ArgumentNullException(nameof(who));
				Hp = hp;
			}


		}
		public class CanChangeArgs : BoolResolverArgs, ILockArgs
		{
			public string From { get; }
			public LockValue<T> Hp { get; }
			public T TryTo { get; }
			public Action Action { get; }
			public T ValueBeforeChange { get; }
			T ILockArgs.ValueAfterChange => TryTo;
			public CanChangeArgs(LockValue<T> hp, T tryTo, Action action, string from)
			{
				Hp = hp ?? throw new ArgumentNullException(nameof(hp));
				TryTo = tryTo;

				Action = action;
				From = from;
				ValueBeforeChange = hp.Value;

			}


		}
		private ILockValuePacker<T> packer;
		public ReadOnlyCollection<string> Effectors => effectors.AsReadOnly();
		public T Value
		{
			get => _Value;
			private set
			{
				if (value.Equals(_Value))
					return;
				_Value = MathHelper.Clamp<T>(value, Min, Max);


			}
		}

		public T Min { get; }
		public readonly T MaxOfMax;

		public event EventHandler<AnyValueChangedArgs> OnValueChanged = delegate { };
		public event EventHandler<AnyValueChangedArgs> OnHpTaken = delegate { };
		public event EventHandler<AnyValueChangedArgs> OnHpGiven = delegate { };
		public event EventHandler<AnyValueChangedArgs> OnValueChangeToMin = delegate { };
		public event EventHandler<AnyValueChangedArgs> OnMaxValueChanged = delegate { };
		public event EventHandler<CanChangeArgs> CanChange = delegate { };

		private T _Value = default;
		private T _Max;
		private readonly List<string> effectors = new List<string>();
		public LockValue(T max, T min, T value, T? maxOfMax = null)
		{
			_Max = max;
			Min = min;
			Value = value;
			MaxOfMax = maxOfMax ?? max;
			packer = Cyberultimate.Utility.LockValuePackersFactory.Create<T>();
		}
		public void SetValue(T val, string from="")
		{
			if (val.Equals(Value))
				return;
			Action action = (val.CompareTo(Value) == 1) ? Action.Add : Action.Take;
			CanChangeArgs args;

			args = new CanChangeArgs(this, val, action, from);
			CanChange(this, args);
			if (args.ResolveValue)
			{
				effectors.Add(from);
				T last = Value;
				Value = val;
				AnyValueChangedArgs ev = new AnyValueChangedArgs(last, Value, action, Element.Value, from, this);
				OnValueChanged(this, ev);
				if (Value.Equals(Min))
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
		public void SetMax(T val, string who)
		{
			T last = _Max;
			if (_Max.Equals(val))
				return;
			val = MathHelper.Clamp<T>(val, packer.GetMin(), MaxOfMax);
			_Max = val;
			if (_Value.CompareTo(_Max) == 1)
			{
				SetValue(_Max, "Max");
			}
			OnMaxValueChanged.Invoke(this, new AnyValueChangedArgs(last, val, (last.CompareTo(val) == 1) ? Action.Take : Action.Add, Element.Max, who, this));

		}
		public string GetLastEffector() => (Effectors.Count >= 1) ? Effectors[Effectors.Count - 1] : null;
		public void TakeValue(T value, string from="")
		{
			SetValue(packer.Remove(Value, value), from);
		}
		public void GiveValue(T value, string from="")
		{

			SetValue(packer.Add(Value, value), from);
		}
		public bool IsInRange(T val)
			=> Max.CompareTo(val) != -1 && Min.CompareTo(val) != 1;
		public T Max
		{
			get => _Max;
		}

		public override string ToString()
		{
			return $"{Value}/{Max}";
		}


	}
}