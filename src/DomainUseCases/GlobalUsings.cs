﻿global using System.Collections.Concurrent;
global using System.Reflection;
global using System.Threading.Channels;
global using Fibonacci.DomainModel.Calculation;
global using Fibonacci.DomainModel.Calculation.Logic;
global using Fibonacci.DomainModel.Calculation.Logic.Service;
global using Fibonacci.DomainModel.Calculation.Result;
global using Fibonacci.DomainModel.Calculation.Result.Consumer;
global using Fibonacci.DomainModel.Calculation.Result.Publishers;
global using Fibonacci.DomainModel.Calculation.Results;
global using Fibonacci.DomainUseCases.App;
global using Fibonacci.DomainUseCases.Calculation;
global using Fibonacci.DomainUseCases.Calculation.Actions.SendResult;
global using Fibonacci.DomainUseCases.Calculation.DTOs;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
