using LifeDrop.Models;
using LifeDrop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DonationCenterController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationCenterController(ApplicationDbContext context)
    {
        _context = context;
    }

}
