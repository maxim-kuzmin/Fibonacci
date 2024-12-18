﻿global using System.Net.Http.Json;
global using System.Threading.Channels;
global using EasyNetQ;
global using Fibonacci.DomainModel.App;
global using Fibonacci.DomainModel.Calculation;
global using Fibonacci.DomainModel.Calculation.Result;
global using Fibonacci.DomainModel.Calculation.Result.Consumer;
global using Fibonacci.DomainModel.Calculation.Result.Publishers;
global using Fibonacci.DomainUseCases.Calculation;
global using Fibonacci.DomainUseCases.Calculation.Actions.SendResult;
global using Fibonacci.DomainUseCases.Calculation.DTOs;
global using Fibonacci.Infrastructure.App;
global using Fibonacci.Infrastructure.App.Buses;
global using Fibonacci.Infrastructure.App.Config;
global using Fibonacci.Infrastructure.Calculation.Result.Publishers.Current;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using OpenTelemetry;
global using OpenTelemetry.Metrics;
global using OpenTelemetry.Resources;
global using OpenTelemetry.Trace;
global using Serilog;
global using Serilog.Extensions.Logging;
global using Serilog.Sinks.OpenTelemetry;
