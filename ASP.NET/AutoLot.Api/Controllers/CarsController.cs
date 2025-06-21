namespace AutoLot.Api.Controllers;

public class CarsController : BaseCrudController<Car, CarsController>
{
    public CarsController(IAppLogging<CarsController> logger, ICarRepo mainRepo) : base(logger, mainRepo)
    {
    }

    /// <summary>
    /// Retrieves a collection of cars filtered by their make identifier.
    /// </summary>
    /// <remarks>If a valid make identifier is provided, the method returns all cars associated with the
    /// specified make. If no identifier is provided or the identifier is invalid, the method returns all cars, ignoring
    /// any query filters.</remarks>
    /// <param name="id">The optional identifier of the car make. Must be greater than 0 to filter by make.</param>
    /// <returns>A collection of <see cref="Car"/> objects. If the make identifier is valid, the collection contains cars
    /// associated with the specified make. Otherwise, the collection contains all cars.</returns>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [HttpGet("bymake/{id:int?}")]
    [ApiVersion("1.0")]
    public ActionResult<IEnumerable<Car>> GetCarsByMake(int? id)
    {
        if (id is > 0)
        {
            return Ok(((ICarRepo)MainRepo).GetAllBy(id.Value));
        }

        return Ok(MainRepo.GetAllIgnoreQueryFilters());
    }
}
