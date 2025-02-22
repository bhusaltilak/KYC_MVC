using KYC_MVC.Data;  
using KYC_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

using System.Threading.Tasks;

namespace KYC_MVC.Controllers
{
    public class KYCController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KYCController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(KYC kyc)
        {

            if (ModelState.IsValid)
            {
                // Check if phone number or email already exists
                bool exists = await _context.KYCs.AnyAsync(k => k.Email == kyc.Email || k.Phone == kyc.Phone);

                if (exists)
                {
                    // Add validation error messages
                    if (_context.KYCs.Any(k => k.Email == kyc.Email))
                        ModelState.AddModelError("Email", "This email is already registered.");

                    if (_context.KYCs.Any(k => k.Phone == kyc.Phone))
                        ModelState.AddModelError("Phone", "This phone number is already registered.");

                    return View(kyc);
                }

                // If no duplicate, save the new entry
                _context.KYCs.Add(kyc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success", new { id = kyc.Id });
            }

            return View(kyc);
        }

        public async Task<IActionResult> Dashboard()
        {
            var kycData = await _context.KYCs.ToListAsync();
            return View(kycData);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var kyc = await _context.KYCs.FindAsync(id);
            if (kyc == null)
            {
                return NotFound();
            }
            return View(kyc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, KYC kyc)
        {
            if (id != kyc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(kyc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard");
            }
            return View(kyc);
        }

        public async Task<IActionResult> Success(int id)
        {
            var kyc = await _context.KYCs.FindAsync(id);
            if (kyc == null)
            {
                return NotFound();
            }
            return View(kyc);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var kyc = await _context.KYCs.FindAsync(id);
            if (kyc == null)
            {
                return NotFound();
            }

            _context.KYCs.Remove(kyc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard"); // Redirect to Dashboard after deletion
        }

    }

}