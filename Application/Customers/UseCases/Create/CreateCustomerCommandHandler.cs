﻿using Domain.Customers.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;
using Application.Customers.Errors;
using Domain.Customers.Exceptions;

namespace Application.UseCases.Customers.Create;
internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
	private ICustomerRepository _customerRepository;

	private IUnitOfWork _unitOfWork;

	public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
	{
		try
		{
			_customerRepository.Add(request.customer);
			await _unitOfWork.SaveChangesAsync();

		}
		catch(SomeOneHasTheSameSerialNumberException exception)
		{
			return Result.Failure(CustomerErrorsDictionary.SomeOneHasTheSameSerialNumber(exception.Message));
		}
		catch (Exception exception)
		{
			return Result.Failure(CustomerErrorsDictionary.DataBaseIgnoredToAddTheCustomer(exception.Message));
		}

		return Result.Success();
	}
}
