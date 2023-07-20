using Domain.Customers.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Customers.UseCases.GetAll;
public sealed record GetAllCustomersQuery():IQuery<List<Customer>>;
