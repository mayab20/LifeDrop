namespace LifeDrop.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalDonations { get; set; }
        public required Dictionary<string, int> BloodGroupStats { get; set; }
    }
}
