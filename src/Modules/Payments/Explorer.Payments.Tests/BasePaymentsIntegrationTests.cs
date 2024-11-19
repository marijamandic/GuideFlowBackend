using Explorer.BuildingBlocks.Tests;

namespace Explorer.Payments.Tests;

public class BasePaymentsIntegrationTests : BaseWebIntegrationTest<PaymentsTestFactory>
{
    public BasePaymentsIntegrationTests(PaymentsTestFactory factory) : base(factory) { }
}
