﻿using Fibonacci.DomainModel.App;
using System.Threading;

namespace Fibonacci.Infrastructure.Tests.Calculation.Result.Publishers.Current;

public class CalculationAppBusCurrentResultPublisherTests
{
  private readonly CalculationAppBusCurrentResultPublisher _sut;

  private readonly Mock<IAppBus> _appBusMock = new();

  public CalculationAppBusCurrentResultPublisherTests()
  {
    _sut = new CalculationAppBusCurrentResultPublisher(_appBusMock.Object);
  }

  [Fact]
  public async Task PublishCalculationResult_Always_CallsOnceAppBusPublish()
  {
    var calculationId = Guid.NewGuid();

    CalculationResult calculationResult = new(0, 0);

    var calculationResultDTO = calculationResult.ToCalculationResultDTO(calculationId);

    await _sut.PublishCalculationResult(calculationId, calculationResult, CancellationToken.None);

    _appBusMock.Verify(
      x => x.Publish(calculationId.ToString(), calculationResultDTO, CancellationToken.None),
      Times.Once());
  }
}
