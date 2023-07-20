﻿using SharedKernal.ValueObjects;

namespace Domain.Customers.ValueObjects;
public class FirstName : ValueObject<string>
{
	public FirstName(string value) : base(value) { }

	protected override void Validate(string value)
	{
		// no validation needed
	}
}
