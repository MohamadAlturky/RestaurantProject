﻿using SharedKernal.ValueObjects;

namespace Domain.Reservations.ValueObjects;
public class Price : ValueObject<int>
{
	public Price(int value) : base(value)
	{
	}

	protected override void Validate(int value)
	{
		if(value < 0)
		{
			throw new ArgumentOutOfRangeException("value");
		}
	}
}
