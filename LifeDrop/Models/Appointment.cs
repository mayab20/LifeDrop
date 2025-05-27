using System;
using System.Collections.Generic;

namespace LifeDrop.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int UserId { get; set; }

    public int CenterId { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly TimeSlot { get; set; }

    public string Status { get; set; } = null!;

    public virtual DonationCenter Center { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
