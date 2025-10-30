﻿using MediatR;
using OrganicEIRL.Application.DTOs;

namespace OrganicEIRL.Application.Features.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<List<ProductDto>>
{

}