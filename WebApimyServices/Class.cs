/*
public class ApplicationUser : IdentityUser
{
    public string Role { get; set; } // "customer" or "factor"
}

public class Rate
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int Rating { get; set; }
}

public class RateService
{
    private readonly DataContext _dataContext;

    public RateService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Rate[] GetAllForUser(int userId)
    {
        return _dataContext.Rates
           .Where(r => r.UserId == userId)
           .ToArray();
    }

    public Rate GetById(int id)
    {
        return _dataContext.Rates.FirstOrDefault(r => r.Id == id);
    }

    public Rate Create(Rate model)
    {
        var id = _dataContext.Rates.Count() > 0? _dataContext.Rates.Max(r => r.Id) + 1 : 1;
        model.Id = id;
        var rateEntity = _dataContext.Rates.Add(model);
        _dataContext.SaveChanges();
        return rateEntity.Entity;
    }

    public Rate Update(Rate model)
    {
        var rateEntity = _dataContext.Rates.FirstOrDefault(r => r.Id == model.Id);
        if (rateEntity!= null)
        {
            rateEntity.UserId = model.UserId;
            rateEntity.Rating = model.Rating;
            _dataContext.SaveChanges();
        }
        return rateEntity;
    }

    public Rate Delete(int id)
    {
        var rateEntity = _dataContext.Rates.FirstOrDefault(r => r.Id == id);
        if (rateEntity!= null)
        {
            _dataContext.Rates.Remove(rateEntity);
            _dataContext.SaveChanges();
        }
        return rateEntity;
    }
}

[Authorize(Roles = "customer")]
[ApiController]
[Route("api/[controller]")]
public class RateController : ControllerBase
{
    private readonly RateService _rateService;

    public RateController(RateService rateService)
    {
        _rateService = rateService;
    }

    [HttpGet]
    public ActionResult<Rate[]> GetAllForUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        return _rateService.GetAllForUser(userId);
    }

    [HttpGet("{id}")]
    public ActionResult<Rate> GetById(int id)
    {
        var rate = _rateService.GetById(id);
        if (rate == null)
        {
            return NotFound();
        }
        return rate;
    }

    [HttpPost]
    public ActionResult<Rate> Create(Rate model)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        model.UserId = userId;
        return _rateService.Create(model);
    }

    [HttpPut("{id}")]
    public ActionResult<Rate> Update(int id, Rate model)
    {
        var rate = _rateService.GetById(id);
        if (rate == null)
        {
            return NotFound();
        }
        rate.Rating = model.Rating;
        return _rateService.Update(rate);
    }

    [HttpDelete("{id}")]
    public ActionResult<Rate> Delete(int id)
    {
        var rate = _rateService.GetById(id);
        if (rate == null)
        {
            return NotFound();
        }
        return _rateService.Delete(id);
    }
}
*/
