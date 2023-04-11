using System;
using System.Globalization;

namespace PayrollEngine;

/// <summary>Represents a payroll calendar</summary>
public class PayrollCalendar : IPayrollCalendar
{
    /// <inheritdoc/>
    public int TenantId { get; }

    /// <inheritdoc/>
    public int? UserId { get; }

    /// <inheritdoc/>
    public CultureInfo Culture { get; }

    /// <inheritdoc/>
    public Calendar Calendar => Culture.Calendar;

    /// <inheritdoc/>
    public CalendarConfiguration Configuration { get; }

    /// <summary>Initializes a new instance of the <see cref="PayrollCalendar"/> class</summary>
    /// <param name="configuration">The calendar configuration</param>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="userId">The user id</param>
    /// <param name="culture">The culture</param>
    public PayrollCalendar(CalendarConfiguration configuration, int tenantId, int? userId = null,
        CultureInfo culture = null)
    {
        TenantId = tenantId;
        UserId = userId;
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        Culture = culture ?? CultureInfo.CurrentCulture;
    }

    /// <summary>Returns the week of year for the specified DateTime</summary>
    /// <param name="moment">The moment of the week</param>
    /// <returns> The returned value is an integer between 1 and 53</returns>
    public int GetWeekOfYear(DateTime moment) =>
        Calendar.GetWeekOfYear(moment, (System.Globalization.CalendarWeekRule)Configuration.CalendarWeekRule,
            (System.DayOfWeek)Configuration.FirstDayOfWeek);
}