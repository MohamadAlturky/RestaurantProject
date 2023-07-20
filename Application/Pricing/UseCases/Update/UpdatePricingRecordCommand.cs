using SharedKernal.CQRS.Commands;

namespace Application.Pricing.UseCases.Update;
public sealed record UpdatePricingRecordCommand(long Id, int price) : ICommand;