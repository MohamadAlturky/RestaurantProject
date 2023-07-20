using Domain.Customers.Aggregate;
using SharedKernal.CQRS.Commands;

namespace Infrastructure.Authentication.Register;
public sealed record RegisterNewCustomerCommand(Customer customer, string password) : ICommand;
