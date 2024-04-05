using Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Controllers;

public class PatientsController : Controller
{
	private readonly ILogger<PatientsController> _logger;
	private readonly MainDbContext _context;

	public PatientsController(ILogger<PatientsController> logger, MainDbContext context)
	{
		_logger = logger;
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> Index(string order = "id_asc", int pageNumber = 1)
	{
		var orders = new[] { "id_asc", "id_desc", "first_name_asc", "first_name_desc", "last_name_asc", "last_name_desc", "pesel_asc", "pesel_desc" };
		if (!orders.Contains(order))
		{
			return NotFound();
		}

		ViewData["IdSort"] = order == "id_asc" ? "id_desc" : "id_asc";
		ViewData["FirstNameSort"] = order == "first_name_asc" ? "first_name_desc" : "first_name_asc";
		ViewData["LastNameSort"] = order == "last_name_asc" ? "last_name_desc" : "last_name_asc";
		ViewData["PeselSort"] = order == "pesel_asc" ? "pesel_desc" : "pesel_asc";

		var name = string.Join('_', order.Split('_')[..^1]);
		var direction = order.Split('_')[^1];
		var delegates = new Dictionary<string, Func<Patient, object>>
		{
			{ "id", p => p.Id },
			{ "first_name", p => p.FirstName },
			{ "last_name", p => p.LastName },
			{ "pesel", p => p.Pesel }
		};

		var patients = await _context.Patients
				.Include(p => p.Address)
				.AsNoTracking()
				.ToListAsync();

		if (pageNumber < 1)
        {
        	pageNumber = 1;
        }

		var paginatedPatients = new PaginatedList<Patient>(patients.AsQueryable(), pageNumber, 5);
		paginatedPatients.Items = direction == "asc" ?
			paginatedPatients.Items.OrderBy(delegates[name]).ToList():
			paginatedPatients.Items.OrderByDescending(delegates[name]).ToList();

		return View(paginatedPatients);
	}

	[HttpGet]
	public async Task<IActionResult> Details(int? id)
	{
		if (id is null or < 0)
		{
			return NotFound();
		}

		var patient = await _context.Patients
			.Include(s => s.Address)
			.AsNoTracking()
			.FirstOrDefaultAsync(m => m.Id == id);

		if (patient == null)
		{
			return NotFound();
		}

		return View(patient);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int? id)
	{
		if (id is null or < 0)
        {
            return NotFound();
        }

		var patient = await _context.Patients
			.Include(s => s.Address)
			// .AsNoTracking()
			.FirstOrDefaultAsync(m => m.Id == id);

		if (patient is null)
		{
			return NotFound();
		}

		return View(patient);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Patient patient)
    {
    	var patientWithSamePesel = await _context.Patients.Where(p => p.Pesel == patient.Pesel && p.Id != patient.Id).FirstOrDefaultAsync();
    	if (patientWithSamePesel != null)
    	{
    		ModelState.AddModelError("Pesel", "Pacjent o podanym numerze PESEL już istnieje!");
    	}

    	if (ModelState.IsValid)
    	{
    		patient.FirstName = Utils.ToTitle(patient.FirstName.Trim());
    		patient.LastName = Utils.ToTitle(patient.LastName.Trim());
    		patient.Address.StreetName = Utils.ToTitle(patient.Address.StreetName.Trim());
    		patient.Address.City = Utils.ToTitle(patient.Address.City.Trim());

			try
			{
				_context.Adresses.Update(patient.Address);
				_context.Patients.Update(patient);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateException exception)
			{
				_logger.LogError(exception, "Error occurred while saving data.");
				ModelState.AddModelError("", "Zapisanie danych nie powiodło się. Spróbuj ponownie.");
			}
    	}
    	return View();
    }

    [HttpPost]
	public async Task<IActionResult> Delete(int? id)
	{
		if (id is null or < 0)
		{
			return NotFound();
		}

		var patient = await _context.Patients
			.Include(s => s.Address)
			.AsNoTracking()
			.FirstOrDefaultAsync(m => m.Id == id);

		if (patient is null)
		{
			return NotFound();
		}

		try
		{
			_context.Adresses.Remove(patient.Address);
			_context.Patients.Remove(patient);
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateException exception)
		{
			_logger.LogError(exception, "Error occurred while deleting data.");
			return NotFound();
		}

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Patient patient)
	{
		var patientWithSamePesel =
			await _context.Patients.Where(p => p.Pesel == patient.Pesel).FirstOrDefaultAsync();
		if (patientWithSamePesel != null)
		{
			ModelState.AddModelError("Pesel", "Pacjent o podanym numerze PESEL już istnieje!");
		}

		if (ModelState.IsValid)
		{
			patient.FirstName = Utils.ToTitle(patient.FirstName.Trim());
			patient.LastName = Utils.ToTitle(patient.LastName.Trim());
			patient.Address.StreetName = Utils.ToTitle(patient.Address.StreetName.Trim());
			patient.Address.City = Utils.ToTitle(patient.Address.City.Trim());

			try
			{
				_context.Add(patient);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateException exception)
			{
				_logger.LogError(exception, "Error occurred while saving data.");
				ModelState.AddModelError("", "Zapisanie danych nie powiodło się. Spróbuj ponownie.");
			}
		}

		return View();
	}
}