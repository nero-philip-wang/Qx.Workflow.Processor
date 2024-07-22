using System.Collections.Generic;

public class StepCondition
{
    public IEnumerable<ValueCondition> Value { get; set; }
    public string Function { get; set; }
}