using MediatR;
using OrganicEIRL.Application.DTOs;

namespace OrganicEIRL.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<List<CustomerDto>>
{

}