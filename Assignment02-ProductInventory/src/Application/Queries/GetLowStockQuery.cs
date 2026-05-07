using Mediator.Net.Contracts;
using Assignment02_ProductInventory.Application.DTOs;

namespace Assignment02_ProductInventory.Application.Queries;

public record GetLowStockQuery(int Threshold) : IRequest;
