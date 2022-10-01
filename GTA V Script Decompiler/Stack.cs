using Decompiler.Ast;
using System;
using System.Collections.Generic;

namespace Decompiler
{
	public class Stack
	{
		List<AstToken> _stack;
		Function function;

		public Stack(Function func)
		{
			_stack = new List<AstToken>();
			function = func;
		}

		public void Dispose()
		{
			_stack.Clear();
		}

		internal void Push(AstToken token)
		{
			_stack.Add(token);
		}

		internal AstToken Pop(bool allowMulti = false)
		{
			int index = _stack.Count - 1;
			if (index < 0)
			{
				return new AstToken(function);
			}
			var val = _stack[index];
			_stack.RemoveAt(index);

			if (val.GetStackCount() != 1 && !allowMulti)
				throw new InvalidOperationException();

			return val;
		}

		internal AstToken Peek()
		{
			if (_stack.Count == 0)
				return new AstToken(function);
			return _stack[^1];
		}

		internal AstToken PeekIdx(int index)
		{
			return _stack[^(index + 1)];
		}

		internal AstToken PopVector()
		{
			if (Peek().GetStackCount() == 3)
				return Pop(true);
			else if (_stack.Count >= 3 && PeekIdx(0).GetStackCount() == 1 && PeekIdx(1).GetStackCount() == 1 && PeekIdx(2).GetStackCount() == 1)
			{
				return new Vector(function, Pop(), Pop(), Pop());
			}
			return new Vector(function, new(function), new(function), new(function));
		}

		internal List<AstToken> PopCount(int count)
		{
			if (count == 0)
				return new List<AstToken>();

			List<AstToken> values = new();
			int popped = 0;
			while (true)
			{
				if (_stack.Count != 0)
				{
					var val = Pop(true);
					if (popped + val.GetStackCount() > count)
						throw new InvalidOperationException("Require split of multi-count stack token");
					values.Add(val);
					popped += val.GetStackCount();
				}
				else
				{
					values.Add(Pop());
					popped++;
				}

				if (popped == count)
					break;
			}
			
			values.Reverse();
			return values;
		}

		internal int GetCount()
		{
			int count = 0;

			foreach (var val in _stack)
				count += val.GetStackCount();

			return count;
		}

		#region Opcodes

		/// <summary>
		/// returns a string saying the size of an array if its > 1
		/// </summary>
		/// <param name="immediate"></param>
		/// <returns></returns>
		/// <remarks>TODO: Move this somewhere else</remarks>
		public static string GetArraySizeCmt(uint immediate)
		{
			if (!Properties.Settings.Default.ShowArraySize)
				return "";
			if (immediate == 1)
				return "";
			return " /*" + immediate.ToString() + "*/";
		}

		#endregion

		#region subclasses

		public enum DataType
		{
			Int,
			IntPtr,
			Float,
			FloatPtr,
			String,
			StringPtr,
			Bool,
			Unk,
			UnkPtr,
			Unsure,
			None, //For Empty returns
			Vector3,

			BoolPtr,
			Any,
			AnyPtr,
			Blip,
			BlipPtr,
			Cam,
			CamPtr,
			Entity,
			EntityPtr,
			FireId,
			FireIdPtr,
			Hash,
			HashPtr,
			Interior,
			InteriorPtr,
			ItemSet,
			ItemSetPtr,
			Object,
			ObjectPtr,
			Ped,
			PedPtr,
			Pickup,
			PickupPtr,
			Player,
			PlayerPtr,
			ScrHandle,
			ScrHandlePtr,
			Vector3Ptr,
			Vehicle,
			VehiclePtr,

			Function,

			// ENUMS
			
			eControlType,
			eControlAction,
			eHudComponent,
			ePedType,
			ePedComponentType,
			eStackSize,
			eDecoratorType,
			eEventGroup,
			eHudColour,
			eBlipSprite,
			eKnockOffVehicle,
			eCombatMovement,
			eCombatAttribute,
			eCharacter,
			eTransitionState,
			eDispatchType,
			eLevelIndex
		}

		#endregion
	}
}
