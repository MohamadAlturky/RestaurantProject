using Domain.Customers.Aggregate;
using Domain.Reservations.ValueObjects;
using Domain.Shared.Entities;
using Domain.Shared.Utilities;
using SharedKernal.Entities;

namespace Domain.Reservations.Aggregate;
public class Reservation : AggregateRoot
{
	private OrderStatus _status = OrderStatus.تم_الطلب_ولم_يؤكد;
	private NumberInQueue _numberInQueue = new NumberInQueue(0);
	private Price _price = new Price(0);


	public DateTime AtDay { get; set; } = new();
	public long CustomerId { get; set; } = 0;
	public long MealEntryId { get; set; } = 0;




	public string ReservationStatus { get => _status.ToString(); set => _status = Enum.Parse<OrderStatus>(value); }
	public int NumberInQueue { get => _numberInQueue.Value; set => _numberInQueue = new NumberInQueue(value); }
	public int Price { get => _price.Value; set => _price = new Price(value); }



	public Customer? Customer { get; set; }
	public MealEntry? MealEntry { get; set; }


	// constructor
	public Reservation(long id, long customerId, long orderedMealId) : base(id)
	{
		CustomerId = customerId;
		MealEntryId = orderedMealId;
	}
	public Reservation() : base(0) { }

	public static Reservation Create(MealEntry entry, Customer customer, PricingRecord pricingRecord, long customerId, long orderedMealId)
	{
		Reservation reservation = new Reservation(0, customerId, orderedMealId);

		reservation.MealEntry = entry;
		reservation.Customer = customer;

		if (customer.Balance < pricingRecord.Price)
		{
			throw new Exception("Don't have money hahahahahaha");
		}

		entry.ReservationsCount++;
		entry.LastNumberInQueue++;

		customer.Balance -= pricingRecord.Price;

		reservation.NumberInQueue = entry.LastNumberInQueue;
		reservation.AtDay = entry.AtDay;
		reservation.Price = pricingRecord.Price;

		if (entry.ReservationsCount > entry.PreparedCount)
		{
			reservation.ReservationStatus = OrderStatus.إنتظار.ToString();
		}
		else
		{
			reservation.ReservationStatus = OrderStatus.محجوزة.ToString();
		}

		return reservation;
	}

	public void Cancel(MealEntry entry, Customer customer)
	{
		this.MealEntry = entry;

		this.Customer = customer;

		CheckCancellationValidity();

		if (Date.ToDay == entry.AtDay)
		{
			this.ReservationStatus = OrderStatus.مطلوب_إلغائها.ToString();
		}
		if (Date.ToDay < entry.AtDay)
		{
			this.ReservationStatus = OrderStatus.ملغية.ToString();

			customer.IncreaseBalance(this.Price);

			entry.ReservationsCount--;

			//this.Price = 0;
		}
	}

	private void CheckCancellationValidity()
	{
		if (MealEntry is null)
		{
			throw new Exception("if(MealEntry is null)");
		}

		if (!MealEntry.CustomerCanCancel)
		{
			throw new Exception("if (!entry.CustomerCanCancel)");
		}
		if (Date.ToDay > MealEntry.AtDay)
		{
			throw new Exception("if(today > entry.AtDay)");
		}

		if (this.ReservationStatus == OrderStatus.مستهلكة.ToString())
		{
			throw new Exception("if(this.ReservationStatus == OrderStatus.مستهلكة.ToString())");
		}
	}
}
