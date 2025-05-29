using System;
using System.Collections.Generic;

namespace LifeDrop.Models;

public partial class BloodInventory
{
    public int InventoryId { get; set; }

    public int CenterId { get; set; }

    public string BloodGroup { get; set; } = null!;

    public int UnitsAvailable { get; set; }
    public virtual DonationCenter Center { get; set; } = null!;
}
