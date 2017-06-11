using Microsoft.Extensions.Options;

public class BusinessMan : IBusinessMan
{
    private readonly float swagAmount;
    public BusinessMan(IOptions<TestConfig> config) {
        swagAmount = config.Value.SampleFloat;
    }
    public float GetSwag()
    {
        return swagAmount;
    }
}