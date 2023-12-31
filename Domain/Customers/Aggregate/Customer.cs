﻿using Domain.Customers.ValueObjects;
using Domain.Reservations.Aggregate;
using SharedKernal.Entities;

namespace Domain.Customers.Aggregate;

public class Customer : AggregateRoot
{
	private SerialNumber _serialNumber = new SerialNumber(0);
	private Balance _balance = new Balance(0);
	private FirstName _firstName = new FirstName("");
	private LastName _lastName = new LastName("");
	private CustomerType _category = CustomerType.موظف;
	private Department _belongsToDepartment = Department.غير_محدد;
	private Note _notes = new Note("");
	
	public int SerialNumber { get => _serialNumber.Value; set => _serialNumber = new SerialNumber(value); }
	public int Balance { get => _balance.Value; set => _balance = new Balance(value); }
	public string FirstName { get => _firstName.Value; set => _firstName = new FirstName(value); }
	public string LastName { get => _lastName.Value; set => _lastName = new LastName(value); }
	public string Category { get => _category.ToString(); set => _category = Enum.Parse<CustomerType>(value); }
	public string BelongsToDepartment { get => _belongsToDepartment.ToString(); set => _belongsToDepartment = Enum.Parse<Department>(value); }
	public string Notes { get => _notes.Value; set => _notes = new Note(value); }
	public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();


	public bool IsRegular { get; set; }
	public bool Eligible { get; set; }
	public bool IsActive { get; set; }


	public Customer(long id) : base(id) { }
	public Customer() : base(0) { }


	public void DecreaseBalance(int value)
	{
		int newBalance = _balance.Value - value;
		_balance = new Balance(newBalance);
	}
	public void IncreaseBalance(int value)
	{
		int newBalance = _balance.Value + value;
		_balance = new Balance(newBalance);
	}
}
