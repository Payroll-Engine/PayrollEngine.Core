using System;

namespace PayrollEngine;

/// <summary>Payroll date period</summary>
public interface IPayrollPeriod
{
    /// <summary>The period start</summary>
    DateTime Start { get; }

    /// <summary>The period end</summary>
    DateTime End { get; }

    /// <summary>The period name</summary>
    string Name { get; }

    /// <summary>Get the payroll period by moment</summary>
    /// <param name="moment">The moment of the period</param>
    /// <param name="offset">The offset:<br />
    /// less than zero: past<br />
    /// zero: current<br />
    /// greater than zero: future<br /></param>
    /// <returns>Payroll moment period</returns>
    IPayrollPeriod GetPayrollPeriod(DateTime moment, int offset = 0);
}