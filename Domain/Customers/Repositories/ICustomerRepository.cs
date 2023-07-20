using Domain.Customers.Aggregate;
using SharedKernal.Repositories;

namespace Domain.Customers.Repositories;
public interface ICustomerRepository : IRepository<Customer>
{
	Customer? GetBySerialNumber(int serialNumber);
	//void IncreaseBalance(int serialNumber, int valueToIncrease);
	//void DecreaseBalance(int serialNumber, int valueToDecrease);
	long CalculateSumOfBalances();
}
